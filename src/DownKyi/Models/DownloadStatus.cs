namespace DownKyi.Models
{
    public enum DownloadStatus
    {
        /// <summary>
        /// 未开始，从未开始下载
        /// </summary>
        NOT_STARTED,
        /// <summary>
        /// 等待下载，下载过，但是启动本次下载周期未开始，如重启程序后未开始
        /// </summary>
        WAIT_FOR_DOWNLOAD,

        /// <summary>
        /// 暂停启动下载
        /// </summary>
        PAUSE_STARTED,

        /// <summary>
        /// 暂停
        /// </summary>
        PAUSE,

        /// <summary>
        /// 下载中
        /// </summary>
        DOWNLOADING,

        /// <summary>
        /// 下载成功
        /// </summary>
        DOWNLOAD_SUCCEED,

        /// <summary>
        /// 下载失败
        /// </summary>
        DOWNLOAD_FAILED,

        //PAUSE_TO_WAIT,     // 暂停后等待
    }
}
