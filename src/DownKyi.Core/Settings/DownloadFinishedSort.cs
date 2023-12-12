namespace DownKyi.Core.Settings
{
    /// <summary>
    /// 下载完成后排序
    /// </summary>
    public enum DownloadFinishedSort
    {
        /// <summary>
        /// 无设置
        /// </summary>
        NOT_SET = 0,
        /// <summary>
        /// 下载完成时间 排序
        /// </summary>
        DOWNLOAD,
        /// <summary>
        /// UP主ID 排序
        /// </summary>
        UPZHUID,
        /// <summary>
        /// 按Order 排序
        /// </summary>
        NUMBER,
        /// <summary>
        /// 文件大小 排序
        /// </summary>
        FILESIZE,
        /// <summary>
        /// 视屏时长 排序
        /// </summary>
        VIDEODURATION,
        /// <summary>
        /// 分区ID 排序
        /// </summary>
        ZONEID,
    }
}
