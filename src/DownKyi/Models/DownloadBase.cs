using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DownKyi.Models
{
    [Serializable]
    public class DownloadBase
    {
        public DownloadBase()
        {
            // 唯一id
            Uuid = Guid.NewGuid().ToString("N");

            // 初始化需要下载的内容
            NeedDownloadContent = new Dictionary<string, bool>
            {
                { "downloadAudio", true },
                { "downloadVideo", true },
                { "downloadDanmaku", true },
                { "downloadSubtitle", true },
                { "downloadCover", true }
            };
        }

        /// <summary>
        /// 静态方法，将 dynamic 转化为 DownloadBase 类型
        /// </summary>
        /// <param name="dynamicObj"></param>
        /// <returns></returns>
        public static DownloadBase FromDynamic(dynamic dynamicObj)
        {
            if (dynamicObj == null)
            {
                return null;
            }
            JObject jObject = JObject.Parse(dynamicObj.NeedDownloadContent);
            Dictionary<string, bool> needDownloadContent = jObject.ToObject<Dictionary<string, bool>>();

            DownloadBase downloadBase = new DownloadBase
            {
                Uuid = dynamicObj.Uuid as string,
                NeedDownloadContent = needDownloadContent,

                Bvid = dynamicObj.Bvid as string,
                Avid = dynamicObj.Avid as long? ?? 0,
                Cid = dynamicObj.Cid as long? ?? 0,
                EpisodeId = dynamicObj.EpisodeId as long? ?? 0,

                CoverUrl = dynamicObj.CoverUrl as string,
                PageCoverUrl = dynamicObj.PageCoverUrl as string,

                ZoneId = dynamicObj.ZoneId as int? ?? 0,
                Order = dynamicObj.ORDER as int? ?? 0,

                MainTitle = dynamicObj.MainTitle as string,
                Name = dynamicObj.Name as string,
                Duration = dynamicObj.Duration as string,

                Dimension = new Dimension 
                {
                    Width = dynamicObj.Dimension_Width as int? ?? 0,
                    Height = dynamicObj.Dimension_Height as int? ?? 0,
                    Rotate = dynamicObj.Dimension_Rotate as int? ?? 0,
                },
                VideoCodecName = dynamicObj.VideoCodecName as string,

                Resolution = new Quality
                {
                    Id = dynamicObj.Resolution_Id as int? ?? 0,
                    Name = dynamicObj.Resolution_Name as string
                },

                AudioCodec = new Quality
                {
                    Id = dynamicObj.AudioCodec_Id as int? ?? 0,
                    Name = dynamicObj.AudioCodec_Name as string
                },

                FilePath = dynamicObj.FilePath as string,
                FileSize = dynamicObj.FileSize as string,
                CreateTime = dynamicObj.CreateTime as string,
                PlayNumber = dynamicObj.PlayNumber as string,
                DanmakuNumber = dynamicObj.DanmakuNumber as string,
                LikeNumber = dynamicObj.LikeNumber as string,
                CoinNumber = dynamicObj.CoinNumber as string,
                FavoriteNumber = dynamicObj.FavoriteNumber as string,
                ShareNumber = dynamicObj.ShareNumber as string,
                Description = dynamicObj.Description as string,

                UpOwner = new VideoOwner
                {
                    Mid = dynamicObj.UpOwner_Mid as long? ?? 0,
                    Name = dynamicObj.UpOwner_Name as string,
                    Face = dynamicObj.UpOwner_Face as string
                }
            };

            return downloadBase;
        }
        /// <summary>
        /// 此条下载项的id
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 需要下载的内容
        /// </summary>
        public Dictionary<string, bool> NeedDownloadContent { get; set; }

        /// <summary>
        /// 视频的id
        /// </summary>
        public string Bvid { get; set; }
        public long Avid { get; set; }
        public long Cid { get; set; }
        public long EpisodeId { get; set; }

        /// <summary>
        /// 视频封面的url
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 视频page的封面的url
        /// </summary>
        public string PageCoverUrl { get; set; }

        /// <summary>
        /// 分区id
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// 视频序号
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 视频主标题
        /// </summary>
        public string MainTitle { get; set; }

        /// <summary>
        /// 视频标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 视频的宽度 高度 旋转角度
        /// </summary>
        public Dimension Dimension { get; set; }
        /// <summary>
        /// 视频编码名称，AVC、HEVC
        /// </summary>
        public string VideoCodecName { get; set; }

        /// <summary>
        /// 视频画质
        /// </summary>
        public Quality Resolution { get; set; }

        /// <summary>
        /// 音频编码
        /// </summary>
        public Quality AudioCodec { get; set; }

        /// <summary>
        /// 文件路径，不包含扩展名，所有内容均以此路径下载
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 视频上传时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 播放数量
        /// </summary>
        public string PlayNumber { get; set; }

        /// <summary>
        /// 弹幕数量
        /// </summary>
        public string DanmakuNumber { get; set; }

        /// <summary>
        /// 喜欢/点赞数量
        /// </summary>
        public string LikeNumber { get; set; }

        /// <summary>
        /// 投币数量
        /// </summary>
        public string CoinNumber { get; set; }

        /// <summary>
        /// 收藏数量
        /// </summary>
        public string FavoriteNumber { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public string ShareNumber { get; set; }

        /// <summary>
        /// 视频描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 视频的UP主
        /// </summary>
        public VideoOwner UpOwner { get; set; } 
    }
}
