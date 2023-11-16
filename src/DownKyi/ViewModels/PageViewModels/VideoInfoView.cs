using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    /// <summary>
    /// 视频信息
    /// </summary>
    public class VideoInfoView : BindableBase
    {
        /// <summary>
        /// 封面地址
        /// </summary>
        public string CoverUrl { get; set; }
        /// <summary>
        /// UP主 id
        /// </summary>
        public long UpperMid { get; set; }
        /// <summary>
        /// 视频类型id
        /// </summary>
        public int TypeId { get; set; }

        private BitmapImage cover;
        /// <summary>
        /// 封面
        /// </summary>
        public BitmapImage Cover
        {
            get => cover;
            set => SetProperty(ref cover, value);
        }

        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string videoZone;
        /// <summary>
        /// 视频分类
        /// </summary>
        public string VideoZone
        {
            get => videoZone;
            set => SetProperty(ref videoZone, value);
        }

        private string createTime;
        /// <summary>
        /// 视频上传时间
        /// </summary>
        public string CreateTime
        {
            get => createTime;
            set => SetProperty(ref createTime, value);
        }

        private string playNumber;
        /// <summary>
        /// 播放数量
        /// </summary>
        public string PlayNumber
        {
            get => playNumber;
            set => SetProperty(ref playNumber, value);
        }

        private string danmakuNumber;
        /// <summary>
        /// 弹幕数量
        /// </summary>
        public string DanmakuNumber
        {
            get => danmakuNumber;
            set => SetProperty(ref danmakuNumber, value);
        }

        private string likeNumber;
        /// <summary>
        /// 喜欢/点赞数量
        /// </summary>
        public string LikeNumber
        {
            get => likeNumber;
            set => SetProperty(ref likeNumber, value);
        }

        private string coinNumber;
        /// <summary>
        /// 投币数量
        /// </summary>
        public string CoinNumber
        {
            get => coinNumber;
            set => SetProperty(ref coinNumber, value);
        }

        private string favoriteNumber;
        /// <summary>
        /// 收藏数量
        /// </summary>
        public string FavoriteNumber
        {
            get => favoriteNumber;
            set => SetProperty(ref favoriteNumber, value);
        }

        private string shareNumber;
        /// <summary>
        /// 分享数量
        /// </summary>
        public string ShareNumber
        {
            get => shareNumber;
            set => SetProperty(ref shareNumber, value);
        }

        private string replyNumber;
        public string ReplyNumber
        {
            get => replyNumber;
            set => SetProperty(ref replyNumber, value);
        }

        private string description;
        /// <summary>
        /// 视频描述
        /// </summary>
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private string upName;
        /// <summary>
        /// Up主名字
        /// </summary>
        public string UpName
        {
            get => upName;
            set => SetProperty(ref upName, value);
        }

        private BitmapImage upHeader;
        /// <summary>
        /// Up主头像
        /// </summary>
        public BitmapImage UpHeader
        {
            get => upHeader;
            set => SetProperty(ref upHeader, value);
        }
    }
}
