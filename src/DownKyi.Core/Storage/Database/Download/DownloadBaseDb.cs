using DownKyi.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;

namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadBaseDb : DownloadDb
    {
        private readonly string tableName_videoowner = "base_pro_videoowner";
        private readonly string tableName_quality = "base_pro_quality";
        private readonly string tableName_dimension = "base_pro_dimension";

        public DownloadBaseDb()
        {
            tableName = "download_base";
            CreateTableBasePro();
            CreateTableUpOwner();
            CreateTableAudioAndVideoQuality();
            CreateTableDimension();
        }


        #region 创建表结构
        /// <summary>
        /// 基础Pro表 如果表不存在则创建表
        /// </summary>
        protected void CreateTableBasePro()
        {
            string sql = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                            Uuid VARCHAR(255) PRIMARY KEY,
                            NeedDownloadContent TEXT,
                            Bvid VARCHAR(20),
                            Avid UNSIGNED BIG INT,
                            Cid UNSIGNED BIG INT,
                            EpisodeId INTEGER,
                            CoverUrl TEXT,
                            PageCoverUrl TEXT,
                            ZoneId INT,
                            [ORDER] INT,
                            MainTitle TEXT,
                            Name TEXT,
                            Duration VARCHAR(20),
                            DimensionId INT,
                            VideoCodecName TEXT,
                            ResolutionId INT,
                            AudioCodecId INT,
                            FilePath TEXT,
                            FileSize TEXT,
                            CreateTime TEXT,
                            PlayNumber VARCHAR(20),
                            DanmakuNumber VARCHAR(20),
                            LikeNumber VARCHAR(20),
                            CoinNumber VARCHAR(20),
                            FavoriteNumber VARCHAR(20),
                            ShareNumber VARCHAR(20),
                            Description TEXT,
                            UpOwnerMid UNSIGNED BIG INT
                        ); ";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// up主个人信息表 如果表不存在则创建表
        /// </summary>
        protected void CreateTableUpOwner()
        {
            string sql = $@"CREATE TABLE IF NOT EXISTS {tableName_videoowner} (
                            UpOwnerMid UNSIGNED BIG INT UNIQUE PRIMARY KEY,
                            Name TEXT,
                            Face TEXT
                        );";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 视频或音频质量表 如果表不存在则创建表
        /// </summary>
        protected void CreateTableAudioAndVideoQuality()
        {
            string sql = $@"CREATE TABLE IF NOT EXISTS {tableName_quality} (
                            Name TEXT,
                            Id INTEGER PRIMARY KEY
                        );";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 视频或音频质量表 如果表不存在则创建表
        /// </summary>
        protected void CreateTableDimension()
        {
            string sql = $@"CREATE TABLE IF NOT EXISTS {tableName_dimension} (
                            DimensionId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Width INT,
                            Height INT,
                            Rotate INT,
                            UNIQUE (Width, Height, Rotate)
                        );";
            dbHelper.ExecuteNonQuery(sql);
        }
        #endregion

        #region 增、删、改、查
        /// <summary>
        /// 插入或更新 UpOwner 表中的数据
        /// </summary>
        private void InsertOrUpdateUpOwner(dynamic dynamicObj)
        {
            string sql = $"INSERT OR REPLACE INTO {tableName_videoowner} (UpOwnerMid, Name, Face) VALUES (@UpOwnerMid, @Name, @Face);";
            dbHelper.ExecuteNonQuery(sql, new Action<SQLiteParameterCollection>((parameters) =>
            {
                parameters.Add("@UpOwnerMid", DbType.UInt64).Value = dynamicObj.UpOwner.Mid;
                parameters.Add("@Name", DbType.String).Value = dynamicObj.UpOwner.Name;
                parameters.Add("@Face", DbType.String).Value = dynamicObj.UpOwner.Face;
            }));
        }

        /// <summary>
        /// 插入或更新 Dimension 表中的数据
        /// </summary>
        private void InsertOrUpdateDimension(dynamic dynamicObj)
        {
            string sql = $"INSERT OR IGNORE INTO {tableName_dimension} (Width, Height, Rotate) VALUES (@Width, @Height, @Rotate);";
            dbHelper.ExecuteNonQuery(sql, new Action<SQLiteParameterCollection>((parameters) =>
            {
                parameters.Add("@Width", DbType.Int32).Value = dynamicObj.Dimension.Width;
                parameters.Add("@Height", DbType.Int32).Value = dynamicObj.Dimension.Height;
                parameters.Add("@Rotate", DbType.Int32).Value = dynamicObj.Dimension.Rotate;
            }));
        }

        /// <summary>
        /// 插入或更新 Quality 表中的数据
        /// </summary>
        private void InsertOrUpdateQuality(dynamic dynamicObj)
        {
            string sql = $"INSERT OR IGNORE INTO {tableName_quality} (Name, Id) VALUES (@Name, @Id);";
            dbHelper.ExecuteNonQuery(sql, new Action<SQLiteParameterCollection>((parameters) =>
            {
                parameters.Add("@Name", DbType.String).Value = dynamicObj.Resolution.Name;
                parameters.Add("@Id", DbType.Int32).Value = dynamicObj.Resolution.Id;
            }));
        }
        /// <summary>
        /// 插入基础数据DownloadBasePro
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="dynamicObj"></param>
        public void InsertDownloadBase(string uuid, dynamic dynamicObj)
        {
            try
            {
                InsertOrUpdateUpOwner(dynamicObj);
                InsertOrUpdateDimension(dynamicObj);
                InsertOrUpdateQuality(dynamicObj);
                InsertOrUpdateQuality(dynamicObj);
                string sql = $@"INSERT INTO {tableName} (
                                    Uuid, NeedDownloadContent, Bvid, Avid, Cid, EpisodeId, CoverUrl, PageCoverUrl, ZoneId, [Order], MainTitle, Name, 
                                    Duration, DimensionId, VideoCodecName, ResolutionId, AudioCodecId, FilePath, FileSize, 
                                    CreateTime, PlayNumber, DanmakuNumber, LikeNumber, CoinNumber, FavoriteNumber, ShareNumber, 
                                    Description, UpOwnerMid
                                ) VALUES (
                                    @Uuid, @NeedDownloadContent, @Bvid, @Avid, @Cid, @EpisodeId, @CoverUrl, @PageCoverUrl, @ZoneId, @Order, @MainTitle, @Name,
                                    @Duration, (SELECT DimensionId FROM {tableName_dimension} WHERE Width = @Width AND Height = @Height AND Rotate = @Rotate), @VideoCodecName, @ResolutionId, @AudioCodecId, @FilePath, @FileSize,
                                    @CreateTime, @PlayNumber, @DanmakuNumber, @LikeNumber, @CoinNumber, @FavoriteNumber, @ShareNumber, 
                                    @Description, @UpOwnerMid
                                );";

                dbHelper.ExecuteNonQuery(sql, new Action<SQLiteParameterCollection>((parameters) =>
                {
                    parameters.Add("@Uuid", DbType.String).Value = uuid;
                    parameters.Add("@NeedDownloadContent", DbType.String).Value = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicObj.NeedDownloadContent);
                    parameters.Add("@Bvid", DbType.String).Value = dynamicObj.Bvid;
                    parameters.Add("@Avid", DbType.Int64).Value = dynamicObj.Avid;
                    parameters.Add("@Cid", DbType.Int64).Value = dynamicObj.Cid;
                    parameters.Add("@EpisodeId", DbType.Int32).Value = dynamicObj.EpisodeId;
                    parameters.Add("@CoverUrl", DbType.String).Value = dynamicObj.CoverUrl;
                    parameters.Add("@PageCoverUrl", DbType.String).Value = dynamicObj.PageCoverUrl;
                    parameters.Add("@ZoneId", DbType.Int32).Value = dynamicObj.ZoneId;
                    parameters.Add("@Order", DbType.Int32).Value = dynamicObj.Order;
                    parameters.Add("@MainTitle", DbType.String).Value = dynamicObj.MainTitle;
                    parameters.Add("@Name", DbType.String).Value = dynamicObj.Name;
                    parameters.Add("@Duration", DbType.String).Value = dynamicObj.Duration;
                    parameters.Add("@Width", DbType.Int32).Value = dynamicObj.Dimension.Width;
                    parameters.Add("@Height", DbType.Int32).Value = dynamicObj.Dimension.Height;
                    parameters.Add("@Rotate", DbType.Int32).Value = dynamicObj.Dimension.Rotate;
                    parameters.Add("@ResolutionId", DbType.Int32).Value = dynamicObj.Resolution.Id;
                    parameters.Add("@AudioCodecId", DbType.Int32).Value = dynamicObj.AudioCodec.Id;
                    parameters.Add("@VideoCodecName", DbType.String).Value = dynamicObj.VideoCodecName;
                    parameters.Add("@FilePath", DbType.String).Value = dynamicObj.FilePath;
                    parameters.Add("@FileSize", DbType.String).Value = dynamicObj.FileSize;
                    parameters.Add("@CreateTime", DbType.String).Value = dynamicObj.CreateTime;
                    parameters.Add("@PlayNumber", DbType.String).Value = dynamicObj.PlayNumber;
                    parameters.Add("@DanmakuNumber", DbType.String).Value = dynamicObj.DanmakuNumber;
                    parameters.Add("@LikeNumber", DbType.String).Value = dynamicObj.LikeNumber;
                    parameters.Add("@CoinNumber", DbType.String).Value = dynamicObj.CoinNumber;
                    parameters.Add("@FavoriteNumber", DbType.String).Value = dynamicObj.FavoriteNumber;
                    parameters.Add("@ShareNumber", DbType.String).Value = dynamicObj.ShareNumber;
                    parameters.Add("@Description", DbType.String).Value = dynamicObj.Description;
                    parameters.Add("@UpOwnerMid", DbType.UInt64).Value = dynamicObj.UpOwner.Mid;

                }));
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Insert()发生异常: {0}", e);
                LogManager.Error($"{tableName}", e);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="header"></param>
        public new void Update(string uuid, dynamic dynamicObj)
        {
            try
            {
                InsertOrUpdateUpOwner(dynamicObj);
                InsertOrUpdateDimension(dynamicObj);
                InsertOrUpdateQuality(dynamicObj);
                InsertOrUpdateQuality(dynamicObj);
                string sql = $@"UPDATE {tableName}
                                    SET NeedDownloadContent = @NeedDownloadContent,
                                    Bvid = @Bvid,
                                    Avid = @Avid,
                                    Cid = @Cid,
                                    EpisodeId = @EpisodeId,
                                    CoverUrl = @CoverUrl,
                                    PageCoverUrl = @PageCoverUrl,
                                    ZoneId = @ZoneId,
                                    [Order] = @Order,
                                    MainTitle = @MainTitle,
                                    Name = @Name,
                                    Duration = @Duration,
                                    DimensionId = (SELECT DimensionId FROM {tableName_dimension} WHERE Width = @Width AND Height = @Height AND Rotate = @Rotate),
                                    VideoCodecName = @VideoCodecName,
                                    ResolutionId = @ResolutionId,
                                    AudioCodecId = @AudioCodecId,
                                    FilePath = @FilePath,
                                    FileSize = @FileSize,
                                    CreateTime = @CreateTime,
                                    PlayNumber = @PlayNumber,
                                    DanmakuNumber = @DanmakuNumber,
                                    LikeNumber = @LikeNumber,
                                    CoinNumber = @CoinNumber,
                                    FavoriteNumber = @FavoriteNumber,
                                    ShareNumber = @ShareNumber,
                                    Description = @Description,
                                    UpOwnerMid = @UpOwnerMid
                                WHERE Uuid = @Uuid;";

                dbHelper.ExecuteNonQuery(sql, new Action<SQLiteParameterCollection>((parameters) =>
                {
                    parameters.Add("@Uuid", DbType.String).Value = uuid;
                    parameters.Add("@NeedDownloadContent", DbType.String).Value = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicObj.NeedDownloadContent);
                    parameters.Add("@Bvid", DbType.String).Value = dynamicObj.Bvid;
                    parameters.Add("@Avid", DbType.Int64).Value = dynamicObj.Avid;
                    parameters.Add("@Cid", DbType.Int64).Value = dynamicObj.Cid;
                    parameters.Add("@EpisodeId", DbType.Int32).Value = dynamicObj.EpisodeId;
                    parameters.Add("@CoverUrl", DbType.String).Value = dynamicObj.CoverUrl;
                    parameters.Add("@PageCoverUrl", DbType.String).Value = dynamicObj.PageCoverUrl;
                    parameters.Add("@ZoneId", DbType.Int32).Value = dynamicObj.ZoneId;
                    parameters.Add("@Order", DbType.Int32).Value = dynamicObj.Order;
                    parameters.Add("@MainTitle", DbType.String).Value = dynamicObj.MainTitle;
                    parameters.Add("@Name", DbType.String).Value = dynamicObj.Name;
                    parameters.Add("@Duration", DbType.String).Value = dynamicObj.Duration;
                    parameters.Add("@Width", DbType.Int32).Value = dynamicObj.Dimension.Width;
                    parameters.Add("@Height", DbType.Int32).Value = dynamicObj.Dimension.Height;
                    parameters.Add("@Rotate", DbType.Int32).Value = dynamicObj.Dimension.Rotate;
                    parameters.Add("@ResolutionId", DbType.Int32).Value = dynamicObj.Resolution.Id;
                    parameters.Add("@AudioCodecId", DbType.Int32).Value = dynamicObj.AudioCodec.Id;
                    parameters.Add("@VideoCodecName", DbType.String).Value = dynamicObj.VideoCodecName;
                    parameters.Add("@FilePath", DbType.String).Value = dynamicObj.FilePath;
                    parameters.Add("@FileSize", DbType.String).Value = dynamicObj.FileSize;
                    parameters.Add("@CreateTime", DbType.String).Value = dynamicObj.CreateTime;
                    parameters.Add("@PlayNumber", DbType.String).Value = dynamicObj.PlayNumber;
                    parameters.Add("@DanmakuNumber", DbType.String).Value = dynamicObj.DanmakuNumber;
                    parameters.Add("@LikeNumber", DbType.String).Value = dynamicObj.LikeNumber;
                    parameters.Add("@CoinNumber", DbType.String).Value = dynamicObj.CoinNumber;
                    parameters.Add("@FavoriteNumber", DbType.String).Value = dynamicObj.FavoriteNumber;
                    parameters.Add("@ShareNumber", DbType.String).Value = dynamicObj.ShareNumber;
                    parameters.Add("@Description", DbType.String).Value = dynamicObj.Description;
                    parameters.Add("@UpOwnerMid", DbType.UInt64).Value = dynamicObj.UpOwner.Mid;

                }));
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Update()发生异常: {0}", e);
                LogManager.Error($"{tableName}", e);
            }
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public new List<dynamic> QueryAll()
        {
            string sql = GetQuerySql();
            return Query(sql);
        }

        /// <summary>
        /// 查询Uuid对应的数据
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public List<dynamic> QueryByUuids(List<string> uuids)
        {
            string sql = GetQuerySql() + $@" WHERE t1.Uuid in ('{string.Join(",", uuids)}')";
            return Query(sql);
        }

        /// <summary>
        /// 查询Uuid对应的数据
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public dynamic QueryByUuid(string uuid)
        {
            string sql = GetQuerySql() + $@" WHERE t1.Uuid in ('{uuid}')";
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
                        dynamic entity = new ExpandoObject();
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

            return objs.Count == 0 ? null : objs;
        }




        /// <summary>
        /// 创建查询sql
        /// </summary>
        /// <returns></returns>
        private string GetQuerySql()
        {
            return $@"SELECT t1.Uuid, t1.NeedDownloadContent, t1.Bvid, t1.Avid, t1.Cid, t1.EpisodeId, t1.CoverUrl, t1.PageCoverUrl, t1.ZoneId, t1.[Order], t1.MainTitle, t1.Name, 
                                    t1.Duration, t4.Width AS Dimension_Width, t4.Height AS Dimension_Height, t4.Rotate AS Dimension_Rotate, t1.VideoCodecName, t2.Id AS Resolution_Id, 
                                    t2.Name AS Resolution_Name, t3.Id AS AudioCodec_Id, t3.Name AS AudioCodec_Name, t1.FilePath, t1.FileSize, 
                                    t1.CreateTime, t1.PlayNumber, t1.DanmakuNumber, t1.LikeNumber, t1.CoinNumber, t1.FavoriteNumber, t1.ShareNumber, 
                                    t1.Description, t5.UpOwnerMid AS UpOwner_Mid, t5.Name AS UpOwner_Name, t5.Face AS UpOwner_Face
                            FROM ""{tableName}"" t1
                            LEFT JOIN ""{tableName_quality}"" t2 on t1.ResolutionId = t2.Id
                            LEFT JOIN ""{tableName_quality}"" t3 on t1.AudioCodecId = t3.Id
                            LEFT JOIN ""{tableName_dimension}"" t4 on t1.DimensionId = t4.DimensionId
                            LEFT JOIN ""{tableName_videoowner}"" t5 on t1.UpOwnerMid = t5.UpOwnerMid";
        }
        #endregion

    }
}
