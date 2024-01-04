using DownKyi.Core.FFmpeg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.Core.Utils
{
    /// <summary>
    /// 用于获取视频的宽度和高度类。
    /// </summary>
    public class VideoSizeGet
    {
        public static async Task<(int Width, int Height)> GetVideoWidthAndHeightAsync(string videoPath)
        {
            int width = 0;
            int height = 0;
            var fileInfo = new FileInfo(videoPath);
            if (File.Exists(fileInfo.FullName))
            {
                await Task.Run(() =>
                {
                    FFmpegHelper.GetVideoDimensions(videoPath, new Action<string>((output) =>
                    {
                        if (output != null)
                        {
                            var match = System.Text.RegularExpressions.Regex.Match(output, @"(\d+)x(\d+)");
                            if (match.Success)
                            {
                                width = int.Parse(match.Groups[1].Value);
                                height = int.Parse(match.Groups[2].Value);
                            }
                        }
                    }));
                });
                return (width, height);
            }
            else
            {
                return (-1,-1);
            }
        }

        public static async Task<bool> DetectingVideoAspectRatio(string videoPath)
        {
            int width = 0;
            int height = 0;
            bool isAspectRatio169 = false;
            (width, height) = await GetVideoWidthAndHeightAsync(videoPath);
            if (width * height > 1)
            {
                isAspectRatio169 = CalculateAspectRatio(width, height) == 16.0 / 9.0;
            }

            return isAspectRatio169;
        }

        public static bool DetectingVideoAspectRatio(int width,  int height)
        {
            bool isAspectRatio169 = false;
            if (width * height > 1)
            {
                isAspectRatio169 = CalculateAspectRatio(width, height) == 16.0 / 9.0;
            }

            return isAspectRatio169;
        }

        /// <summary>
        /// 计算 宽高比
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double CalculateAspectRatio(int width, int height)
        {
            return (double)width / height;
        }
    }
}
