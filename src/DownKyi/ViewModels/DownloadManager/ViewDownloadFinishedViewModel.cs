using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.CustomControl;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Services;
using DownKyi.Services.Download;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadFinishedViewModel : BaseViewModel
    {
        #region 其他属性
        public const string Tag = "PageDownloadManagerDownloadFinished";
        private CancellationTokenSource tokenSource2;
        // 下载数据存储服务
        private readonly DownloadStorageService downloadStorageService;

        #endregion

        #region 页面属性申明
        /// <summary>
        /// 所有的下载项 数量
        /// </summary>
        private int recordCountDownloaded;

        public int RecordCountDownloaded { get; set; }

        /// <summary>
        /// 上次选择的排序方式
        /// </summary>
        private int lastTimeSortSelected;

        private ObservableCollection<DownloadedItem> downloadedList;
        /// <summary>
        /// 页面上显示的  下载项
        /// </summary>
        public ObservableCollection<DownloadedItem> DownloadedList
        {
            get => downloadedList;
            set
            {
                SetProperty(ref downloadedList, value);
            }
        }

        private CustomSimplePagerViewModel pager;
        public CustomSimplePagerViewModel Pager
        {
            get => pager;
            set => SetProperty(ref pager, value);
        }

        private int finishedSortBy;
        public int FinishedSortBy
        {
            get => finishedSortBy;
            set => SetProperty(ref finishedSortBy, value);
        }

        private Visibility downloadedItemLoadingVisibility;
        /// <summary>
        /// 下载完成项加载中  元素的显示控制
        /// </summary>
        public Visibility DownloadedItemLoadingVisibility
        {
            get => downloadedItemLoadingVisibility;
            set => SetProperty(ref downloadedItemLoadingVisibility, value);
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        private GifImage downloadedItemLoading;
        public GifImage DownloadedItemLoading
        {
            get => downloadedItemLoading;
            set => SetProperty(ref downloadedItemLoading, value);
        }

        /// <summary>
        /// 查询 筛选 图标
        /// </summary>
        public VectorImage QueryIcon { get; private set; }

        /// <summary>
        /// 导入数据库 图标
        /// </summary>
        public VectorImage ImportDataIcon { get; private set; }

        /// <summary>
        /// 导出数据库 图标
        /// </summary>
        public VectorImage ExportDataIcon { get; private set; }

        /// <summary>
        /// 清空数据库 图标
        /// </summary>
        public VectorImage ClearDataIcon { get; private set; }

        #endregion

        #region 构造函数
        public ViewDownloadFinishedViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            downloadStorageService = new DownloadStorageService();
            // 设置对下载中页面 下载完成消息的监听处理
            eventAggregator.GetEvent<DownloadFinishedEvent>().Subscribe(DownloadFinishedHandler);

            // 设置页面显示的排序方式
            DownloadFinishedSort finishedSort = SettingsManager.GetInstance().GetDownloadFinishedSort();
            switch (finishedSort)
            {
                case DownloadFinishedSort.DOWNLOAD:
                    FinishedSortBy = 0;
                    break;
                case DownloadFinishedSort.UPZHUID:
                    FinishedSortBy = 1;
                    break;
                case DownloadFinishedSort.FILESIZE:
                    FinishedSortBy = 2;
                    break;
                case DownloadFinishedSort.VIDEODURATION:
                    FinishedSortBy = 3;
                    break;
                case DownloadFinishedSort.ZONEID:
                    FinishedSortBy = 4;
                    break;
                case DownloadFinishedSort.NUMBER:
                    FinishedSortBy = 5;
                    break;
                default:
                    FinishedSortBy = 0;
                    break;
            }

            #region 图标初始化
            QueryIcon = ButtonIcon.Instance().GeneralSearch;
            QueryIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
            ImportDataIcon = ButtonIcon.Instance().ImportDataIcon;
            ImportDataIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
            ExportDataIcon = ButtonIcon.Instance().ExportDataIcon;
            ExportDataIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
            ClearDataIcon = ButtonIcon.Instance().ClearDataIcon;
            ClearDataIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
            #endregion

            DownloadedItemLoading = new GifImage(Properties.Resources.loading);
            DownloadedItemLoading.StartAnimate();
            DownloadedItemLoadingVisibility = Visibility.Collapsed;

            // 初始化DownloadedList
            recordCountDownloaded = downloadStorageService.GetRecordCount();
            DownloadedList = new ObservableCollection<DownloadedItem>();
            UpdateDownloadedItemList(1);
            lastTimeSortSelected = FinishedSortBy;
            Pager = new CustomSimplePagerViewModel(recordCountDownloaded);
            Pager.CurrentChanged += OnCurrentChanged_Pager;
            Pager.ItemsPerPageChanged += OnItemsPerPageChanged_Pager;
            Pager.CurrentPage = 1;

            DownloadedList.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    UpdateEventListeners();
                    SetDialogService();
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    SetRecordCountDownloaded();
                }
            });

            foreach (var downloadedItem in DownloadedList)
            {
                downloadedItem.DownloadedRemove += DownloadedList_DownloadedRemove;
            }

            SetDialogService();
        }

        #endregion

        #region 命令申明
        /// <summary>
        /// 视频下载完成后的处理    视频下载完成弹窗事件的处理函数
        /// (只有当前页面的item数少于每页显示的条数时才会添加至DownloadedList中)
        /// </summary>
        /// <param name="e"></param>
        private void DownloadFinishedHandler(DownloadFinishedEventArgs e)
        {
            // 处理下载完成事件
            // 这里可以更新 ViewModel 中的数据或执行其他操作
            if (e.DownloadedItem is DownloadedItem downloadedItem)
            {
                if (DownloadedList.Count < Pager.ItemsPerPage)
                {
                    downloadedItem.Downloaded = downloadStorageService.GetDownloaded(downloadedItem.DownloadBase.Uuid);
                    downloadedItem.DownloadedRemove += DownloadedList_DownloadedRemove;
                    DownloadedList.Add(downloadedItem);
                }
                recordCountDownloaded += 1;
                Pager.RecordCount = recordCountDownloaded;
            }
        }

        #region 弃用的刷新

        private DelegateCommand refreshDownloadFinishedList;
        /// <summary>
        /// 刷新下载完成列表
        /// </summary>
        public DelegateCommand RefreDownloadFinishedList => refreshDownloadFinishedList ?? (refreshDownloadFinishedList = new DelegateCommand(ExecuteRefreshDownloadFinishedList));
        private void ExecuteRefreshDownloadFinishedList()
        {
            Task.Run(() =>
            {
                ExecuteFinishedSortCommand(lastTimeSortSelected);
                PublishTipHandler(DictionaryResource.GetString("RefreshCompletedTip"));
            }); 
        }
        #endregion
        // 两次选则相同的排序 则逆序   目前为当页逆序，非整个数据库中的逆序
        private DelegateCommand<object> reverseSortCommand;
        public DelegateCommand<object> ReverseSortCommand => reverseSortCommand ?? (reverseSortCommand = new DelegateCommand<object>(ExecuteReverseSortCommand));

        /// <summary>
        /// 两次选择相同的排序 则逆序
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteReverseSortCommand(object parameter)
        {
            if (!(parameter is int index)) { return; }

            if (index == lastTimeSortSelected)
            {
                if (SettingsManager.GetInstance().GetSortOrder() == SortOrder.DESCENDING)
                    SettingsManager.GetInstance().SetSortOrder(SortOrder.ASCENDING);
                else
                    SettingsManager.GetInstance().SetSortOrder(SortOrder.DESCENDING);
                DownloadedList = new ObservableCollection<DownloadedItem>();
                UpdateDownloadedItemList(Pager.CurrentPage);
            }
        }

        // 下载完成列表排序事件
        private DelegateCommand<object> finishedSortCommand;
        public DelegateCommand<object> FinishedSortCommand => finishedSortCommand ?? (finishedSortCommand = new DelegateCommand<object>(ExecuteFinishedSortCommand));

        /// <summary>
        /// 下载完成列表排序事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteFinishedSortCommand(object parameter)
        {
            if (!(parameter is int index)) { return; }
            #region 弃用的排序
            //switch (index)
            //{
            //    case 1:
            //        SortallDownloadedList(DownloadFinishedSort.UPZHUID);
            //        // 更新设置
            //        SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.UPZHUID);
            //        break;
            //    case 2:
            //        SortallDownloadedList(DownloadFinishedSort.FILESIZE);
            //        // 更新设置
            //        SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.FILESIZE);
            //        break;
            //    case 3:
            //        SortallDownloadedList(DownloadFinishedSort.VIDEODURATION);
            //        // 更新设置
            //        SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.VIDEODURATION);
            //        break;
            //    case 4:
            //        SortallDownloadedList(DownloadFinishedSort.ZONEID);
            //        // 更新设置
            //        SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.ZONEID);
            //        break;
            //    case 5:
            //        SortallDownloadedList(DownloadFinishedSort.NUMBER);
            //        // 更新设置
            //        SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.NUMBER);
            //        break;
            //    default:
            //        SortallDownloadedList(DownloadFinishedSort.DOWNLOAD);
            //        // 更新设置
            //        SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.DOWNLOAD);
            //        break;
            //}
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    DownloadedList.Clear();
            //    DownloadedList.AddRange(newData);
            //});
            #endregion
            switch (index)
            {
                case 1:
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.UPZHUID);
                    break;
                case 2:
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.FILESIZE);
                    break;
                case 3:
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.VIDEODURATION);
                    break;
                case 4:
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.ZONEID);
                    break;
                case 5:
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.NUMBER);
                    break;
                default:
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.DOWNLOAD);
                    break;
            }
            lastTimeSortSelected = index;
            DownloadedList = new ObservableCollection<DownloadedItem>();
            UpdateDownloadedItemList(1);
        }

        // 清空下载完成列表事件
        private DelegateCommand clearAllDownloadedCommand;
        public DelegateCommand ClearAllDownloadedCommand => clearAllDownloadedCommand ?? (clearAllDownloadedCommand = new DelegateCommand(ExecuteClearAllDownloadedCommand));

        /// <summary>
        /// 清空下载完成列表事件
        /// </summary>
        private async void ExecuteClearAllDownloadedCommand()
        {
            AlertService alertService = new AlertService(dialogService);
            ButtonResult result = alertService.ShowWarning(DictionaryResource.GetString("ConfirmDelete"));
            if (result != ButtonResult.OK)
            {
                return;
            }

            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DownloadedList.Clear();
                });
                downloadStorageService.ClearAll();
            });
        }

        // 打包导出数据库及其配置文件
        private DelegateCommand packageDatabaseAndConfigFilesCommand;

        public DelegateCommand PackageDatabaseAndConfigFilesCommand => packageDatabaseAndConfigFilesCommand ?? (packageDatabaseAndConfigFilesCommand = new DelegateCommand(ExecutePackageDatabaseAndConfigFiles));

        /// <summary>
        /// 打包当前环境中的数据库及其配置文件
        /// </summary>
        private async void ExecutePackageDatabaseAndConfigFiles()
        {
            // 选择目标文件夹
            string directory = DialogUtils.SetDownloadDirectory();
            if (directory == "") { return; }

            string[] sourceFolderPaths = new string[] { StorageManager.GetBilibili(), StorageManager.GetConfig(), StorageManager.GetStorage()};
            Task.Run(() => 
            {
                FileHelper.CreateZipArchive(sourceFolderPaths, directory);
            });
        }

        // 载入数据库及其配置文件
        private DelegateCommand loadDatabaseAndConfigFilesCommand;

        public DelegateCommand LoadDatabaseAndConfigFilesCommand => loadDatabaseAndConfigFilesCommand ?? (loadDatabaseAndConfigFilesCommand = new DelegateCommand(ExecuteLoadDatabaseAndConfigFiles));

        /// <summary>
        /// 打包当前环境中的数据库及其配置文件
        /// </summary>
        private async void ExecuteLoadDatabaseAndConfigFiles()
        {
            // 选择目标文件夹
            string directory = DialogUtils.SetDownloadDirectory();
            if (directory == "") { return; }

        }

        // 查询或筛选数据
        private DelegateCommand queryingOrFilteringDataCommand;

        public DelegateCommand QueryingOrFilteringDataCommand => queryingOrFilteringDataCommand ?? (queryingOrFilteringDataCommand = new DelegateCommand(ExecuteQueryingOrFilteringData));

        /// <summary>
        /// 查询或筛选数据
        /// </summary>
        private async void ExecuteQueryingOrFilteringData()
        {
            // 选择目标文件夹
            string directory = DialogUtils.SetDownloadDirectory();
            if (directory == "") { return; }

        }
        #endregion

        #region 功能性函数
        /// <summary>
        /// 移除  下载完成项的监听  操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadedList_DownloadedRemove(object sender, DownloadFinishedEventArgs e)
        {
            DownloadedList.Remove(e.DownloadedItem);
        }

        /// <summary>
        /// 设置记录总数
        /// </summary>
        private void SetRecordCountDownloaded()
        {
            recordCountDownloaded -= 1;
            Pager.RecordCount = recordCountDownloaded;
        }

        /// <summary>
        /// 单页显示条数改变   处理函数
        /// </summary>
        /// <param name="current"></param>
        /// <param name="limitNum"></param>
        private void OnItemsPerPageChanged_Pager(int current, int limitNum)
        {
            UpdateDownloadedItemList(current);
        }

        /// <summary>
        /// 当前页数改变       处理函数
        /// </summary>
        /// <param name="old"></param>
        /// <param name="current"></param>
        /// <param name="limitNum"></param>
        /// <returns></returns>
        private bool OnCurrentChanged_Pager(int old, int current, int limitNum)
        {
            if (old != current)
            {
                UpdateDownloadedItemList(current);
            }
            return true;
        }

        private async Task UpdateDownloadedItemList(int current)
        {
            DownloadedList.Clear();

            DownloadedItemLoadingVisibility = Visibility.Visible;

            // 是否正在获取数据
            // 在所有的退出分支中都需要设为true
            //IsEnabled = false;

            await Task.Run(new Action(() =>
            {
                CancellationToken cancellationToken = tokenSource2.Token;

                //List<DownloadedItem> downloadedLists = downloadStorageService.GetSortPageDownloaded(current);
                List<string> downloadedLists = downloadStorageService.GetSortPageDownloaded1(current);
                if (downloadedLists == null || downloadedLists.Count == 0)
                {
                    return;
                }
                DownloadedItemLoadingVisibility = Visibility.Collapsed;
                GetDownloadedList(downloadedLists, DownloadedList, cancellationToken);
            }), (tokenSource2 = new CancellationTokenSource()).Token);

            //IsEnabled = true;
        }

        public void GetDownloadedList(List<string> downloadedLists, ObservableCollection<DownloadedItem> result, CancellationToken cancellationToken)
        {
            int order = 0;
            foreach (var uuid in downloadedLists)
            {
                order++;

                App.PropertyChangeAsync(new Action(() =>
                {
                    if (!result.ToList().Exists(t => t.DownloadBase.Uuid == uuid))
                    {
                        result.Add(downloadStorageService.GetDownloadedItem(uuid));
                        Thread.Sleep(10);
                    }
                }));

                // 判断是否该结束线程，若为true，跳出循环
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
        public void GetDownloadedList(List<DownloadedItem> downloadedLists, ObservableCollection<DownloadedItem> result, CancellationToken cancellationToken)
        {
            int order = 0;
            foreach (DownloadedItem downloaded in downloadedLists)
            {
                order++;

                App.PropertyChangeAsync(new Action(() =>
                {
                    if (!result.ToList().Exists(t => t.DownloadBase.Uuid == downloaded.DownloadBase.Uuid))
                    {
                        result.Add(downloaded);
                    }
                }));

                // 判断是否该结束线程，若为true，跳出循环
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 对 DownloadItem发布消息事件的监听
        /// </summary>
        private void UpdateEventListeners()
        {
            foreach (var item in DownloadedList)
            {
                item.PublishTip -= PublishTipHandler;
                item.PublishTip += PublishTipHandler;
            }
        }

        /// <summary>
        /// 设置弹窗服务
        /// </summary>
        private async Task SetDialogService()
        {
            try
            {
                await Task.Run(() =>
                {
                    List<DownloadedItem> list = DownloadedList.ToList();
                    foreach (var item in list)
                    {
                        if (item != null && item.DialogService == null)
                        {
                            item.DialogService = dialogService;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine("SetDialogService()发生异常: {0}", e);
                LogManager.Error($"{Tag}.SetDialogService()", e);
            }
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            SetDialogService();
        }

        /// <summary>
        /// 发送需要显示的tip
        /// </summary>
        /// <param name="isSucceed"></param>
        private void PublishTipHandler(string message)
        {
            eventAggregator.GetEvent<MessageEvent>().Publish(message);
        }

        #endregion
    }
}
