using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// 创建Zip压缩包
        /// </summary>
        /// <param name="sourceFolderPaths"></param>
        /// <param name="zipFilePath"></param>
        public static void CreateZipArchive(string[] sourceFolderPaths, string zipFilePath)
        {
            // 创建临时文件夹路径
            string tempFolderPath = Path.Combine(zipFilePath, "temp");

            try
            {
                // 创建临时文件夹，并设置为隐藏
                Directory.CreateDirectory(tempFolderPath);
                File.SetAttributes(tempFolderPath, FileAttributes.Hidden);

                // 将sourceFolderPaths中的文件夹及其子文件夹拷贝到临时文件夹中
                Parallel.ForEach(sourceFolderPaths, sourceFolderPath =>
                {
                    string destinationFolderPath = Path.Combine(tempFolderPath, Path.GetFileName(sourceFolderPath));
                    Directory.CreateDirectory(destinationFolderPath);

                    // 复制源文件夹中的文件和子文件夹到目标文件夹中
                    CopyFolder(sourceFolderPath, destinationFolderPath);
                });

                // 将临时文件夹中的所有内容打包压缩为指定的压缩文件
                string tempZipFilePath = Path.Combine(zipFilePath, DateTime.Now.ToString("yy-MM-dd hh mm ss ffff") + "bilibili.zip");
                ZipFile.CreateFromDirectory(tempFolderPath, tempZipFilePath);

                // 删除临时文件夹
                Directory.Delete(tempFolderPath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建压缩包时发生错误：{ex.Message}");
            }
        }

        // 复制文件夹及其子文件夹的方法
        private static void CopyFolder(string sourceFolderPath, string destinationFolderPath)
        {
            // 获取源文件夹中的所有文件和子文件夹
            Parallel.ForEach(Directory.GetFiles(sourceFolderPath), sourceFilePath =>
            {
                string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(sourceFilePath));
                File.Copy(sourceFilePath, destinationFilePath, true);
            });

            Parallel.ForEach(Directory.GetDirectories(sourceFolderPath), sourceSubFolderPath =>
            {
                string destinationSubFolderPath = Path.Combine(destinationFolderPath, Path.GetFileName(sourceSubFolderPath));
                Directory.CreateDirectory(destinationSubFolderPath);
                CopyFolder(sourceSubFolderPath, destinationSubFolderPath);
            });
        }
    }
}
