using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    /// <summary>
    /// 视频被喜欢的状态
    /// </summary>
    public class FavStatus : BaseModel
    {
        /// <summary>
        /// 收藏数
        /// </summary>
        [JsonProperty("collect")]
        public long Collect { get; set; }

        /// <summary>
        /// 播放数
        /// </summary>
        [JsonProperty("play")]
        public long Play { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        [JsonProperty("thumb_up")]
        public long ThumbUp { get; set; }

        /// <summary>
        /// 分享数
        /// </summary>
        [JsonProperty("share")]
        public long Share { get; set; }
    }
}
