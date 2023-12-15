using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DownKyi.Utils
{
    /// <summary>
    /// 剪贴板监听器类，用于在 Windows 操作系统中监听剪贴板的更新事件。
    /// </summary>
    public class ClipboardHooker : IDisposable
    {
        // 剪贴板更新消息的常量值
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        // 外部函数，向系统注册剪贴板格式监听器
        [DllImport("User32.dll")]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        // 与窗口句柄关联的 HwndSource 实例
        private readonly HwndSource _hwndSource = null;

        /// <summary>
        /// 析构函数，释放资源
        /// </summary>
        public void Dispose()
        {
            _hwndSource?.Dispose();
        }

        /// <summary>
        /// 构造函数，初始化剪贴板监听器
        /// </summary>
        /// <param name="window">要监听剪贴板事件的窗口对象</param>
        public ClipboardHooker(Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            _hwndSource = HwndSource.FromHwnd(helper.Handle);
            bool r = AddClipboardFormatListener(_hwndSource.Handle);
            if (r)
            {
                _hwndSource.AddHook(new HwndSourceHook(OnHooked));
            }
        }

        /// <summary>
        /// 剪贴板更新事件的消息处理方法
        /// </summary>
        private IntPtr OnHooked(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // 当剪贴板更新消息发生时，触发 ClipboardUpdated 事件
            if (msg == WM_CLIPBOARDUPDATE)
            {
                ClipboardUpdated?.Invoke(this, EventArgs.Empty);
                return IntPtr.Zero;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 剪贴板更新事件
        /// </summary>
        public event EventHandler ClipboardUpdated;
    }
}

