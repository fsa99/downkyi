using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Models;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Models;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows;
using System.Windows.Media;

namespace DownKyi.ViewModels.DownloadManager
{
    public class DownloadBaseItem : BindableBase
    {
        public IDialogService DialogService;

        public DownloadBaseItem()
        {
            DialogService = null;
        }

        public DownloadBaseItem(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        // model数据
        private DownloadBase downloadBase;
        public DownloadBase DownloadBase
        {
            get => downloadBase;
            set
            {
                downloadBase = value;

                if (value != null)
                {
                    ZoneImage = (DrawingImage)Application.Current.Resources[VideoZoneIcon.Instance().GetZoneImageKey(DownloadBase.ZoneId)];
                }
            }
        }

        // 视频分区image
        private DrawingImage zoneImage;
        public DrawingImage ZoneImage
        {
            get => zoneImage;
            set => SetProperty(ref zoneImage, value);
        }

        // 视频序号
        public int Order
        {
            get => DownloadBase == null ? 0 : DownloadBase.Order;
            set
            {
                DownloadBase.Order = value;
                RaisePropertyChanged("Order");
            }
        }

        // 视频主标题
        public string MainTitle
        {
            get => DownloadBase == null ? "" : DownloadBase.MainTitle;
            set
            {
                DownloadBase.MainTitle = value;
                RaisePropertyChanged("MainTitle");
            }
        }

        // 视频标题
        public string Name
        {
            get => DownloadBase == null ? "" : DownloadBase.Name;
            set
            {
                DownloadBase.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        // 时长
        public string Duration
        {
            get => DownloadBase == null ? "" : DownloadBase.Duration;
            set
            {
                DownloadBase.Duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        // 视频编码名称，AVC、HEVC
        public string VideoCodecName
        {
            get => DownloadBase == null ? "" : DownloadBase.VideoCodecName;
            set
            {
                DownloadBase.VideoCodecName = value;
                RaisePropertyChanged("VideoCodecName");
            }
        }

        // 视频画质
        public Quality Resolution
        {
            get => DownloadBase == null ? null : DownloadBase.Resolution;
            set
            {
                DownloadBase.Resolution = value;
                RaisePropertyChanged("Resolution");
            }
        }

        // 音频编码
        public Quality AudioCodec
        {
            get => DownloadBase == null ? null : DownloadBase.AudioCodec;
            set
            {
                DownloadBase.AudioCodec = value;
                RaisePropertyChanged("AudioCodec");
            }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize
        {
            get => DownloadBase == null ? "" : DownloadBase.FileSize;
            set
            {
                DownloadBase.FileSize = value;
                RaisePropertyChanged("FileSize");
            }
        }

        /// <summary>
        /// 视频创建时间
        /// </summary>
        public string CreateTime
        {
            get => DownloadBase == null ? "" : DownloadBase.CreateTime;
            set
            {
                DownloadBase.CreateTime = value;
                RaisePropertyChanged("CreateTime");
            }
        }

        /// <summary>
        /// 播放数量
        /// </summary>
        public string PlayNumber
        {
            get => DownloadBase == null ? "" : DownloadBase.PlayNumber;
            set
            {
                DownloadBase.PlayNumber = value;
                RaisePropertyChanged("PlayNumber");
            }
        }
        /// <summary>
        /// 弹幕数量
        /// </summary>
        public string DanmakuNumber
        {
            get => DownloadBase == null ? "" : DownloadBase.DanmakuNumber;
            set
            {
                DownloadBase.DanmakuNumber = value;
                RaisePropertyChanged("DanmakuNumber");
            }
        }
        /// <summary>
        /// 点赞人数
        /// </summary>
        public string LikeNumber
        {
            get => DownloadBase == null ? "" : DownloadBase.LikeNumber;
            set
            {
                DownloadBase.LikeNumber = value;
                RaisePropertyChanged("LikeNumber");
            }
        }
        /// <summary>
        /// 投币数量
        /// </summary>
        public string CoinNumber
        {
            get => DownloadBase == null ? "" : DownloadBase.CoinNumber;
            set
            {
                DownloadBase.CoinNumber = value;
                RaisePropertyChanged("CoinNumber");
            }
        }
        /// <summary>
        /// 收藏数量
        /// </summary>
        public string FavoriteNumber
        {
            get => DownloadBase == null ? "" : DownloadBase.FavoriteNumber;
            set
            {
                DownloadBase.FavoriteNumber = value;
                RaisePropertyChanged("FavoriteNumber");
            }
        }
        /// <summary>
        /// 分享数量
        /// </summary>
        public string ShareNumber
        {
            get => DownloadBase == null ? "" : DownloadBase.ShareNumber;
            set
            {
                DownloadBase.ShareNumber = value;
                RaisePropertyChanged("ShareNumber");
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get => DownloadBase == null ? "" : DownloadBase.Description;
            set
            {
                DownloadBase.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        /// <summary>
        /// UP主
        /// </summary>
        public VideoOwner UpOwner
        {
            get => DownloadBase == null ? new VideoOwner() : DownloadBase.UpOwner;
            set
            {
                DownloadBase.UpOwner = value;
                RaisePropertyChanged("UpOwner");
            }
        }
    }
}
