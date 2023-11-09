using DownKyi.Core.BiliApi.VideoStream;
using System;
using System.Collections.Generic;

namespace DownKyi.Models
{
    [Serializable]
    public class Downloading// : DownloadBase
    {
        public Downloading() : base()
        {
            // 初始化下载的文件列表
            DownloadFiles = new Dictionary<string, string>();
            DownloadedFiles = new List<string>();
        }

        /// <summary>
        /// Aria相关
        /// </summary>
        public string Gid { get; set; }

        /// <summary>
        /// 下载的文件
        /// </summary>
        public Dictionary<string, string> DownloadFiles { get; set; }

        /// <summary>
        /// 已下载的文件
        /// </summary>
        public List<string> DownloadedFiles { get; set; }

        /// <summary>
        /// 视频类别
        /// </summary>
        public PlayStreamType PlayStreamType { get; set; }

        /// <summary>
        /// 下载状态
        /// </summary>
        public DownloadStatus DownloadStatus { get; set; }

        /// <summary>
        /// 正在下载内容（音频、视频、弹幕、字幕、封面）
        /// </summary>
        public string DownloadContent { get; set; }

        /// <summary>
        /// 下载状态显示
        /// </summary>
        public string DownloadStatusTitle { get; set; }

        /// <summary>
        /// 下载进度
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// 已下载大小/文件大小
        /// </summary>
        public string DownloadingFileSize { get; set; }

        /// <summary>
        /// 下载的最高速度
        /// </summary>
        public long MaxSpeed { get; set; }

        /// <summary>
        /// 下载速度
        /// </summary>
        public string SpeedDisplay { get; set; }

    }
}
