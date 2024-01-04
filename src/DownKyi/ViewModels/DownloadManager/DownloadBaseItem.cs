using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Models;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Core.Logging;
using DownKyi.Core.Storage;
using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private DownloadBase downloadBase;
        /// <summary>
        /// 下载的model数据
        /// </summary>
        public DownloadBase DownloadBase
        {
            get => downloadBase;
            set
            {
                downloadBase = value;

                if (value != null)
                {
                    // B站 投币的图标
                    CoinIcon = NormalIcon.Instance().CoinIcon;
                    CoinIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
                    PlayIcon = NormalIcon.Instance().Play;
                    PlayIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
                    LikeIcon = NormalIcon.Instance().Like;
                    LikeIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
                    FavoriteIcon = NormalIcon.Instance().Favorite;
                    FavoriteIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
                    UpzhuIconIcon = NormalIcon.Instance().UpzhuIcon;
                    UpzhuIconIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
                    ZoneImage = (DrawingImage)Application.Current.Resources[VideoZoneIcon.Instance().GetZoneImageKey(DownloadBase.ZoneId)];
                    StorageCover storageCover = new StorageCover();
                    VideoCoverImage = storageCover.GetCoverThumbnail(DownloadBase.Avid, DownloadBase.Bvid, DownloadBase.Cid, DownloadBase.CoverUrl, 112, 70);
                    if (value.UpOwner != null)
                    {
                        StorageHeader storageHeader = new StorageHeader();
                        UPHeaderFaceImage = storageHeader.GetHeaderThumbnail(DownloadBase.UpOwner.Mid, DownloadBase.UpOwner.Name, DownloadBase.UpOwner.Face, 33, 33);
                    }
                    if (DownloadBase.Dimension != null)
                    {
                        if (DownloadBase.Dimension.Height > DownloadBase.Dimension.Width) { VerticalOrHorizontalIcon = NormalIcon.Instance().VerticalScreen; }
                        else { VerticalOrHorizontalIcon = NormalIcon.Instance().HorizontalScreen; }
                    }
                    else { VerticalOrHorizontalIcon = NormalIcon.Instance().UnKnownIcon; }
                    VerticalOrHorizontalIcon.Fill = DictionaryResource.GetColor("ColorBackgroundGrey");
                }
            }
        }

        #region 图片或图标
        private BitmapImage videoCoverImage;
        /// <summary>
        /// 视频封面image
        /// </summary>
        public BitmapImage VideoCoverImage
        {
            get => videoCoverImage;
            set => SetProperty(ref videoCoverImage, value);
        }

        private BitmapImage upHeaderFaceImage;
        /// <summary>
        /// UP主头像image
        /// </summary>
        public BitmapImage UPHeaderFaceImage
        {
            get => upHeaderFaceImage;
            set => SetProperty(ref upHeaderFaceImage, value);
        }

        private DrawingImage zoneImage;
        /// <summary>
        /// 视频分区image
        /// </summary>
        public DrawingImage ZoneImage
        {
            get => zoneImage;
            set => SetProperty(ref zoneImage, value);
        }
        private VectorImage verticalOrHorizontalIcon;
        /// <summary>
        /// 横屏或竖屏  图标
        /// </summary>
        public VectorImage VerticalOrHorizontalIcon
        {
            get => verticalOrHorizontalIcon;
            set => SetProperty(ref verticalOrHorizontalIcon, value);
        }
        private VectorImage coinIcon;
        /// <summary>
        /// 投币 币 图
        /// </summary>
        public VectorImage CoinIcon
        {
            get => coinIcon;
            set => SetProperty(ref coinIcon, value);
        }
        private VectorImage playIcon;
        /// <summary>
        /// 播放 图标
        /// </summary>
        public VectorImage PlayIcon
        {
            get => playIcon;
            set => SetProperty(ref playIcon, value);
        }
        private VectorImage likeIcon;
        /// <summary>
        /// 点赞 图标
        /// </summary>
        public VectorImage LikeIcon
        {
            get => likeIcon;
            set => SetProperty(ref likeIcon, value);
        }
        private VectorImage favoriteIcon;
        /// <summary>
        /// 收藏 图标
        /// </summary>
        public VectorImage FavoriteIcon
        {
            get => favoriteIcon;
            set => SetProperty(ref favoriteIcon, value);
        }
        private VectorImage upzhuIconIcon;
        /// <summary>
        /// UP 图标
        /// </summary>
        public VectorImage UpzhuIconIcon
        {
            get => upzhuIconIcon;
            set => SetProperty(ref upzhuIconIcon, value);
        }
        #endregion

        #region 视频 文件信息编码画质等
        /// <summary>
        /// 视频序号
        /// </summary>
        public int Order
        {
            get => DownloadBase == null ? 0 : DownloadBase.Order;
            set
            {
                DownloadBase.Order = value;
                RaisePropertyChanged("Order");
            }
        }

        /// <summary>
        /// 视频主标题
        /// </summary>
        public string MainTitle
        {
            get
            {
                if (DownloadBase == null)
                {
                    return string.Empty;
                }
                else
                {
                    if (DownloadBase.MainTitle == DownloadBase.Name)
                    {
                        return DownloadBase.MainTitle;
                    }
                    else
                    {
                        return DownloadBase.MainTitle + DownloadBase.Name;
                    }
                }
            }
            set
            {
                DownloadBase.MainTitle = value;
                RaisePropertyChanged("MainTitle");
            }
        }

        /// <summary>
        /// 视频标题
        /// </summary>
        public string Name
        {
            get => DownloadBase == null ? "" : DownloadBase.Name;
            set
            {
                DownloadBase.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// 时长
        /// </summary>
        public string Duration
        {
            get => DownloadBase == null ? "" : DownloadBase.Duration;
            set
            {
                DownloadBase.Duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        /// <summary>
        /// 视频编码名称，AVC、HEVC
        /// </summary>
        public string VideoCodecName
        {
            get => DownloadBase == null ? "" : DownloadBase.VideoCodecName;
            set
            {
                DownloadBase.VideoCodecName = value;
                RaisePropertyChanged("VideoCodecName");
            }
        }

        /// <summary>
        /// 视频画质
        /// </summary>
        public Quality Resolution
        {
            get => DownloadBase?.Resolution;
            set
            {
                DownloadBase.Resolution = value;
                RaisePropertyChanged("Resolution");
            }
        }

        /// <summary>
        /// 音频编码
        /// </summary>
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
                LogManager.Info("设置FileSize", $"设置FileSize为：{value}");
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
        /// 视屏宽度 高度 角度
        /// </summary>
        public Dimension Dimension
        {
            get => DownloadBase == null ? new Dimension() : DownloadBase.Dimension;
            set
            {
                DownloadBase.Dimension = value;
                RaisePropertyChanged("Dimension");
            }
        }
        #endregion

        #region 视频播放 收藏等
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
        #endregion

        #region 其他信息 UP主 分区
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

        /// <summary>
        /// 视频分区id  以及主分区id和名称
        /// </summary>
        public string ZoneAndParent
        {
            get
            {
                if (DownloadBase == null)
                {
                    return string.Empty;
                }
                else
                {
                    var zones = VideoZone.Instance().GetCurAndParentZoneAttrs(DownloadBase.ZoneId);
                    if (zones.Count > 0)
                    {
                        return string.Join("=>", zones.Select(zone => $"{zone.Id}:{zone.Name}"));
                    }
                    return string.Empty;
                }
            }
            set
            {
                RaisePropertyChanged("ZoneAndParent");
            }
        }
        #endregion
    }
}
