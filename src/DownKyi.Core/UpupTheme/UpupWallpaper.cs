using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.UpupTheme
{
    public class UpupWallpaper
    {
        private List<UpupModel> _upupData;
        [JsonProperty("data")]
        public List<UpupModel> UpupData 
        {
            get => _upupData;
            set{
                _upupData = value;
                UpdateTotal();
            }
        }

        [JsonProperty("total")]
        public int Total { get; private set; }

        private void UpdateTotal()
        {
            Total = _upupData.Count;
        }
    }
}
