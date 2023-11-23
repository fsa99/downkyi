namespace DownKyi.Core.Settings
{
    /// <summary>
    /// 下载器
    /// </summary>
    public enum Downloader
    {
        /// <summary>
        /// 没有设置
        /// </summary>
        NOT_SET = 0,
        /// <summary>
        /// 内置下载器
        /// </summary>
        BUILT_IN,
        /// <summary>
        /// Aria2下载器
        /// </summary>
        ARIA,
        /// <summary>
        /// 自定义Aria下载器
        /// </summary>
        CUSTOM_ARIA,
    }
}
