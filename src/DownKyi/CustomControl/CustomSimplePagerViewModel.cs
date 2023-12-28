using Prism.Commands;
using System.ComponentModel;
using System.Windows;

namespace DownKyi.CustomControl
{
    public class CustomSimplePagerViewModel : INotifyPropertyChanged
    {
        public CustomSimplePagerViewModel(int current, int count)
        {
            CurrentPage = current;
            PageCount = count;
            CurPageLimitsVisibility = Visibility.Collapsed;

            SetView();
        }

        public CustomSimplePagerViewModel(int recordsCount)
        {
            ItemsPerPage = 1;
            RecordCount = recordsCount;
            CurrentPage = 1;
            
            PageCount = CalculatePageCount();
            CurPageLimitsVisibility = Visibility.Collapsed;

            SetView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // CurrentPage修改的回调
        public delegate bool CurrentChangedHandler(int old, int current, int limitNum);
        public event CurrentChangedHandler CurrentChanged;
        protected virtual bool OnCurrentChanged(int old, int current, int limitNum)
        {
            if (CurrentChanged == null)
            {
                return false;
            }
            else
            {
                return CurrentChanged.Invoke(old, current, limitNum);
            }
        }

        // ItemsPerPage修改的回调
        public delegate void CountChangedHandler(int current, int itemsPerPage);
        public event CountChangedHandler ItemsPerPageChanged;
        protected virtual void OnItemsPerPageChanged(int current, int itemsPerPage)
        {
            ItemsPerPageChanged?.Invoke(current, itemsPerPage);
        }

        #region 页面绑定属性

        private Visibility visibility;
        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Visibility"));
            }
        }

