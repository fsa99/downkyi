using System;
using System.IO;
using System.Linq;

namespace DownKyi.Core.Utils
{
    public static class FileHelper
    {
        /// <summary>
        /// 是否 是视频文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsVideoFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".flv", ".webm", ".ogg", ".ogv", ".mpeg", ".mpg", ".m4v", ".3gp" };
            return videoExtensions.Contains(fileExtension.ToLower());
        }

        /// <summary>
        /// 是否是  upup支持的视频文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsVideoFile1(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            string[] videoExtensions = { ".mp4", ".flv"};
            return videoExtensions.Contains(fileExtension.ToLower());
        }
    }
}
