﻿using DownKyi.Core.Settings;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadFinishedViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloadFinished";

        #region 页面属性申明

        private ObservableCollection<DownloadedItem> downloadedList;
        public ObservableCollection<DownloadedItem> DownloadedList
        {
            get => downloadedList;
            set => SetProperty(ref downloadedList, value);
        }

        #endregion

        public ViewDownloadFinishedViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // 初始化DownloadedList
            DownloadedList = App.DownloadedList;
        }

        #region 命令申明

        // 下载完成列表排序事件
        private DelegateCommand<object> finishedSortCommand;
        public DelegateCommand<object> FinishedSortCommand => finishedSortCommand ?? (finishedSortCommand = new DelegateCommand<object>(ExecuteFinishedSortCommand));

        /// <summary>
        /// 下载完成列表排序事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteFinishedSortCommand(object parameter)
        {
            if (!(parameter is int index)) { return; }

            switch (index)
            {
                case 1:
                    App.SortDownloadedList(DownloadFinishedSort.DOWNLOAD);
                    break;
                case 2:
                    App.SortDownloadedList(DownloadFinishedSort.NUMBER);
                    break;
            }
        }

        // 清空下载完成列表事件
        private DelegateCommand clearAllDownloadedCommand;
        public DelegateCommand ClearAllDownloadedCommand => clearAllDownloadedCommand ?? (clearAllDownloadedCommand = new DelegateCommand(ExecuteClearAllDownloadedCommand));

        /// <summary>
        /// 清空下载完成列表事件
        /// </summary>
        private void ExecuteClearAllDownloadedCommand()
        {
        }

        #endregion

    }
}