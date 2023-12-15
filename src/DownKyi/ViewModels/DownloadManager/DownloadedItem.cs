using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Core.UpupTheme;
using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Services;
using DownKyi.Utils;
using DownKyi.ViewModels.Dialogs;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DownKyi.ViewModels.DownloadManager
{
    public class DownloadedItem : DownloadBaseItem
    {
        public DownloadedItem() : this(null)
        {
        }

        public event Action<string> PublishTip;

        public DownloadedItem(IDialogService dialogService) : base(dialogService)
        {
            // 打开文件夹按钮
            OpenFolder = ButtonIcon.Instance().Folder;
            OpenFolder.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 打开视频按钮
            OpenVideo = ButtonIcon.Instance().Start;
            OpenVideo.Fill = DictionaryResource.GetColor("ColorPrimary");

            // UpUp资源按钮
            UpupTheme = ButtonIcon.Instance().UpTheme;
            UpupTheme.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 删除按钮
            RemoveVideo = ButtonIcon.Instance().Trash;
            RemoveVideo.Fill = DictionaryResource.GetColor("ColorWarning");
        }

        // model数据
        public Downloaded Downloaded { get; set; }

        //  下载速度
        public string MaxSpeedDisplay
        {
            get => Downloaded.MaxSpeedDisplay;
            set
            {
                Downloaded.MaxSpeedDisplay = value;
                RaisePropertyChanged("MaxSpeedDisplay");
            }
        }

        // 完成时间
        public string FinishedTime
        {
            get => Downloaded.FinishedTime;
            set
            {
                Downloaded.FinishedTime = value;
                RaisePropertyChanged("FinishedTime");
            }
        }

        #region 控制按钮 

        private VectorImage openFolder;
        public VectorImage OpenFolder
        {
            get => openFolder;
            set => SetProperty(ref openFolder, value);
        }

        private VectorImage openVideo;
        public VectorImage OpenVideo
        {
            get => openVideo;
            set => SetProperty(ref openVideo, value);
        }

        private VectorImage upupTheme;

        public VectorImage UpupTheme
        {
            get => upupTheme;
            set => SetProperty(ref upupTheme, value);
        }

        private VectorImage removeVideo;
        public VectorImage RemoveVideo
        {
            get => removeVideo;
            set => SetProperty(ref removeVideo, value);
        }

        #endregion

        #region 命令申明

        // 打开文件夹事件
        private DelegateCommand openFolderCommand;
        public DelegateCommand OpenFolderCommand => openFolderCommand ?? (openFolderCommand = new DelegateCommand(ExecuteOpenFolderCommand));

        /// <summary>
        /// 打开文件夹事件
        /// </summary>
        private void ExecuteOpenFolderCommand()
        {
            if (DownloadBase == null) { return; }
            //TODO:这里不光有mp4视频文件，也可能存在音频文件、字幕，或者其他文件类型
            //fix bug:Issues #709
            //这里根据需要下载的类型判断，具体对应的文件后缀名
            var downLoadContents = DownloadBase.NeedDownloadContent.Where(e => e.Value == true).Select(e => e.Key);
            string fileSuffix = string.Empty;
            if (downLoadContents.Contains("downloadVideo"))
            {
                fileSuffix = ".mp4";
            }
            else if (downLoadContents.Contains("downloadAudio"))
            {
                fileSuffix = ".aac";
            }
            else if (downLoadContents.Contains("downloadCover"))
            {
                fileSuffix = ".jpg";
            }
            string videoPath = $"{DownloadBase.FilePath}{fileSuffix}";
            FileInfo fileInfo = new FileInfo(videoPath);
            if (File.Exists(fileInfo.FullName))
            {
                System.Diagnostics.Process.Start("Explorer", "/select," + fileInfo.FullName);
            }
            else
            {
                //eventAggregator.GetEvent<MessageEvent>().Publish("没有找到视频文件，可能被删除或移动！");
            }
        }

        // 打开视频事件
        private DelegateCommand openVideoCommand;
        public DelegateCommand OpenVideoCommand => openVideoCommand ?? (openVideoCommand = new DelegateCommand(ExecuteOpenVideoCommand));

        /// <summary>
        /// 打开视频事件
        /// </summary>
        private void ExecuteOpenVideoCommand()
        {
            if (DownloadBase == null) { return; }

            string videoPath = $"{DownloadBase.FilePath}.mp4";
            var fileInfo = new FileInfo(videoPath);
            if (File.Exists(fileInfo.FullName))
            {
                System.Diagnostics.Process.Start(fileInfo.FullName);
            }
            else
            {
                //eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipAddDownloadingZero"));
                //eventAggregator.GetEvent<MessageEvent>().Publish("没有找到视频文件，可能被删除或移动！");
            }
        }

        // 删除事件
        private DelegateCommand removeVideoCommand;
        public DelegateCommand RemoveVideoCommand => removeVideoCommand ?? (removeVideoCommand = new DelegateCommand(ExecuteRemoveVideoCommand));

        /// <summary>
        /// 删除事件
        /// </summary>
        private void ExecuteRemoveVideoCommand()
        {
            AlertService alertService = new AlertService(DialogService);
            ButtonResult result = alertService.ShowWarning(DictionaryResource.GetString("ConfirmDelete"), 2);
            if (result != ButtonResult.OK)
            {
                return;
            }

            App.DownloadedList.Remove(this);
        }

        // 生成UPUPoo资源事件
        private DelegateCommand createUpupTheme;
        public DelegateCommand CreateUpupTheme => createUpupTheme ?? (createUpupTheme = new DelegateCommand(ExecuteCreateUpupThemeCommand));

        private void ExecuteCreateUpupThemeCommand()
        {
            if (DownloadBase == null) { return; }
            // 选择文件夹
            string directory = SetDirectory(DialogService);
            if (string.IsNullOrEmpty(directory))
            {

            }
            UpupModel upupModel = new UpupModel()
            {
                UserName = SettingsManager.GetInstance().GetDefaultUseName(),
                Author = DownloadBase.UpOwner.Name,
                Name = DownloadBase.Name,
                Src = DownloadBase.FilePath + ".mp4",
                Themeno = (int)DownloadBase.Avid,
                Tag = DownloadBase.ZoneId.ToString(),
                Description = DownloadBase.Description,
                ReprintUrl = DownloadBase.Bvid,
            };
            string targetDirectory = Path.Combine(directory, upupModel.Themeno.ToString());
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            string sourceIMGFileName = DownloadBase.FilePath + ".jpg";
            string targetIMGFileName = Path.Combine(targetDirectory, "preview.jpg");
            string sourceVideoFileName = DownloadBase.FilePath + ".mp4";
            string targetVideoFileName = Path.Combine(targetDirectory, Path.GetFileName(sourceVideoFileName));
            // 判断是否下载了视频
            if (DownloadBase.NeedDownloadContent.ContainsKey("downloadVideo") && DownloadBase.NeedDownloadContent["downloadVideo"])
            {
                if (!File.Exists(sourceVideoFileName))
                {
                    // 视频文件不存在或被删除
                    return;
                }
                //判断视频是复制到目标目录  还是放在源  Src要做对应改变
                if (SettingsManager.GetInstance().GetIsMoveVideoUpDirectory() == AllowStatus.YES)
                {
                    System.Threading.Tasks.Task.Run(() => File.Copy(sourceVideoFileName, targetVideoFileName, true));
                    upupModel.Src = targetVideoFileName;
                }
                // 判断有没有下载封面
                #region 封面的处理
                if (DownloadBase.NeedDownloadContent.ContainsKey("downloadCover") && DownloadBase.NeedDownloadContent["downloadCover"])
                {
                    
                    if (File.Exists(sourceIMGFileName))
                    {
                        File.Move(sourceIMGFileName, targetIMGFileName);
                    }
                    else
                    {
                        // 查询、保存封面
                        StorageCover storageCover = new StorageCover();
                        string cover = storageCover.GetCover(DownloadBase.Avid, DownloadBase.Bvid, DownloadBase.Cid, DownloadBase.CoverUrl);
                        if (cover == null)
                        {
                            // 本地不存在且下载失败
                        }
                        using (Bitmap bitmap = new Bitmap(cover))
                        {
                            bitmap.Save(targetIMGFileName);
                        }
                    }
                }
                else
                {
                    // 查询、保存封面
                    StorageCover storageCover = new StorageCover();
                    string cover = storageCover.GetCover(DownloadBase.Avid, DownloadBase.Bvid, DownloadBase.Cid, DownloadBase.CoverUrl);
                    if (cover == null)
                    {
                        // 本地不存在且下载失败
                    }
                    using (Bitmap bitmap = new Bitmap(cover))
                    {
                        bitmap.Save(targetIMGFileName);
                    }
                }
                #endregion
                string themeupup = JsonConvert.SerializeObject(upupModel, Formatting.Indented);
                File.WriteAllText(Path.Combine(targetDirectory, "theme.upup"), themeupup);
                PublishTip?.Invoke("成功生成了upup资源");

            }
            else
            {
                // 没有下载视频
                PublishTip?.Invoke("该完成项没有下载视频");
            }
        }
        #endregion
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="dialogService"></param>
        public string SetDirectory(IDialogService dialogService)
        {
            // 选择的下载文件夹
            string directory = string.Empty;

            // 是否使用默认下载目录
            if (SettingsManager.GetInstance().GetIsUseSaveUpupRootPath() == AllowStatus.YES)
            {
                directory = SettingsManager.GetInstance().GetSaveUpupRootPath();
            }
            else
            {
                // 打开文件夹选择器
                dialogService.ShowDialog(ViewUpCreateSetterViewModel.Tag, null, result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        // 选择的下载文件夹
                        directory = result.Parameters.GetValue<string>("directory");
                    }
                });
            }
            if (string.IsNullOrEmpty(directory))
            {
                return null;
            }
            if (!Directory.Exists(Directory.GetDirectoryRoot(directory)))
            {
                var alert = new AlertService(dialogService);
                alert.ShowError(DictionaryResource.GetString("DriveNotFound"));

                directory = string.Empty;
            }

            // 下载设置dialog中如果点击取消或者关闭窗口，
            // 会返回空字符串，
            // 这时直接退出
            if (directory == null || directory == string.Empty) { return null; }

            // 文件夹不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
