using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.Storage.Database.Download;
using DownKyi.Events;
using DownKyi.Models;
using DownKyi.ViewModels.DownloadManager;
using Prism.Events;
using System.Collections.Generic;

namespace DownKyi.Services.Download
{
    public class DownloadStorageService
    {
        public DownloadStorageService() { }

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

            AddDownloadBase(downloadingItem.DownloadBase);

            DownloadingDb downloadingDb = new DownloadingDb();
            object obj = downloadingDb.QueryById(downloadingItem.DownloadBase.Uuid);
            if (obj == null)
            {
                downloadingDb.Insert(downloadingItem.DownloadBase.Uuid, downloadingItem.Downloading);
            }
        }

        /// <summary>
        /// 删除下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void RemoveDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.DeleteByid(downloadingItem.DownloadBase.Uuid);
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
            object obj = downloadedDb.QueryByUuid(downloadedItem.DownloadBase.Uuid);
            if (obj == null)
            {
                downloadedDb.SaveDownloaded(downloadedItem.DownloadBase.Uuid, downloadedItem.Downloaded);
            }
        }

        /// <summary>
        /// 清空所有下载完成记录
        /// </summary>
        public void ClearAll()
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.ClearAll();
        }

        /// <summary>
        /// 删除下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void RemoveDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null) { return; }

            RemoveDownloadBase(downloadedItem.DownloadBase.Uuid);

            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Delete(downloadedItem.DownloadBase.Uuid);
        }

        /// <summary>
        /// 删除下载完成数据
        /// </summary>
        /// <param name="uuid"></param>
        public void RemoveDownloaded(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) { return; }

            RemoveDownloadBase(uuid);

            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Delete(uuid);
        }

        /// <summary>
        /// 获取所有的下载完成数据
        /// </summary>
        /// <returns></returns>
        public List<DownloadedItem> GetDownloaded()
        {
            // 从数据库获取数据
            List<DownloadedItem> list = new List<DownloadedItem>();
            DownloadedDb downloadedDb = new DownloadedDb();
            //Dictionary<string, object> dic = downloadedDb.QueryAll();
            //// 遍历downloadedDb
            //foreach (KeyValuePair<string, object> item in dic)
            //{
            //    if (item.Value is Downloaded downloaded)
            //    {
            //        downloadedDb.SaveDownloaded(item.Key, item.Value);
            //        DownloadedItem downloadedItem = new DownloadedItem
            //        {
            //            DownloadBase = GetDownloadBase(item.Key),
            //            Downloaded = downloaded
            //        };

            //        if (downloadedItem.DownloadBase == null) { continue; }

            //        list.Add(downloadedItem);
            //    }
            //}

            List<dynamic> downloadeds = downloadedDb.QueryAll();
            foreach (dynamic item in downloadeds)
            {
                Downloaded downloaded = Downloaded.FromDynamic(item);
                DownloadedItem downloadedItem = new DownloadedItem
                {
                    DownloadBase = GetDownloadBase(item.Uuid),
                    Downloaded = downloaded
                };

                if (downloadedItem.DownloadBase == null) { continue; }

                list.Add(downloadedItem);
            }

            return list;
        }

        /// <summary>
        /// 获取所有的下载完成数据
        /// </summary>
        /// <returns></returns>
        public DownloadedItem GetDownloadedItem(string uuid)
        {
            DownloadedDb downloadedDb = new DownloadedDb();

            return new DownloadedItem()
            {
                Downloaded = GetDownloaded(uuid),
                DownloadBase = GetDownloadBase(uuid)
            };
        }

        /// <summary>
        /// 获取Uuid对应的下载完成数据
        /// </summary>
        /// <returns></returns>
        public Downloaded GetDownloaded(string Uuid)
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            dynamic downloaded = downloadedDb.QueryByUuid(Uuid);
            if (downloaded == null) return null;
            return Downloaded.FromDynamic(downloaded);
        }

        /// <summary>
        /// 获取下载完成数据  排序的分页的
        /// </summary>
        /// <returns></returns>
        public List<DownloadedItem> GetSortPageDownloaded(int curPage = 1)
        {
            List<DownloadedItem> list = new List<DownloadedItem>();
            DownloadedDb downloadedDb = new DownloadedDb();
            List<dynamic> downloadeds = downloadedDb.QueryPageData(curPage);
            foreach (dynamic item in downloadeds)
            {
                Downloaded downloaded = Downloaded.FromDynamic(item);
                DownloadedItem downloadedItem = new DownloadedItem
                {
                    DownloadBase = GetDownloadBase(item.Uuid),
                    Downloaded = downloaded
                };

                if (downloadedItem.DownloadBase == null) { continue; }

                list.Add(downloadedItem);
            }

            return list;

            // 测试

            //DownloadBaseProDb downloadedBaseProDb = new DownloadBaseProDb();
            //foreach (var item in allDownloadedItems)
            //{
            //    downloadedBaseProDb.InsertDownloadBase(item?.DownloadBase.Uuid, item?.DownloadBase);
            //}

            //var aaaaaaaaa = downloadedBaseProDb.QueryAll();
            //return allDownloadedItems;
        }
        /// <summary>
        /// 获取下载完成数据  排序的分页的
        /// </summary>
        /// <returns></returns>
        public List<string> GetSortPageDownloaded1(int curPage = 1)
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            return downloadedDb.QueryUuidPageData(curPage);
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
            downloadedDb.SaveDownloaded(downloadedItem.DownloadBase.Uuid, downloadedItem.Downloaded);
        }

        /// <summary>
        /// 获取下载完成项的总记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            return downloadedDb.GetRecordCount();
        }

        /// <summary>
        /// 查询是否存在下载完成列表中
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="ResolutionId"></param>
        /// <param name="AudioCodecName"></param>
        /// <param name="VideoCodecName"></param>
        /// <returns></returns>
        public bool IsExistDownloaded(long Cid, int ResolutionId, string AudioCodecName, string VideoCodecName)
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            return downloadedDb.IsExistDownloaded(Cid, ResolutionId, AudioCodecName, VideoCodecName);
        }

        /// <summary>
        /// 查询是否存在下载完成列表中
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="ResolutionId"></param>
        /// <param name="AudioCodecName"></param>
        /// <param name="VideoCodecName"></param>
        /// <returns></returns>
        public string GetDownloadedUuid(long Cid, long Avid, string Bvid, int ResolutionId, string Name, string VideoCodecName)
        {
            DownloadedDb downloadedDb = new DownloadedDb();
            return downloadedDb.GetDownloadedUuid(Cid, Avid, Bvid, ResolutionId, Name, VideoCodecName);
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
            object obj = downloadBaseDb.QueryByUuid(downloadBase.Uuid);
            if (obj == null)
            {
                downloadBaseDb.InsertDownloadBase(downloadBase.Uuid, downloadBase);
            }
            else
            {
                downloadBaseDb.Update(downloadBase.Uuid, downloadBase);
            }
        }

        /// <summary>
        /// 从数据库删除DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void RemoveDownloadBase(string uuid)
        {
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            downloadBaseDb.Delete(uuid);
        }

        /// <summary>
        /// 从数据库获取uuid 对应的DownloadBase
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        private DownloadBase GetDownloadBase(string uuid)
        {
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            dynamic dynamicobj = downloadBaseDb.QueryByUuid(uuid);
            DownloadBase downloadBaseModel = DownloadBase.FromDynamic(dynamicobj);
            return downloadBaseModel ?? null;
        }

        /// <summary>
        /// 从数据库获取所有的DownloadBase
        /// </summary>
        /// <returns></returns>
        private List<DownloadBase> GetDownloadBase()
        {
            List<DownloadBase> downloadBases = new List<DownloadBase>();
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            List<dynamic> dynamicObjs = downloadBaseDb.QueryAll();
            if (dynamicObjs != null && dynamicObjs.Count > 0)
            {
                foreach (var item in dynamicObjs)
                {
                    downloadBases.Add(DownloadBase.FromDynamic(item));
                }
            }
            
            return downloadBases.Count == 0 ? null : downloadBases;
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
        }

        #endregion
    }
}
