using DownKyi.Core.FileName;
using DownKyi.Core.Settings.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        
        // 默认下载目录
        private readonly string saveUpupRootPath = Path.Combine(Environment.CurrentDirectory, "UpupResource");

        // 历史下载目录
        private readonly List<string> historyUpupRootPaths = new List<string>();

        // 是否使用默认下载目录，如果是，则每次点击下载选中项时不再询问下载目录
        private readonly AllowStatus isUseSaveUpupRootPath = AllowStatus.NO;

        // 默认使用者名字
        private readonly string defaultUseName = "小怪兽";

        // 默认版本号
        private readonly string ver = "1.0.0";

        // 是否将视频复制到生成目录
        private readonly AllowStatus isMoveVideoUpDirectory = AllowStatus.YES;

        public AllowStatus GetIsMoveVideoUpDirectory()
        {
            appSettings = GetSettings();
            if (appSettings.UpupInfo.IsMoveVideoUpDirectory == AllowStatus.NONE)
            {
                SetIsMoveVideoUpDirectory(isMoveVideoUpDirectory);
                return isMoveVideoUpDirectory;
            }
            return appSettings.UpupInfo.IsMoveVideoUpDirectory;
        }

        /// <summary>
        /// 设置 是否将视频复制到生成目录
        /// </summary>
        /// <param name="allow"></param>
        /// <returns></returns>
        public bool SetIsMoveVideoUpDirectory(AllowStatus allow)
        {
            appSettings.UpupInfo.IsMoveVideoUpDirectory = allow;
            return SetSettings();
        }

        public string GetVer()
        {
            appSettings = GetSettings();
            if (string.IsNullOrEmpty(appSettings.UpupInfo.Ver))
            {
                SetVer(ver);
                return ver;
            }
            return appSettings.UpupInfo.Ver;
        }
        public bool SetVer(string _ver)
        {
            appSettings.UpupInfo.Ver = _ver;
            return SetSettings();
        }
        /// <summary>
        /// 获取默认upup使用者名字
        /// </summary>
        /// <returns></returns>
        public string GetDefaultUseName()
        {
            appSettings = GetSettings();
            if (string.IsNullOrEmpty(appSettings.UpupInfo.UpupDefaultUseName))
            {
                SetDefaultUseName(defaultUseName);
                return defaultUseName;
            }
            return appSettings.UpupInfo.UpupDefaultUseName;
        }

        /// <summary>
        /// 设置默认使用者名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SetDefaultUseName(string name)
        {
            appSettings.UpupInfo.UpupDefaultUseName = name;
            return SetSettings();
        }

        /// <summary>
        /// 获取生成目录
        /// </summary>
        /// <returns></returns>
        public string GetSaveUpupRootPath()
        {
            appSettings = GetSettings();
            if (appSettings.UpupInfo.SaveUpupRootPath == null)
            {
                // 第一次获取，先设置默认值
                SetSaveUpupRootPath(saveUpupRootPath);
                return saveUpupRootPath;
            }
            return appSettings.UpupInfo.SaveUpupRootPath;
        }

        /// <summary>
        /// 设置生成目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SetSaveUpupRootPath(string path)
        {
            appSettings.UpupInfo.SaveUpupRootPath = path;
            return SetSettings();
        }

        /// <summary>
        /// 获取历史生成目录
        /// </summary>
        /// <returns></returns>
        public List<string> GetHistoryUpupRootPaths()
        {
            appSettings = GetSettings();
            if (appSettings.UpupInfo.HistoryUpupRootPaths == null)
            {
                // 第一次获取，先设置默认值
                SetHistoryVUpupRootPaths(historyUpupRootPaths);
                return historyUpupRootPaths;
            }
            return appSettings.UpupInfo.HistoryUpupRootPaths;
        }

        /// <summary>
        /// 设置历史下载目录
        /// </summary>
        /// <param name="historyPaths"></param>
        /// <returns></returns>
        public bool SetHistoryVUpupRootPaths(List<string> historyPaths)
        {
            appSettings.UpupInfo.HistoryUpupRootPaths = historyPaths;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否使用默认生成upup目录
        /// </summary>
        /// <returns></returns>
        public AllowStatus GetIsUseSaveUpupRootPath()
        {
            appSettings = GetSettings();
            if (appSettings.UpupInfo.IsUseSaveUpupRootPath == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                SetIsUseSaveUpupRootPath(isUseSaveUpupRootPath);
                return isUseSaveUpupRootPath;
            }
            return appSettings.UpupInfo.IsUseSaveUpupRootPath;
        }

        /// <summary>
        /// 设置是否使用默认生成upup目录
        /// </summary>
        /// <param name="isUseSaveUpupRootPath"></param>
        /// <returns></returns>
        public bool SetIsUseSaveUpupRootPath(AllowStatus isUseSaveUpupRootPath)
        {
            appSettings.UpupInfo.IsUseSaveUpupRootPath = isUseSaveUpupRootPath;
            return SetSettings();
        }

    }
}
