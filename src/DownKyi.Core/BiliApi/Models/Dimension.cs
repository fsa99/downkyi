using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Models
{
    [System.Serializable]
    public class Dimension : BaseModel
    {
        /// <summary>
        /// 宽度
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// 旋转角度
        /// </summary>
        [JsonProperty("rotate")]
        public int Rotate { get; set; }
    }
}
