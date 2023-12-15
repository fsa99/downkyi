using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Services;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadFinishedViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloadFinished";

        #region 页面属性申明

        private ObservableCollection<DownloadedItem> downloadedList;
        public ObservableCollection<DownloadedItem> DownloadedList
        {
            get => downloadedList;
            set
            {
                SetProperty(ref downloadedList, value);
                UpdateEventListeners();
            }
        }

        private int finishedSortBy;
        public int FinishedSortBy
        {
            get => finishedSortBy;
            set => SetProperty(ref finishedSortBy, value);
        }
        private void UpdateEventListeners()
        {
            // 移除之前的监听
            foreach (var item in DownloadedList)    
            {
                item.PublishTip -= PublishTipHandler;
            }

            // 添加新的监听
            foreach (var item in DownloadedList)
            {
                item.PublishTip += PublishTipHandler;
            }
        }
        /// <summary>
        /// 上次选择的排序方式
        /// </summary>
        private int lastTimeSortSelected;
        #endregion

        public ViewDownloadFinishedViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            // 初始化DownloadedList
            DownloadedList = App.DownloadedList;
            DownloadedList.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    SetDialogService();
                }
            });
            SetDialogService();

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
            lastTimeSortSelected = FinishedSortBy;
            App.SortDownloadedList(finishedSort);
        }

        #region 命令申明
        // 两次选则相同的排序 则逆序
        private DelegateCommand<object> reverseSortCommand;
        public DelegateCommand<object> ReverseSortCommand => reverseSortCommand ?? (reverseSortCommand = new DelegateCommand<object>(ExecuteReverseSortCommand));

        /// <summary>
        /// 两次选则相同的排序 则逆序
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteReverseSortCommand(object parameter)
        {
            if (!(parameter is int index)) { return; }

            if (index == lastTimeSortSelected)
            {
                DownloadedList = new ObservableCollection<DownloadedItem>(DownloadedList.Reverse());
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

            switch (index)
            {
                case 0:
                    App.SortDownloadedList(DownloadFinishedSort.DOWNLOAD);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.DOWNLOAD);
                    break;
                case 1:
                    App.SortDownloadedList(DownloadFinishedSort.UPZHUID);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.UPZHUID);
                    break;
                case 2:
                    App.SortDownloadedList(DownloadFinishedSort.FILESIZE);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.FILESIZE);
                    break;
                case 3:
                    App.SortDownloadedList(DownloadFinishedSort.VIDEODURATION);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.VIDEODURATION);
                    break;
                case 4:
                    App.SortDownloadedList(DownloadFinishedSort.ZONEID);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.ZONEID);
                    break;
                case 5:
                    App.SortDownloadedList(DownloadFinishedSort.NUMBER);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.NUMBER);
                    break;
                default:
                    App.SortDownloadedList(DownloadFinishedSort.DOWNLOAD);
                    // 更新设置
                    SettingsManager.GetInstance().SetDownloadFinishedSort(DownloadFinishedSort.DOWNLOAD);
                    break;
            }
            lastTimeSortSelected = index;
            DownloadedList = App.DownloadedList;
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

            // 使用Clear()不能触发NotifyCollectionChangedAction.Remove事件
            // 因此遍历删除
            // DownloadingList中元素被删除后不能继续遍历
            await Task.Run(() =>
            {
                List<DownloadedItem> list = DownloadedList.ToList();
                foreach (DownloadedItem item in list)
                {
                    App.PropertyChangeAsync(new Action(() =>
                    {
                        App.DownloadedList.Remove(item);
                    }));
                }
            });
        }

        #endregion

        private async void SetDialogService()
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
    }
}
