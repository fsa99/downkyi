using DownKyi.Core.Logging;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.IO;

namespace DownKyi.Core.Utils
{
    /// <summary>
    /// 用于生成视频缩略图的类。
    /// </summary>
    public class VideoThumbnailGenerator
    {
        /// <summary>
        /// 生成略缩图的路径
        /// </summary>
        public readonly string _tempImagePath;

        public VideoThumbnailGenerator(string tempImagePath)
        {
            _tempImagePath = tempImagePath;
            string folderPath = Path.GetDirectoryName(_tempImagePath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// 生成略缩图
        /// </summary>
        /// <param name="videoFilePath">视频路径</param>
        /// <param name="position">获取的时间戳</param>
        public void ExtractSnapshot(string videoFilePath, TimeSpan position)
        {
            try
            {
                if (!File.Exists(videoFilePath))
                {
                    LogManager.Info($"{videoFilePath} 文件不存在！");
                    return;
                }

                var inputFile = new MediaFile { Filename = videoFilePath };
                if (File.Exists(_tempImagePath))
                {
                    File.Delete(_tempImagePath);
                }
                var outputFile = new MediaFile { Filename = _tempImagePath };

                var conversionOptions = new ConversionOptions { Seek = position };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.GetThumbnail(inputFile, outputFile, conversionOptions);
                }
                LogManager.Info($"视频截图成功：{_tempImagePath}");
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
            }
        }
    }
}
