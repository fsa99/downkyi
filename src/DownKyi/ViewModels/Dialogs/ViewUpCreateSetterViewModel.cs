using DownKyi.Core.Settings;
using DownKyi.Core.Utils;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System.Collections.Generic;

namespace DownKyi.ViewModels.Dialogs
{
    public class ViewUpCreateSetterViewModel : BaseDialogViewModel
    {
        public const string Tag = "ViewUpCreateSetter";
        private readonly IEventAggregator eventAggregator;

        // 历史文件夹的数量
        private readonly int maxDirectoryListCount = 20;

        #region 页面属性申明

        private VectorImage vidiconIcon;
        /// <summary>
        /// 摄像机图标
        /// </summary>
        public VectorImage VidiconIcon
        {
            get { return vidiconIcon; }
            set { SetProperty(ref vidiconIcon, value); }
        }

        private VectorImage pcWallpaperIcon;
        /// <summary>
        /// PC端视频壁纸图标
        /// </summary>
        public VectorImage PCWallpaperIcon
        {
            get { return pcWallpaperIcon; }
            set { SetProperty(ref pcWallpaperIcon, value); }
        }

        private bool isDefaultUpCreateDirectory;
        /// <summary>
        /// 是否设置为默认的Up壁纸生成文件夹
        /// </summary>
        public bool IsDefaultUpCreateDirectory
        {
            get { return isDefaultUpCreateDirectory; }
            set { SetProperty(ref isDefaultUpCreateDirectory, value); }
        }

        private List<string> directoryList;
        /// <summary>
        /// 历史选择过的文件夹
        /// </summary>
        public List<string> DirectoryList
        {
            get { return directoryList; }
            set { SetProperty(ref directoryList, value); }
        }

        private string directory;
        /// <summary>
        /// 现在的文件夹
        /// </summary>
        public string Directory
        {
            get { return directory; }
            set
            {
                SetProperty(ref directory, value);

                if (directory != null && directory != string.Empty)
                {
                    DriveName = directory.Substring(0, 1).ToUpper();
                    DriveNameFreeSpace = Format.FormatFileSize(HardDisk.GetHardDiskFreeSpace(DriveName));
                }
            }
        }

        private string driveName;
        /// <summary>
        /// 驱动器盘符 C盘 D盘
        /// </summary>
        public string DriveName
        {
            get { return driveName; }
            set { SetProperty(ref driveName, value); }
        }

        private string driveNameFreeSpace;
        /// <summary>
        /// 盘符剩余空间
        /// </summary>
        public string DriveNameFreeSpace
        {
            get { return driveNameFreeSpace; }
            set { SetProperty(ref driveNameFreeSpace, value); }
        }

        #endregion

        public ViewUpCreateSetterViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            #region 属性初始化

            VidiconIcon = NormalIcon.Instance().Vidicon;
            VidiconIcon.Fill = DictionaryResource.GetColor("ColorPrimary");
            PCWallpaperIcon = NormalIcon.Instance().PlatformPC;
            PCWallpaperIcon.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 历史下载目录
            DirectoryList = SettingsManager.GetInstance().GetHistoryUpupRootPaths();
            string directory = SettingsManager.GetInstance().GetSaveUpupRootPath();
            if (!DirectoryList.Contains(directory))
            {
                ListHelper.InsertUnique(DirectoryList, directory, 0);
            }
            Directory = directory;

            // 是否使用默认生成目录
            IsDefaultUpCreateDirectory = SettingsManager.GetInstance().GetIsUseSaveUpupRootPath() == AllowStatus.YES;
            #endregion
        }
        #region 命令申明

        // 浏览文件夹事件
        private DelegateCommand browseCommand;
        public DelegateCommand BrowseCommand => browseCommand ?? (browseCommand = new DelegateCommand(ExecuteBrowseCommand));

        /// <summary>
        /// 浏览文件夹事件
        /// </summary>
        private void ExecuteBrowseCommand()
        {
            string directory = SetDirectory();

            if (directory == null)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("WarningNullDirectory"));
                Directory = string.Empty;
            }
            else
            {
                ListHelper.InsertUnique(DirectoryList, directory, 0);
                Directory = directory;

                if (DirectoryList.Count > maxDirectoryListCount)
                {
                    DirectoryList.RemoveAt(maxDirectoryListCount);
                }
            }
        }

        
        // 确认生成事件
        private DelegateCommand upupCreateCommand;
        public DelegateCommand UpupCreateCommand => upupCreateCommand ?? (upupCreateCommand = new DelegateCommand(ExecuteUpupCreateCommand));

        /// <summary>
        /// 确认生成事件
        /// </summary>
        private void ExecuteUpupCreateCommand()
        {
            if (Directory == null || Directory == string.Empty)
            {
                return;
            }

            // 设此文件夹为默认下载文件夹
            if (IsDefaultUpCreateDirectory)
            {
                SettingsManager.GetInstance().SetIsUseSaveUpupRootPath(AllowStatus.YES);
            }
            else
            {
                SettingsManager.GetInstance().SetIsUseSaveUpupRootPath(AllowStatus.NO);
            }

            // 将Directory移动到第一项
            // 如果直接在ComboBox中选择的就需要
            // 否则选中项不会在下次出现在第一项
            ListHelper.InsertUnique(DirectoryList, Directory, 0);

            // 将更新后的DirectoryList写入历史中
            SettingsManager.GetInstance().SetSaveUpupRootPath(Directory);
            SettingsManager.GetInstance().SetHistoryVUpupRootPaths(DirectoryList);

            // 返回数据
            ButtonResult result = ButtonResult.OK;
            IDialogParameters parameters = new DialogParameters
            {
                { "directory", Directory },
                
            };
            RaiseRequestClose(new DialogResult(result, parameters));
        }

        #endregion

        /// <summary>
        /// 设置生成路径
        /// </summary>
        /// <returns></returns>
        private string SetDirectory()
        {
            // 下载目录
            string path;

            // 弹出选择生成目录的窗口
            path = DialogUtils.SetDownloadDirectory();
            if (path == null || path == string.Empty)
            {
                return null;
            }

            return path;
        }

    }
}
