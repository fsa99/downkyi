namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        // 默认下载完成后的操作
        private readonly AfterDownloadOperation afterDownload = AfterDownloadOperation.NONE;

        // 是否监听剪贴板
        private readonly AllowStatus isListenClipboard = AllowStatus.YES;

        // 视频详情页面是否自动解析
        private readonly AllowStatus isAutoParseVideo = AllowStatus.NO;

        // 默认的视频解析项
        private readonly ParseScope parseScope = ParseScope.NONE;

        // 解析后自动下载解析视频
        private readonly AllowStatus isAutoDownloadAll = AllowStatus.NO;

        // 下载完成列表排序
        private readonly DownloadFinishedSort finishedSort = DownloadFinishedSort.DOWNLOAD;

        // 排序方式
        private readonly SortOrder sortOrder = SortOrder.ASCENDING;

        // 记录上次修改的单页显示条数
        private readonly int lastItemsPerPage = 20;

        /// <summary>
        /// 获取下载完成后的操作
        /// </summary>
        /// <returns></returns>
        public AfterDownloadOperation GetAfterDownloadOperation()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.AfterDownload == AfterDownloadOperation.NOT_SET)
            {
                // 第一次获取，先设置默认值
                SetAfterDownloadOperation(afterDownload);
                return afterDownload;
            }
            return appSettings.Basic.AfterDownload;
        }

        /// <summary>
        /// 设置下载完成后的操作
        /// </summary>
        /// <param name="afterDownload"></param>
        /// <returns></returns>
        public bool SetAfterDownloadOperation(AfterDownloadOperation afterDownload)
        {
            appSettings.Basic.AfterDownload = afterDownload;
            return SetSettings();
        }

        /// <summary>
        /// 是否监听剪贴板
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsListenClipboard()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.IsListenClipboard == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                IsListenClipboard(isListenClipboard);
                return isListenClipboard;
            }
            return appSettings.Basic.IsListenClipboard;
        }

        /// <summary>
        /// 是否监听剪贴板
        /// </summary>
        /// <param name="isListen"></param>
        /// <returns></returns>
        public bool IsListenClipboard(AllowStatus isListen)
        {
            appSettings.Basic.IsListenClipboard = isListen;
            return SetSettings();
        }

        /// <summary>
        /// 视频详情页面是否自动解析
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsAutoParseVideo()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.IsAutoParseVideo == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                IsAutoParseVideo(isAutoParseVideo);
                return isAutoParseVideo;
            }
            return appSettings.Basic.IsAutoParseVideo;
        }

        /// <summary>
        /// 视频详情页面是否自动解析
        /// </summary>
        /// <param name="IsAuto"></param>
        /// <returns></returns>
        public bool IsAutoParseVideo(AllowStatus IsAuto)
        {
            appSettings.Basic.IsAutoParseVideo = IsAuto;
            return SetSettings();
        }

        /// <summary>
        /// 获取视频解析项
        /// </summary>
        /// <returns></returns>
        public ParseScope GetParseScope()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.ParseScope == ParseScope.NOT_SET)
            {
                // 第一次获取，先设置默认值
                SetParseScope(parseScope);
                return parseScope;
            }
            return appSettings.Basic.ParseScope;
        }

        /// <summary>
        /// 设置视频解析项
        /// </summary>
        /// <param name="parseScope"></param>
        /// <returns></returns>
        public bool SetParseScope(ParseScope parseScope)
        {
            appSettings.Basic.ParseScope = parseScope;
            return SetSettings();
        }

        /// <summary>
        /// 解析后是否自动下载解析视频
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsAutoDownloadAll()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.IsAutoDownloadAll == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                IsAutoDownloadAll(isAutoDownloadAll);
                return isAutoDownloadAll;
            }
            return appSettings.Basic.IsAutoDownloadAll;
        }

        /// <summary>
        /// 解析后是否自动下载解析视频
        /// </summary>
        /// <param name="isAutoDownloadAll"></param>
        /// <returns></returns>
        public bool IsAutoDownloadAll(AllowStatus isAutoDownloadAll)
        {
            appSettings.Basic.IsAutoDownloadAll = isAutoDownloadAll;
            return SetSettings();
        }

        /// <summary>
        /// 获取下载完成列表排序
        /// </summary>
        /// <returns></returns>
        public DownloadFinishedSort GetDownloadFinishedSort()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.DownloadFinishedSort == DownloadFinishedSort.NOT_SET)
            {
                // 第一次获取，先设置默认值
                SetDownloadFinishedSort(finishedSort);
                return finishedSort;
            }
            return appSettings.Basic.DownloadFinishedSort;
        }

        /// <summary>
        /// 设置下载完成列表排序
        /// </summary>
        /// <param name="finishedSort"></param>
        /// <returns></returns>
        public bool SetDownloadFinishedSort(DownloadFinishedSort finishedSort)
        {
            appSettings.Basic.DownloadFinishedSort = finishedSort;
            return SetSettings();
        }

        /// <summary>
        /// 获取排序方式
        /// </summary>
        /// <returns></returns>
        public SortOrder GetSortOrder()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.SortOrder == SortOrder.NOT_SET)
            {
                // 第一次获取，先设置默认值
                SetSortOrder(sortOrder);
                return sortOrder;
            }
            return appSettings.Basic.SortOrder;
        }

        /// <summary>
        /// 设置排序方式
        /// </summary>
        /// <param name="finishedSort"></param>
        /// <returns></returns>
        public bool SetSortOrder(SortOrder sortOrder)
        {
            appSettings.Basic.SortOrder = sortOrder;
            return SetSettings();
        }

        /// <summary>
        /// 获取单页显示条数
        /// </summary>
        /// <returns></returns>
        public int GetLastItemsPerPage()
        {
            appSettings = GetSettings();
            if (appSettings.Basic.LastItemsPerPage == 0)
            {
                SetLastItemsPerPage(lastItemsPerPage);
            }
            return appSettings.Basic.LastItemsPerPage;
        }

        /// <summary>
        /// 设置单页显示条数
        /// </summary>
        /// <param name="finishedSort"></param>
        /// <returns></returns>
        public bool SetLastItemsPerPage(int _lastItemsPerPage)
        {
            appSettings.Basic.LastItemsPerPage = _lastItemsPerPage;
            return SetSettings();
        }
    }
}
