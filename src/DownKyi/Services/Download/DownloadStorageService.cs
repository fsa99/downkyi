using DownKyi.Core.Logging;
using DownKyi.Core.Storage.Database.Download;
using DownKyi.Models;
using DownKyi.ViewModels.DownloadManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DownKyi.Services.Download
{
    public class DownloadStorageService
    {
        ~DownloadStorageService()
        {
            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.Close();
            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Close();
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            downloadBaseDb.Close();
        }

        #region 下载中数据

        /// <summary>
        /// 添加下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void AddDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            //LogManager.Info("数据库交互记录", nameof(RemoveDownloading), $"添加下载中数据{downloadingItem.DownloadBase.Uuid}");
            //string jsonContent = JsonConvert.SerializeObject(downloadingItem, Formatting.Indented);
            //string fileName = $"{downloadingItem.DownloadBase.Uuid}_{Stopwatch.GetTimestamp()}.json";
            //string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string filePath = Path.Combine(appDirectory, "jsonData", fileName);
            //Directory.CreateDirectory(Path.Combine(appDirectory, "jsonData"));
            //using (StreamWriter sw = new StreamWriter(filePath))
            //{
            //    sw.Write(jsonContent);
            //}

            AddDownloadBase(downloadingItem.DownloadBase);

            DownloadingDb downloadingDb = new DownloadingDb();
            object obj = downloadingDb.QueryById(downloadingItem.DownloadBase.Uuid);
            if (obj == null)
            {
                downloadingDb.Insert(downloadingItem.DownloadBase.Uuid, downloadingItem.Downloading);
            }
            //downloadingDb.Close();
        }

        /// <summary>
        /// 删除下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void RemoveDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            // RemoveDownloadBase(downloadingItem.DownloadBase.Uuid);

            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.Delete(downloadingItem.DownloadBase.Uuid);
            //downloadingDb.Close();
        }

        /// <summary>
        /// 获取所有的下载中数据
        /// </summary>
        /// <returns></returns>
        public List<DownloadingItem> GetDownloading()
        {
            // 从数据库获取数据
            DownloadingDb downloadingDb = new DownloadingDb();
            Dictionary<string, object> dic = downloadingDb.QueryAll();
            //downloadingDb.Close();

            // 遍历
            List<DownloadingItem> list = new List<DownloadingItem>();
            foreach (KeyValuePair<string, object> item in dic)
            {
                if (item.Value is Downloading downloading)
                {
                    DownloadingItem downloadingItem = new DownloadingItem
                    {
                        DownloadBase = GetDownloadBase(item.Key),
                        Downloading = downloading
                    };

                    if (downloadingItem.DownloadBase == null) { continue; }
                    list.Add(downloadingItem);
                }
            }

            return list;
        }

        /// <summary>
        /// 修改下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void UpdateDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            UpdateDownloadBase(downloadingItem.DownloadBase);

            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.Update(downloadingItem.DownloadBase.Uuid, downloadingItem.Downloading);
            //downloadingDb.Close();
        }

        #endregion

        #region 下载完成数据

        /// <summary>
        /// 添加下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void AddDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null){ return; }

            AddDownloadBase(downloadedItem.DownloadBase);

            DownloadedDb downloadedDb = new DownloadedDb();
            object obj = downloadedDb.QueryById(downloadedItem.DownloadBase.Uuid);
            if (obj == null)
            {
                downloadedDb.Insert(downloadedItem.DownloadBase.Uuid, downloadedItem.Downloaded);
            }
            //downloadedDb.Close();
        }

        /// <summary>
        /// 删除下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void RemoveDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null) { return; }

            LogManager.Info("数据库交互记录", nameof(AddDownloaded), $"删除下载完成数据{downloadedItem.DownloadBase.Uuid}");

            string jsonContent = JsonConvert.SerializeObject(downloadedItem, Formatting.Indented);
            string fileName = $"{downloadedItem.DownloadBase.Uuid}_{Stopwatch.GetTimestamp()}.json";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(appDirectory, "jsonData", fileName);
            Directory.CreateDirectory(Path.Combine(appDirectory, "jsonData"));
            //File.WriteAllText(filePath, jsonContent);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(jsonContent);
            }

            RemoveDownloadBase(downloadedItem.DownloadBase.Uuid);

            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Delete(downloadedItem.DownloadBase.Uuid);
            //downloadedDb.Close();
        }

        /// <summary>
        /// 获取所有的下载完成数据
        /// </summary>
        /// <returns></returns>
        public List<DownloadedItem> GetDownloaded()
        {
            // 从数据库获取数据
            DownloadedDb downloadedDb = new DownloadedDb();
            Dictionary<string, object> dic = downloadedDb.QueryAll();
            //downloadedDb.Close();

            // 遍历
            List<DownloadedItem> list = new List<DownloadedItem>();
            foreach (KeyValuePair<string, object> item in dic)
            {
                if (item.Value is Downloaded downloaded)
                {
                    DownloadedItem downloadedItem = new DownloadedItem
                    {
                        DownloadBase = GetDownloadBase(item.Key),
                        Downloaded = downloaded
                    };

                    if (downloadedItem.DownloadBase == null) { continue; }

                    list.Add(downloadedItem);
                }
            }

            return list;
        }

        /// <summary>
        /// 修改下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void UpdateDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null) { return; }

            UpdateDownloadBase(downloadedItem.DownloadBase);

            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Update(downloadedItem.DownloadBase.Uuid, downloadedItem.Downloaded);
            //downloadedDb.Close();
        }

        #endregion

        #region DownloadBase

        /// <summary>
        /// 向数据库添加DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void AddDownloadBase(DownloadBase downloadBase)
        {
            if (downloadBase == null) { return; }

            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            object obj = downloadBaseDb.QueryById(downloadBase.Uuid);
            if (obj == null)
            {
                downloadBaseDb.Insert(downloadBase.Uuid, downloadBase);
            }
            else
            {
                downloadBaseDb.Update(downloadBase.Uuid, downloadBase);
            }
            //downloadBaseDb.Close();
        }

        /// <summary>
        /// 从数据库删除DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void RemoveDownloadBase(string uuid)
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            object obj = downloadedDb.QueryById(uuid);
            if (obj == null)
            {
                DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
                downloadBaseDb.Delete(uuid);
            }
            //DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            //downloadBaseDb.Delete(uuid);
            //downloadBaseDb.Close();
        }

        /// <summary>
        /// 从数据库获取所有的DownloadBase
        /// </summary>
        /// <returns></returns>
        private DownloadBase GetDownloadBase(string uuid)
        {
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            object obj = downloadBaseDb.QueryById(uuid);
            //downloadBaseDb.Close();
            if (obj is DownloadBase downloadObj)
            {
                if (downloadObj.Dimension == null || (downloadObj.Dimension != null && downloadObj.Dimension.Width * downloadObj.Dimension.Height <2))
                {
                    (int width, int height) = System.Threading.Tasks.Task.Run(async () => await Core.Utils.VideoSizeGet.GetVideoWidthAndHeightAsync(downloadObj.FilePath + ".mp4")).Result;
                    downloadObj.Dimension = new Core.BiliApi.Models.Dimension() { Height = height, Width = width, Rotate = 0 };
                    UpdateDownloadBase(downloadObj);
                    return downloadObj;
                }
            }

            return obj is DownloadBase downloadBase ? downloadBase : null;
        }

        /// <summary>
        /// 从数据库修改DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void UpdateDownloadBase(DownloadBase downloadBase)
        {
            if (downloadBase == null) { return; }

            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            downloadBaseDb.Update(downloadBase.Uuid, downloadBase);
            //downloadBaseDb.Close();
        }

        #endregion

    }
}
