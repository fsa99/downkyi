using Newtonsoft.Json;
using System;
using System.IO;

namespace DownKyi.Core.UpupTheme
{
    [Serializable]
    public class UpupModel
    {
        private static readonly string defaultReUrl = "https://www.bilibili.com/video/";
        private static readonly string defaultUrl = "http://source.upupoo.com/theme/";
        private static readonly string defaultFileName = "theme.upup";

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

        /// <summary>
        /// 视频原地址
        /// </summary>
        [JsonProperty("reprintUrl")]
        public string ReprintUrl { get; set; }

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

        /// <summary>
        /// 主题编号 唯一
        /// </summary>
        [JsonProperty("themeno")]
        public int Themeno { get; set; }

        /// <summary>
        /// 配置文件相对网络路径
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

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

        public UpupModel(string videoFileName, int i)
        {
            Name = Path.GetFileNameWithoutExtension(videoFileName);
            Src = videoFileName;
            Themeno = int.Parse(DateTime.Now.ToString("MMddHHmmss")) - i;
            Url = defaultUrl + Themeno.ToString() + "/" + defaultFileName;
            ThemeType = 1;
            Used = 1;
            IsOriginal = 1;
            Ver = "1.0";
        }
        public void SetThemeno(int themeno)
        {
            Themeno = themeno;
            Url = defaultUrl + themeno.ToString() + "/" + defaultFileName;
        }

        public void SetReprintUrl(string Bvid)
        {
            ReprintUrl = defaultReUrl + Bvid;
        }

        /// <summary>
        /// 写成upup文件
        /// </summary>
        public void WriteJsonData(string directory)
        {
            string targetDirectory = Path.Combine(directory, Themeno.ToString());
            Directory.CreateDirectory(Path.GetDirectoryName(targetDirectory));
            string themeupup = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(Path.Combine(targetDirectory, defaultFileName), themeupup);
        }
    }
}

