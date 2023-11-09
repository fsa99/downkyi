namespace DownKyi.Core.BiliApi.Danmaku.Models
{
    /// <summary>
    /// BiliBili_弹幕类
    /// </summary>
    public class BiliDanmaku
    {
        /// <summary>
        /// 弹幕dmID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 出现时间
        /// </summary>
        public int Progress { get; set; }
        /// <summary>
        /// 弹幕类型
        /// </summary>
        public int Mode { get; set; }
        /// <summary>
        /// 文字大小
        /// </summary>
        public int Fontsize { get; set; }
        /// <summary>
        /// 弹幕颜色
        /// </summary>
        public uint Color { get; set; }
        /// <summary>
        /// 发送者UID的HASH
        /// </summary>
        public string MidHash { get; set; }
        /// <summary>
        /// 弹幕内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public long Ctime { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }
        //public string Action { get; set; }    //动作？
        /// <summary>
        /// 弹幕池
        /// </summary>
        public int Pool { get; set; }

        public override string ToString()
        {
            string separator = "\n";
            return $"id: {Id}{separator}" +
                $"progress: {Progress}{separator}" +
                $"mode: {Mode}{separator}" +
                $"fontsize: {Fontsize}{separator}" +
                $"color: {Color}{separator}" +
                $"midHash: {MidHash}{separator}" +
                $"content: {Content}{separator}" +
                $"ctime: {Ctime}{separator}" +
                $"weight: {Weight}{separator}" +
                //$"action: {Action}{separator}" +
                $"pool: {Pool}";
        }
    }
}
