namespace DownKyi.Core.Settings
{
    /// <summary>
    /// 视频解析范围
    /// </summary>
    public enum ParseScope
    {
        NOT_SET = 0,
        NONE = 1,
        /// <summary>
        /// 选定的项目
        /// </summary>
        SELECTED_ITEM,
        /// <summary>
        /// 当前部分
        /// </summary>
        CURRENT_SECTION,
        /// <summary>
        /// 所有
        /// </summary>
        ALL
    }
}
