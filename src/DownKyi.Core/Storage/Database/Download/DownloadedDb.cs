using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadedDb : DownloadDb
    {
        private readonly string tableName_base = "download_base";
        private static bool isReverseOrder = false; // 当前是否为逆序

        public DownloadedDb()
        {
            tableName = "downloaded";
            CreateTableDownloaded();
        }

        /// <summary>
        /// 基础Pro表 如果表不存在则创建表
        /// </summary>
        protected void CreateTableDownloaded()
        {
            string sql = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                            Uuid VARCHAR(255) PRIMARY KEY,
                            MaxSpeedDisplay TEXT,
                            FinishedTimestamp UNSIGNED BIG INT
                        ); ";
            dbHelper.ExecuteNonQuery(sql);
        }

        #region 增、删、改、查
        /// <summary>
        /// 插入或更新已下载数据downloaded
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="dynamicObj"></param>
        public void SaveDownloaded(string uuid, dynamic dynamicObj)
        {
            try
            {
                string sql1 = $"INSERT OR REPLACE INTO {tableName} (Uuid, MaxSpeedDisplay, FinishedTimestamp) VALUES (@Uuid, @MaxSpeedDisplay, @FinishedTimestamp);";

                dbHelper.ExecuteNonQuery(sql1, new Action<SQLiteParameterCollection>((parameters) =>
                {
                    parameters.Add("@Uuid", DbType.String).Value = uuid;
                    parameters.Add("@MaxSpeedDisplay", DbType.String).Value = dynamicObj.MaxSpeedDisplay;
                    parameters.Add("@FinishedTimestamp", DbType.UInt64).Value = dynamicObj.FinishedTimestamp;
                }));

            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Insert()发生异常: {0}", e);
                Logging.LogManager.Error($"{tableName}", e);
            }
        }

        /// <summary>
        /// 清空所有下载完成记录
        /// </summary>
        public void ClearAll()
        {
            try
            {
                string sql = $@"WITH temp_result AS(
                                SELECT t1.uuid
                                FROM ""{tableName_base}"" t1
                                INNER JOIN ""{tableName}"" t2 ON t1.Uuid = t2.Uuid)

                                DELETE FROM ""{tableName}""
                                WHERE uuid IN(SELECT uuid FROM temp_result);
                                DELETE FROM ""{tableName_base}""
                                WHERE uuid IN(SELECT uuid FROM temp_result);
                ";
                dbHelper.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Delete()发生异常: {0}", e);
                LogManager.Error($"{tableName}", e);
            }
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public new List<dynamic> QueryAll()
        {
            string sql = $@"SELECT Uuid, MaxSpeedDisplay, FinishedTimestamp
                            FROM ""{tableName}"" ";
            return Query(sql);
        }

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <returns></returns>
        public new List<dynamic> QueryPageData(int curPage)
        {
            List<string> uuids = GetSortedUuids(GetSortedData(), curPage);
            List<dynamic> results = new List<dynamic>();
            foreach (var uuid in uuids)
            {
                results.Add(QueryByUuid(uuid));
            }
            return results;
        }

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <returns></returns>
        public List<string> QueryUuidPageData(int curPage)
        {
            return GetSortedUuids(GetSortedData(), curPage);
        }

        /// <summary>
        /// 查询uuids对应的数据
        /// </summary>
        /// <param name="uuids"></param>
        /// <returns></returns>
        public List<dynamic> QueryByUuids(List<string> uuids)
        {
            string sql = $@"SELECT Uuid, MaxSpeedDisplay, FinishedTimestamp
                            FROM ""{tableName}""
                            WHERE Uuid in ('{string.Join("', '", uuids)}')";
            return Query(sql);
        }

        /// <summary>
        /// 查询uuid对应的数据
        /// </summary>
        /// <param name="uuids"></param>
        /// <returns></returns>
        public dynamic QueryByUuid(string uuid)
        {
            string sql = $@"SELECT Uuid, MaxSpeedDisplay, FinishedTimestamp
                            FROM ""{tableName}""
                            WHERE Uuid in ('{uuid}')";
            return Query(sql)?[0];
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<dynamic> Query(string sql)
        {
            List<dynamic> objs = new List<dynamic>();

            try
            {
                dbHelper.ExecuteQuery(sql, reader =>
                {
                    while (reader.Read())
                    {
                        dynamic entity = new System.Dynamic.ExpandoObject();
                        var expandoDict = (IDictionary<string, object>)entity;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            expandoDict.Add(reader.GetName(i), reader[i]);
                        }

                        objs.Add(entity);
                    }
                });
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Query()发生异常: {0}", e);
                LogManager.Error($"{tableName}", e);
            }

            return objs;
        }

        /// <summary>
        /// 查询是否存在下载完成列表中
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="ResolutionId"></param>
        /// <param name="AudioCodecId"></param>
        /// <param name="VideoCodecName"></param>
        /// <returns></returns>
        public bool IsExistDownloaded(long Cid, int ResolutionId, string AudioCodecName, string VideoCodecName)
        {
            string sql = $@"SELECT COUNT(*) AS RecordCount
                            FROM ""{tableName_base}"" t1
                            INNER JOIN {tableName} t2 on t1.Uuid = t2.Uuid
                            WHERE Cid = {Cid} AND ResolutionId = {ResolutionId} AND VideoCodecName = '{VideoCodecName}' AND AudioCodecId = (SELECT Id from base_pro_quality WHERE NAME = '{AudioCodecName}') ;";
            
            return QueryCount(sql) > 0;
        }

        /// <summary>
        /// 查询是否存在下载完成列表中 并返回uuid
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="ResolutionId"></param>
        /// <param name="AudioCodecName"></param>
        /// <param name="VideoCodecName"></param>
        /// <returns></returns>
        public string GetDownloadedUuid(long Cid, long Avid, string Bvid, int ResolutionId, string Name, string VideoCodecName)
        {
            string sql = $@"SELECT t1.Uuid
                            FROM ""{tableName_base}"" t1
                            INNER JOIN {tableName} t2 on t1.Uuid = t2.Uuid
                            WHERE Cid = {Cid} AND Bvid = '{Bvid}' AND Avid = '{Avid}' AND ResolutionId = {ResolutionId} AND VideoCodecName = '{VideoCodecName}' AND Name = '{Name}' ;";

            return QueryUuid(sql);
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private long QueryCount(string sql)
        {
            long RecordCount = 0;

            try
            {
                dbHelper.ExecuteQuery(sql, reader =>
                {
                    while (reader.Read())
                    {
                        RecordCount = (long)reader["RecordCount"];
                    }
                });
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Query()发生异常: {0}", e);
                LogManager.Error($"{sql}", e);
            }

            return RecordCount;
        }

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string QueryUuid(string sql)
        {
            string uuid = string.Empty;

            try
            {
                dbHelper.ExecuteQuery(sql, reader =>
                {
                    while (reader.Read())
                    {
                        uuid = (string)reader["Uuid"];
                    }
                });
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Query()发生异常: {0}", e);
                LogManager.Error($"{sql}", e);
            }

            return uuid;
        }
        #endregion

        #region 排序后的分页查询
        /// <summary>
        /// 获取表的总记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            string sql = $@"SELECT COUNT(*) AS RecordCount
                            FROM ""{tableName}"" t1
                            INNER JOIN ""{tableName_base}"" t2 ON t1.Uuid = t2.Uuid; ";
            
            return Convert.ToInt32(QueryCount(sql));
        }
        /// <summary>
        /// 获取所有的可排序的字段  数据
        /// </summary>
        /// <returns></returns>
        private List<DownloadedSortModel> GetSortedData()
        {
            List<DownloadedSortModel> sortModels = new List<DownloadedSortModel>();
            string sql = $@"SELECT t1.Uuid, t2.""ORDER"", t2.FileSize, t2.UpOwnerMid, t2.ZoneId, t2.Duration, t1.FinishedTimestamp
                            from ""{tableName}"" t1
                            INNER JOIN ""{tableName_base}"" t2 on t1.Uuid = t2.Uuid";

            try
            {
                dbHelper.ExecuteQuery(sql, reader =>
                {
                    while (reader.Read())
                    {
                        sortModels.Add(new DownloadedSortModel()
                        {
                            Uuid = (string)reader["Uuid"],
                            Order = (int)reader["ORDER"],
                            FileSize = Utils.Format.ParseFileSize((string)reader["FileSize"]),
                            UpOwnerMid = (long)reader["UpOwnerMid"],
                            ZoneId = (int)reader["ZoneId"],
                            Duration = Utils.Format.ConvertTimeToSeconds((string)reader["Duration"]),
                            FinishedTimestamp = (long)reader["FinishedTimestamp"]
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Query()发生异常: {0}", e);
                LogManager.Error($"{tableName}", e);
            }

            return sortModels;
        }

        public List<string> GetSortedUuids(List<DownloadedSortModel> downloadedSorts, int curPage)
        {
            if (curPage < 1) { curPage = 1; }
            int itemsPer = SettingsManager.GetInstance().GetLastItemsPerPage();
            int startIndex = itemsPer * (curPage - 1);
            DownloadFinishedSort finishedSort = SettingsManager.GetInstance().GetDownloadFinishedSort();
            
            isReverseOrder = SettingsManager.GetInstance().GetSortOrder() == SortOrder.DESCENDING;

            List<string> curPageUuids = new List<string>();
            if (downloadedSorts.Any())
            {
                IEnumerable<DownloadedSortModel> sortedList;

                switch (finishedSort)
                {
                    case DownloadFinishedSort.DOWNLOAD:
                        sortedList = downloadedSorts.OrderBy(item => item.FinishedTimestamp);
                        break;
                    case DownloadFinishedSort.NUMBER:
                        sortedList = downloadedSorts.OrderBy(item => item.Order);
                        break;
                    case DownloadFinishedSort.UPZHUID:
                        sortedList = downloadedSorts.OrderBy(item => item.UpOwnerMid).ThenBy(item => item.Order);
                        break;
                    case DownloadFinishedSort.FILESIZE:
                        sortedList = downloadedSorts.OrderBy(item => item.FileSize).ThenBy(item => item.Order);
                        break;
                    case DownloadFinishedSort.VIDEODURATION:
                        sortedList = downloadedSorts.OrderBy(item => item.Duration).ThenBy(item => item.Order);
                        break;
                    case DownloadFinishedSort.ZONEID:
                        sortedList = downloadedSorts.OrderBy(item => item.ZoneId).ThenBy(item => item.UpOwnerMid).ThenBy(item => item.Order);
                        break;
                    default:
                        sortedList = downloadedSorts;
                        break;
                }

                // 如果是逆序，则反转排序
                if (isReverseOrder)
                {
                    sortedList = sortedList.Reverse();
                }

                // 分页获取当前页的 UUID 列表
                curPageUuids = sortedList.Skip(startIndex).Take(itemsPer).Select(x => x.Uuid).ToList();
            }

            return curPageUuids;
        }
        #endregion
    }
}
