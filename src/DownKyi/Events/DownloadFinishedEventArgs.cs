using DownKyi.ViewModels.DownloadManager;
using System;

namespace DownKyi.Events
{
    public class DownloadFinishedEventArgs : EventArgs
    {
        public DownloadedItem DownloadedItem { get; }

        public DownloadFinishedEventArgs(DownloadedItem downloadedItem)
        {
            DownloadedItem = downloadedItem;
        }
    }
}