        private int recordCount;
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                if (recordCount != value) { PageCount = CalculatePageCount(); }
                recordCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RecordCount")); 
            }
        }

        private int itemsPerPage;
        /// <summary>
        /// 一页 显示的条数
        /// </summary>
        public int ItemsPerPage
        {
            get { return itemsPerPage; }
            set
            {
                int tempValue = value;
                if (value < 1) { tempValue = 1; }
                if (value > 200) { tempValue = 200; }
                if (itemsPerPage != tempValue) { itemsPerPage = tempValue; PageCount = CalculatePageCount(); }
                itemsPerPage = tempValue;

                OnItemsPerPageChanged(CurrentPage, itemsPerPage);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ItemsPerPage"));
            }
        }


        private int pagecount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return pagecount;
            }
            set
            {
                if (value < 1)
                {
                    Visibility = Visibility.Hidden;
                    //throw new Exception("数值不在允许的范围内。");
                    System.Console.WriteLine(value.ToString());
                }
                else
                {
                    pagecount = value;
                    if (value < CurrentPage) { CurrentPage = 1; }

                    Visibility = Visibility.Visible;

                    SetView();

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PageCount"));
                }
            }
        }

        public string PageTipText { get => Utils.DictionaryResource.GetString("CurrentPageText"); }

        private int currentpage;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (currentpage < 1) { currentpage = 1; }
                return currentpage;
            }
            set
            {
                int tempValue = value;
                if (PageCount > 0 && (value > PageCount || value < 1))
                {
                    if (value > PageCount)
                    {
                        tempValue = PageCount;
                    }
                    if (value < 1)
                    {
                        tempValue = 1;
                    }
                    //throw new Exception("数值不在允许的范围内。");
                }
                bool isSuccess = OnCurrentChanged(currentpage, tempValue, ItemsPerPage);
                if (true)
                {
                    currentpage = tempValue;

                    SetView();

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }
        }

        #region 首页 尾页 上一页 下一页 四个按钮的显示控制以及禁用和启用    条/业 修改文本框
        private Visibility curPageLimitsVisibility;
        /// <summary>
        /// 修改  单页条数的显示控制
        /// </summary>
        public Visibility CurPageLimitsVisibility
        {
            get { return curPageLimitsVisibility; }
            set
            {
                curPageLimitsVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurPageLimitsVisibility"));
            }
        }
        private Visibility previousVisibility;
        /// <summary>
        /// 上一页的显示控制
        /// </summary>
        public Visibility PreviousVisibility
        {
            get { return previousVisibility; }
            set
            {
                previousVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousVisibility"));
            }
        }

        private Visibility firstVisibility;
        /// <summary>
        /// 首页 的显示控制
        /// </summary>
        public Visibility FirstVisibility
        {
            get { return firstVisibility; }
            set
            {
                firstVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstVisibility"));
            }
        }

        private Visibility lastVisibility;
        /// <summary>
        /// 最后一页的显示控制
        /// </summary>
        public Visibility LastVisibility
        {
            get { return lastVisibility; }
            set
            {
                lastVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastVisibility"));
            }
        }

        private Visibility nextVisibility;
        /// <summary>
        /// 下一页的显示控制
        /// </summary>
        public Visibility NextVisibility
        {
            get { return nextVisibility; }
            set
            {
                nextVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextVisibility"));
            }
        }
        private bool isPreviousEnabled;
        /// <summary>
        /// 上一页 按钮的禁用启用
        /// </summary>
        public bool IsPreviousEnabled
        {
            get { return isPreviousEnabled; }
            set
            {
                isPreviousEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsPreviousEnabled"));
            }
        }

        private bool isFirstEnabled;
        /// <summary>
        /// 第一页 按钮的禁用启用
        /// </summary>
        public bool IsFirstEnabled
        {
            get { return isFirstEnabled; }
            set
            {
                isFirstEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsFirstEnabled"));
            }
        }
        private bool isNextEnabled;
        /// <summary>
        /// 下一页 按钮的禁用启用
        /// </summary>
        public bool IsNextEnabled
        {
            get { return isNextEnabled; }
            set
            {
                isNextEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsNextEnabled"));
            }
        }

        private bool isLastEnabled;
        /// <summary>
        /// 最后一页 按钮的禁用启用
        /// </summary>
        public bool IsLastEnabled
        {
            get { return isLastEnabled; }
            set
            {
                isLastEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLastEnabled"));
            }
        }
        #endregion
        #endregion

        #region 绑定命令
        private MyDelegateCommand previousCommand;
        public MyDelegateCommand PreviousCommand => previousCommand ?? (previousCommand = new MyDelegateCommand(PreviousExecuted));
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="obj"></param>
        public void PreviousExecuted(object obj)
        {
            CurrentPage -= 1;

            SetView();
        }

        private MyDelegateCommand firstCommand;
        public MyDelegateCommand FirstCommand => firstCommand ?? (firstCommand = new MyDelegateCommand(FirstExecuted));
        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="obj"></param>
        public void FirstExecuted(object obj)
        {
            CurrentPage = 1;

            SetView();
        }

        private MyDelegateCommand lastCommand;
        public MyDelegateCommand LastCommand => lastCommand ?? (lastCommand = new MyDelegateCommand(LastExecuted));
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="obj"></param>
        public void LastExecuted(object obj)
        {
            CurrentPage = PageCount;

            SetView();
        }

        private MyDelegateCommand nextCommand;
        public MyDelegateCommand NextCommand => nextCommand ?? (nextCommand = new MyDelegateCommand(NextExecuted));
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="obj"></param>
        public void NextExecuted(object obj)
        {
            CurrentPage += 1;

            SetView();
        }
        private DelegateCommand showCurPageLimitCommand;
        public DelegateCommand ShowCurPageLimitCommand => showCurPageLimitCommand ?? (showCurPageLimitCommand = new DelegateCommand(ShowCurPageLimitExecuted));
        /// <summary>
        /// 显示修改单页条数
        /// </summary>
        public void ShowCurPageLimitExecuted()
        {
            if (CurPageLimitsVisibility == Visibility.Collapsed)
            {
                CurPageLimitsVisibility = Visibility.Visible;
            }
            else
            {
                CurPageLimitsVisibility = Visibility.Collapsed;
            }
        }
        private DelegateCommand hideCurPageLimitCommand;
        public DelegateCommand HideCurPageLimitCommand => hideCurPageLimitCommand ?? (hideCurPageLimitCommand = new DelegateCommand(HideCurPageLimitExecuted));
        /// <summary>
        /// 显示修改单页条数
        /// </summary>
        public void HideCurPageLimitExecuted()
        {
            CurPageLimitsVisibility = Visibility.Collapsed;
        }
        #endregion
        /// <summary>
        /// 计算页数
        /// </summary>
        /// <returns></returns>
        private int CalculatePageCount()
        {
            if (ItemsPerPage == 0 || RecordCount == 0) { return 0; }
            return RecordCount / ItemsPerPage + (RecordCount % ItemsPerPage == 0 ? 0 : 1);
        }

        /// <summary>
        /// 控制显示
        /// </summary>
        private void SetView()
        {
            PreviousVisibility = Visibility.Visible;
            FirstVisibility = Visibility.Visible; 
            LastVisibility = Visibility.Visible;
            NextVisibility = Visibility.Visible;
            if (CurrentPage == 1)
            {
                IsFirstEnabled = false;
                IsPreviousEnabled = false;
            }
            else
            {
                IsFirstEnabled = true;
                IsPreviousEnabled = true;
            }
            if (currentpage == PageCount)
            {
                IsLastEnabled = false;
                IsNextEnabled = false;
            }
            else
            {
                IsLastEnabled = true;
                IsNextEnabled = true;
            }
            //// 控制Current左边的控件
            //if (Current == 1)
            //{
            //    PreviousVisibility = Visibility.Collapsed;
            //    FirstVisibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    PreviousVisibility = Visibility.Visible;
            //    FirstVisibility = Visibility.Visible;
            //}

            //// 控制Current右边的控件
            //if (Current == Count)
            //{
            //    LastVisibility = Visibility.Collapsed;
            //    NextVisibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    LastVisibility = Visibility.Visible;
            //    NextVisibility = Visibility.Visible;
            //}
        }
    }
}
