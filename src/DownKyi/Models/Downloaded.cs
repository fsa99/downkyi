using System;

namespace DownKyi.Models
{
    [Serializable]
    public class Downloaded// : DownloadBase
    {
        public Downloaded() : base()
        {
        }

        /// <summary>
        /// 静态方法，将 dynamic 转化为 Downloaded 类型
        /// </summary>
        /// <param name="dynamicObj"></param>
        /// <returns></returns>
        public static Downloaded FromDynamic(dynamic dynamicObj)
        {
            Downloaded downloaded = new Downloaded
            {
                MaxSpeedDisplay = dynamicObj.MaxSpeedDisplay as string,
                FinishedTimestamp = dynamicObj.FinishedTimestamp as long? ?? 0 // 如果为null，默认为0
            };
            // 如果 FinishedTimestamp 为0，则不设置 FinishedTime
            if (downloaded.FinishedTimestamp != 0)
            {
                downloaded.SetFinishedTimestamp(downloaded.FinishedTimestamp);
            }

            return downloaded;
        }

        //  下载速度
        public string MaxSpeedDisplay { get; set; }

        // 完成时间戳
        public long FinishedTimestamp { get; set; }
        public void SetFinishedTimestamp(long finishedTimestamp)
        {
            FinishedTimestamp = finishedTimestamp;

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime dateTime = startTime.AddSeconds(finishedTimestamp);
            FinishedTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // 完成时间
        public string FinishedTime { get; set; }

    }
}
