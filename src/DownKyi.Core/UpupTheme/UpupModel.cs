using Newtonsoft.Json;
using System;
using System.IO;

namespace DownKyi.Core.UpupTheme
{
    [Serializable]
    public class UpupModel
    {
        /// <summary>
        /// 使用者 名字
        /// </summary>
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// 是否原创  1:原创 | 2:转载 | 3:二次创作
        /// </summary>
        [JsonProperty("isOriginal")]
        public int IsOriginal { get; set; }
        private string reprintUrl;
        /// <summary>
        /// 视频原地址
        /// </summary>
        [JsonProperty("reprintUrl")]
        public string ReprintUrl
        {
            get => reprintUrl;
            set => reprintUrl = Path.Combine("https://www.bilibili.com/video/", value.ToString());
        }
        /// <summary>
        /// 作者
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 视屏本地地址
        /// </summary>
        [JsonProperty("src")]
        public string Src { get; set; }
        /// <summary>
        /// 标记
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }
        /// <summary>
        /// 主题类型 1:视频 | 3:HTML
        /// </summary>
        [JsonProperty("themeType")]
        public int ThemeType { get; set; }

        private int themeno;
        /// <summary>
        /// 主题编号 唯一
        /// </summary>
        [JsonProperty("themeno")]
        public int Themeno
        {
            get => themeno;
            set
            {
                Url = value.ToString() + "/theme.upup";
                themeno = value;
            }
        }
        private string url;
        /// <summary>
        /// 配置文件相对网络路径
        /// </summary>
        [JsonProperty("url")]
        public string Url
        {
            get => url;
            set => url = "http://source.upupoo.com/theme/" + value;
        }
        /// <summary>
        /// 已使用
        /// </summary>
        [JsonProperty("used")]
        public int Used { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        [JsonProperty("ver")]
        public string Ver { get; set; }

        public UpupModel()
        {
            ThemeType = 1;
            Used = 1;
            IsOriginal = 1;
            Ver = "1.0";
        }
    }
}

