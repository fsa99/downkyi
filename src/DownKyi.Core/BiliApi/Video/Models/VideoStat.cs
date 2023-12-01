using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class VideoStat : BaseModel
    {
        /// <summary>
        /// 视频id
        /// </summary>
        [JsonProperty("aid")]
        public long Aid { get; set; }
        /// <summary>
        /// 播放数量
        /// </summary>
        [JsonProperty("view")]
        public long View { get; set; }
        /// <summary>
        /// 弹幕数量
        /// </summary>
        [JsonProperty("danmaku")]
        public long Danmaku { get; set; }
        /// <summary>
        /// 重播数量
        /// </summary>
        [JsonProperty("reply")]
        public long Reply { get; set; }
        /// <summary>
        /// 收藏数
        /// </summary>
        [JsonProperty("favorite")]
        public long Favorite { get; set; }
        /// <summary>
        /// 投币数
        /// </summary>
        [JsonProperty("coin")]
        public long Coin { get; set; }
        /// <summary>
        /// 分享数
        /// </summary>
        [JsonProperty("share")]
        public long Share { get; set; }
        /// <summary>
        /// 现在排名
        /// </summary>
        [JsonProperty("now_rank")]
        public long NowRank { get; set; }
        /// <summary>
        /// 历史最高排名
        /// </summary>
        [JsonProperty("his_rank")]
        public long HisRank { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        [JsonProperty("like")]
        public long Like { get; set; }
        /// <summary>
        /// 不喜欢数
        /// </summary>
        [JsonProperty("dislike")]
        public long Dislike { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        [JsonProperty("evaluation")]
        public string Evaluation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("argue_msg")]
        public string ArgueMsg { get; set; }
    }
}
