namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 基本
    /// </summary>
    public class BasicSettings
    {
        /// <summary>
        /// 下载完成后的操作
        /// </summary>
        public AfterDownloadOperation AfterDownload { get; set; } = AfterDownloadOperation.NOT_SET;
        /// <summary>
        /// 是否监听剪切板
        /// </summary>
        public AllowStatus IsListenClipboard { get; set; } = AllowStatus.NONE;
        /// <summary>
        /// 是否自动解析视频
        /// </summary>
        public AllowStatus IsAutoParseVideo { get; set; } = AllowStatus.NONE;
        /// <summary>
        /// 视屏解析范围
        /// </summary>
        public ParseScope ParseScope { get; set; } = ParseScope.NOT_SET;
        /// <summary>
        /// 是否自动下载所有
        /// </summary>
        public AllowStatus IsAutoDownloadAll { get; set; } = AllowStatus.NONE;
        /// <summary>
        /// 下载完成后排序
        /// </summary>
        public DownloadFinishedSort DownloadFinishedSort { get; set; } = DownloadFinishedSort.NOT_SET;
        /// <summary>
        /// 排序方式
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.NOT_SET;
        /// <summary>
        /// 记录上次修改的单页显示条数
        /// </summary>
        public int LastItemsPerPage { get; set; }

    }
}
