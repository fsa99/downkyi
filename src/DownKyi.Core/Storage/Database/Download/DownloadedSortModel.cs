namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadedSortModel
    {
        /// <summary>
        /// 完成时间戳
        /// </summary>
        public long FinishedTimestamp { get; set; }

        /// <summary>
        /// 此条下载项的id
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 分区id
        /// </summary>
        public long ZoneId { get; set; }

        /// <summary>
        /// 视频序号
        /// </summary>
        public long Order { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double FileSize { get; set; }

        /// <summary>
        /// Up主id
        /// </summary>
        public long UpOwnerMid { get; set; }
    }
}
