using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Models
{
    /// <summary>
    /// 视频的作者
    /// </summary>
    public class VideoOwner : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [JsonProperty("face")]
        public string Face { get; set; }
    }
}
