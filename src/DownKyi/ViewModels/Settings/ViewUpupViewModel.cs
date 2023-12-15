using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace DownKyi.ViewModels.Settings
{
    public class ViewUpupViewModel : BaseViewModel
    {
        public const string Tag = "PageSettingsUpup";

        private bool isOnNavigatedTo;

        #region 页面属性申明

        private bool isMoveVideoUpDirectory;
        /// <summary>
        /// 是否移动视频到目标目录
        /// </summary>
        public bool IsMoveVideoUpDirectory
        {
            get => isMoveVideoUpDirectory;
            set => SetProperty(ref isMoveVideoUpDirectory, value);
        }

        private bool isUseDefaultUpDirectory;
        /// <summary>
        /// 是否使用默认生成目录
        /// </summary>
        public bool IsUseDefaultUpDirectory
        {
            get => isUseDefaultUpDirectory;
            set => SetProperty(ref isUseDefaultUpDirectory, value);
        }

        private string defaultUpDirectory;
        /// <summary>
        /// 生成目录
        /// </summary>
        public string DefaultUpDirectory
        {
            get => defaultUpDirectory;
            set => SetProperty(ref defaultUpDirectory, value);
        }

        private string ver;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Ver
        {
            get => ver;
            set => SetProperty(ref ver, value);
        }

        private string defaultUseName;
        /// <summary>
        /// 默认使用者名称
        /// </summary>
        public string DefaultUseName
        {
            get => defaultUseName;
            set => SetProperty(ref defaultUseName, value);
        }

        #endregion

        public ViewUpupViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            #region 属性初始化

            
            #endregion

        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            isOnNavigatedTo = true;
            DefaultUpDirectory = SettingsManager.GetInstance().GetSaveUpupRootPath();
            Ver = SettingsManager.GetInstance().GetVer();
            DefaultUseName = SettingsManager.GetInstance().GetDefaultUseName();
            IsMoveVideoUpDirectory = SettingsManager.GetInstance().GetIsMoveVideoUpDirectory() == AllowStatus.YES;
            IsUseDefaultUpDirectory = SettingsManager.GetInstance().GetIsUseSaveUpupRootPath() == AllowStatus.YES;
            isOnNavigatedTo = false;
        }

        #region 命令申明

        // 是否使用默认生成目录事件
        private DelegateCommand isUseDefaultUpDirectoryCommand;
        public DelegateCommand IsUseDefaultUpDirectoryCommand => isUseDefaultUpDirectoryCommand ?? (isUseDefaultUpDirectoryCommand = new DelegateCommand(ExecuteIsUseDefaultDirectoryCommand));

        /// <summary>
        /// 是否使用默认生成目录事件
        /// </summary>
        private void ExecuteIsUseDefaultDirectoryCommand()
        {
            AllowStatus isUseDefaultDirectory = IsUseDefaultUpDirectory ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().SetIsUseSaveUpupRootPath(isUseDefaultDirectory);
            PublishTip(isSucceed);
        }

        // 是否向生成目录复制视频
        private DelegateCommand isCopyVideoUpDirectoryCommand;
        public DelegateCommand IsCopyVideoUpDirectoryCommand => isCopyVideoUpDirectoryCommand ?? (isCopyVideoUpDirectoryCommand = new DelegateCommand(ExecuteIsCopyVideoUpDirectoryCommand));

        /// <summary>
        /// 是否向生成目录复制视频
        /// </summary>
        private void ExecuteIsCopyVideoUpDirectoryCommand()
        {
            AllowStatus isMoveVideoUpDirectory = IsMoveVideoUpDirectory ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().SetIsMoveVideoUpDirectory(isMoveVideoUpDirectory);
            PublishTip(isSucceed);
        }

        // 修改默认生成目录事件
        private DelegateCommand changeSaveUpupDirectoryCommand;
        public DelegateCommand ChangeSaveUpupDirectoryCommand => changeSaveUpupDirectoryCommand ?? (changeSaveUpupDirectoryCommand = new DelegateCommand(ExecuteChangeSaveUpupDirectoryCommand));

        /// <summary>
        /// 修改默认生成目录事件
        /// </summary>
        private void ExecuteChangeSaveUpupDirectoryCommand()
        {
            string directory = DialogUtils.SetDownloadDirectory();
            if (directory == "") { return; }

            bool isSucceed = SettingsManager.GetInstance().SetSaveUpupRootPath(directory);
            PublishTip(isSucceed);

            if (isSucceed)
            {
                DefaultUpDirectory = directory;
            }
        }

        private DelegateCommand<string> changeVerCommand;
        /// <summary>
        /// 默认版本号修改事件
        /// </summary>
        public DelegateCommand<string> ChangeVerCommand => changeVerCommand ?? (changeVerCommand = new DelegateCommand<string>(ExecuteChangeVerCommand));

        private void ExecuteChangeVerCommand(string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                bool isSucceed = SettingsManager.GetInstance().SetVer(parameter);
                PublishTip(isSucceed);
            }
        }

        private DelegateCommand<string> changeDefaultUseNameCommand;
        /// <summary>
        /// 默认使用者修改事件
        /// </summary>
        public DelegateCommand<string> ChangeDefaultUseNameCommand => changeDefaultUseNameCommand ?? (changeDefaultUseNameCommand = new DelegateCommand<string>(ExecuteChangeDefaultUseNameCommand));

        private void ExecuteChangeDefaultUseNameCommand(string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                bool isSucceed = SettingsManager.GetInstance().SetDefaultUseName(parameter);
                PublishTip(isSucceed);
            }
        }

        #endregion

        /// <summary>
        /// 发送需要显示的tip
        /// </summary>
        /// <param name="isSucceed"></param>
        private void PublishTip(bool isSucceed)
        {
            if (isOnNavigatedTo) { return; }

            if (isSucceed)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingUpdated"));
            }
            else
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingFailed"));
            }
        }

    }
}
