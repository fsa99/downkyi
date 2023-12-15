using System.Collections.Generic;

namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// Upup资源
    /// </summary>
    public class UpupSettings
    {
        /// <summary>
        /// 默认的使用者名称
        /// </summary>
        public string UpupDefaultUseName { get; set; }
        /// <summary>
        /// 默认的版本号
        /// </summary>
        public string Ver { get; set; }
        /// <summary>
        /// Upup保存路径
        /// </summary>
        public string SaveUpupRootPath { get; set; } = null;
        /// <summary>
        /// 历史Upup保存路径
        /// </summary>
        public List<string> HistoryUpupRootPaths { get; set; } = null;
        /// <summary>
        /// 是否使用默认Upup资源保存路径
        /// </summary>
        public AllowStatus IsUseSaveUpupRootPath { get; set; } = AllowStatus.NONE;
        /// <summary>
        /// 是否将视频复制到生成目录
        /// </summary>
        public AllowStatus IsMoveVideoUpDirectory { get; set; } = AllowStatus.NONE;

    }
}
