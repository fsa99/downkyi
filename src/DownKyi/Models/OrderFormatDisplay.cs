using DownKyi.Core.Settings;

namespace DownKyi.Models
{
    /// <summary>
    /// 表示订单格式显示的类
    /// </summary>
    public class OrderFormatDisplay
    {
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置订单格式
        /// </summary>
        public OrderFormat OrderFormat { get; set; }
    }

}
