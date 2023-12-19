using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DownKyi.Core.UpupTheme
{
    public class UpupWallpaper
    {
        private static readonly string defaultFileName = "upWallpaper.json";
        [JsonIgnore]
        private string JsonFilePath { get; set; }

        private List<UpupModel> _upupData;
        [JsonProperty("data")]
        public List<UpupModel> UpupData
        {
            get => _upupData;
            set => _upupData = value;
        }

        [JsonProperty("total")]
        private int Total 
        {
            get => _upupData.Count; 
        }

        private void UpupDataAdd(UpupModel upup)
        {
            if (!UpupData.Any(item => item.Themeno == upup.Themeno))
            {
                UpupData.Add(upup);
                WriteJsonData();
            }
        }

        public UpupWallpaper(string directory, UpupModel upup)
        {
            string filePath = Path.Combine(directory, defaultFileName);
            JsonFilePath = filePath;
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                JsonConvert.PopulateObject(jsonContent, this);
            }
            if (UpupData == null)
            {
                UpupData = new List<UpupModel>();
            }
            UpupDataAdd(upup);
        }
        /// <summary>
        /// 写成json文件
        /// </summary>
        private void WriteJsonData()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(JsonFilePath));
            File.WriteAllText(JsonFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
