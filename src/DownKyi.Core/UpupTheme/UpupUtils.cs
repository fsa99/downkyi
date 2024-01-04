using DownKyi.Core.FFmpeg;
using DownKyi.Core.Utils;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.Core.UpupTheme
{
    /// <summary>
    /// Upup工具类
    /// </summary>
    public class UpupUtils
    {
        private static readonly string imgFileName = ".Cover.jpg";
        private static readonly string videoFileName = ".mp4";
        private static readonly string previewIMGName = "preview.jpg";

        /// <summary>
        /// 源图片文件路径
        /// </summary>
        public string SourceIMGFileName { get; set; }
        /// <summary>
        /// 源视频文件路径
        /// </summary>
        public string SourceVideoFileName { get; set; }
        /// <summary>
        /// 目标 根目录
        /// </summary>
        public string TargetRootDirectory { get; set; }
        /// <summary>
        /// 目标目录
        /// </summary>
        public string TargetDirectory { get; set; }
        /// <summary>
        /// 目标图片文件路径
        /// </summary>
        public string TargetIMGFileName { get; set; }
        /// <summary>
        /// 目标视频文件路径
        /// </summary>
        public string TargetVideoFileName { get; set; }
        public UpupModel Upup { get; set; }

        public UpupUtils(string directory, string sourceFilePath , ref UpupModel upupModel)
        {
            SourceIMGFileName = sourceFilePath + imgFileName;
            SourceVideoFileName = sourceFilePath + videoFileName;
            TargetRootDirectory = directory;
            TargetDirectory = Path.Combine(directory, upupModel.Themeno.ToString());
            Directory.CreateDirectory(TargetDirectory);
            TargetIMGFileName = Path.Combine(TargetDirectory, previewIMGName);
            TargetVideoFileName = Path.Combine(TargetDirectory, Path.GetFileName(SourceVideoFileName));
            Upup = upupModel;
        }

        /// <summary>
        /// 本地视频拖放 的构造函数 会自动从视频中生成封面
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <param name="targetdirectory"></param>
        /// <param name="i"></param>
        public UpupUtils(string localFilePath, string targetdirectory, int i)
        {
            if (!string.IsNullOrEmpty(localFilePath) && !string.IsNullOrEmpty(targetdirectory))
            {
                Upup = new UpupModel(Path.GetFileName(localFilePath), i);
                SourceVideoFileName = localFilePath;
                TargetRootDirectory = targetdirectory;
                TargetDirectory = Path.Combine(TargetRootDirectory, Upup.Themeno.ToString());
                TargetIMGFileName = Path.Combine(TargetDirectory, previewIMGName);
                TargetVideoFileName = Path.Combine(TargetDirectory, Path.GetFileName(localFilePath));

                //FFmpegHelper.ExtractFrame(SourceVideoFileName, TargetIMGFileName, 5);
                VideoThumbnailGenerator videoThumbnail = new VideoThumbnailGenerator(TargetIMGFileName);
                videoThumbnail.ExtractSnapshot(SourceVideoFileName, TimeSpan.FromSeconds(5));
            }
        }
        /// <summary>
        /// 创建视频、封面、配置文件   拖放视频生成使用
        /// </summary>
        /// <returns></returns>
        public bool UpupCreate_AllFromLocal()
        {
            return UpupCreate_Video() && UpupWriteupupJsonFile();
        }

        /// <summary>
        /// 生成视频到目标文件夹
        /// </summary>
        /// <returns></returns>
        public bool UpupCreate_Video()
        {
            if (!File.Exists(SourceVideoFileName))
            {
                // 视频文件不存在或被删除
                return false;
            }
            Directory.CreateDirectory(Path.GetDirectoryName(TargetVideoFileName));
            Task.Run(() => File.Copy(SourceVideoFileName, TargetVideoFileName, true));
            Upup.Src = Path.GetFileName(SourceVideoFileName);
            return true;
        }

        /// <summary>
        /// upup资源中 封面的生成 (下载有 复制即可)
        /// </summary>
        /// <returns></returns>
        public bool UpupCreate_CoverCopy()
        {
            if (File.Exists(SourceIMGFileName))
            {
                File.Copy(SourceIMGFileName, TargetIMGFileName, true);
                return true;
            }
            else
            {
                // 查询、保存封面
                return false;
            }
        }

        /// <summary>
        /// 写入.upup配置文件以及 更新配置汇总文件
        /// </summary>
        public bool UpupWriteupupJsonFile()
        {
            try
            {
                Upup.WriteJsonData(TargetRootDirectory);
                UpupWallpaper upuptotalJson = new UpupWallpaper(TargetRootDirectory, Upup);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 视频宽高比检测
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DetectingVideoAspectRatio()
        {
            int width = 0;
            int height = 0;

            (width, height) = await VideoSizeGet.GetVideoWidthAndHeightAsync(SourceVideoFileName);

            bool isAspectRatio169 = VideoSizeGet.CalculateAspectRatio(width, height) == 16.0 / 9.0;

            return isAspectRatio169;
        }

        /// <summary>
        /// 视频宽高比检测
        /// </summary>
        /// <returns></returns>
        public async Task<(int width, int height)> GetVideoAspectRatio()
        {
            return await VideoSizeGet.GetVideoWidthAndHeightAsync(SourceVideoFileName).ConfigureAwait(false);
        }
    }
}
