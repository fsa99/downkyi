namespace DownKyi.Core.Settings
{
    /// <summary>
    /// 下载完成后的操作 枚举
    /// </summary>
    public enum AfterDownloadOperation
    {
        /// <summary>
        /// 没有设置
        /// </summary>
        NOT_SET = 0,
        /// <summary>
        /// 什么都不做
        /// </summary>
        NONE = 1,
        /// <summary>
        /// 打开文件夹
        /// </summary>
        OPEN_FOLDER,
        /// <summary>
        /// 关闭APP
        /// </summary>
        CLOSE_APP,
        /// <summary>
        /// 关闭系统
        /// </summary>
        CLOSE_SYSTEM,
        /// <summary>
        /// 创建UpUp壁纸资源
        /// </summary>
        CREATE_UPUPRESOURCE
    }
}
