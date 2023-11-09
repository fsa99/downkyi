using DownKyi.Core.Settings;

namespace DownKyi.Models
{
    /// <summary>
    /// 表示解析范围显示的类
    /// </summary>
    public class ParseScopeDisplay
    {
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置解析范围
        /// </summary>
        public ParseScope ParseScope { get; set; }
    }

}
