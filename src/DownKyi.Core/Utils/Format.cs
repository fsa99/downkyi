using System.Linq;
using System.Text.RegularExpressions;

namespace DownKyi.Core.Utils
{
    public static class Format
    {
        /// <summary>
        /// 将不同格式的时间字符串转换为总秒数  用于排序 比较
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ConvertTimeToSeconds(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            string[] parts = input.Split(':');
            int hours = 0, minutes = 0, seconds = 0;

            if (parts.Length == 1)
            {
                // 只有秒
                if (parts[0].EndsWith("s"))
                {
                    int.TryParse(parts[0].TrimEnd('s'), out seconds);
                }
                else
                {
                    int.TryParse(parts[0], out seconds);
                }
            }
            else if (parts.Length == 2)
            {
                // 分钟和秒
                int.TryParse(parts[0], out minutes);
                if (parts[1].EndsWith("s"))
                {
                    int.TryParse(parts[1].TrimEnd('s'), out seconds);
                }
                else
                {
                    int.TryParse(parts[1], out seconds);
                }
            }
            else if (parts.Length == 3)
            {
                // 小时、分钟和秒
                int.TryParse(parts[0], out hours);
                int.TryParse(parts[1], out minutes);
                if (parts[2].EndsWith("s"))
                {
                    int.TryParse(parts[2].TrimEnd('s'), out seconds);
                }
                else
                {
                    int.TryParse(parts[2], out seconds);
                }
            }

            // 计算总秒数
            return hours * 3600 + minutes * 60 + seconds;
        }




        /// <summary>
        /// 格式化Duration时间
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string FormatDuration(long duration)
        {
            string formatDuration;
            if (duration / 60 > 0)
            {
                long dur = duration / 60;
                if (dur / 60 > 0)
                {
                    //formatDuration = $"{dur / 60}h{dur % 60}m{duration % 60}s";
                    formatDuration = $"{dur / 60}:{dur % 60}:{duration % 60}";
                }
                else
                {
                    //formatDuration = $"{duration / 60}m{duration % 60}s";
                    formatDuration = $"{duration / 60}:{duration % 60}";
                }
            }
            else
            {
                formatDuration = $"{duration}s";
            }
            return formatDuration;
        }

        /// <summary>
        /// 格式化Duration时间，格式为00:00:00
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string FormatDuration2(long duration)
        {
            string formatDuration;
            if (duration / 60 > 0)
            {
                long dur = duration / 60;
                if (dur / 60 > 0)
                {
                    formatDuration = string.Format("{0:D2}", dur / 60) + ":" + string.Format("{0:D2}", dur % 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
                else
                {
                    formatDuration = "00:" + string.Format("{0:D2}", duration / 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
            }
            else
            {
                formatDuration = "00:00:" + string.Format("{0:D2}", duration);
            }
            return formatDuration;
        }

        /// <summary>
        /// 格式化Duration时间，格式为00:00
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string FormatDuration3(long duration)
        {
            string formatDuration;
            if (duration / 60 > 0)
            {
                long dur = duration / 60;
                if (dur / 60 > 0)
                {
                    formatDuration = string.Format("{0:D2}", dur / 60) + ":" + string.Format("{0:D2}", dur % 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
                else
                {
                    formatDuration = string.Format("{0:D2}", duration / 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
            }
            else
            {
                formatDuration = "00:" + string.Format("{0:D2}", duration);
            }
            return formatDuration;
        }

        /// <summary>
        /// 格式化数字，超过10000的数字将单位改为万，超过100000000的数字将单位改为亿，并保留1位小数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatNumber(long number)
        {
            if (number > 99999999)
            {
                return (number / 100000000.0f).ToString("F1") + "亿";
            }

            if (number > 9999)
            {
                return (number / 10000.0f).ToString("F1") + "万";
            }
            else
            {
                return number.ToString();
            }
        }

        /// <summary>
        /// 格式化网速
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static string FormatSpeed(float speed)
        {
            string formatSpeed;
            if (speed <= 0)
            {
                formatSpeed = "0B/s";
            }
            else if (speed < 1024)
            {
                formatSpeed = string.Format("{0:F2}", speed) + "B/s";
            }
            else if (speed < 1024 * 1024)
            {
                formatSpeed = string.Format("{0:F2}", speed / 1024) + "KB/s";
            }
            else
            {
                formatSpeed = string.Format("{0:F2}", speed / 1024 / 1024) + "MB/s";
            }
            return formatSpeed;
        }

        /// <summary>
        /// 格式化字节大小为数字，用于文件大小的比较
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static double ParseFileSize(string fileSize)
        {
            if (string.IsNullOrEmpty(fileSize)){ return 0; }
            string numericPart = new string(fileSize.Where(char.IsDigit).ToArray());

            if (double.TryParse(numericPart, out double result))
            {
                return result;
            }
            // 解析失败时，默认返回 0
            return 0;
        }

        /// <summary>
        /// 格式化字节大小，可用于文件大小的显示
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string FormatFileSize(long fileSize)
        {
            string formatFileSize;
            if (fileSize <= 0)
            {
                formatFileSize = "0B";
            }
            else if (fileSize < 1024)
            {
                formatFileSize = fileSize.ToString() + "B";
            }
            else if (fileSize < 1024 * 1024)
            {
                formatFileSize = (fileSize / 1024.0).ToString("#.##") + "KB";
            }
            else if (fileSize < 1024 * 1024 * 1024)
            {
                formatFileSize = (fileSize / 1024.0 / 1024.0).ToString("#.##") + "MB";
            }
            else
            {
                formatFileSize = (fileSize / 1024.0 / 1024.0 / 1024.0).ToString("#.##") + "GB";
            }
            return formatFileSize;
        }

        /// <summary>
        /// 去除非法字符
        /// </summary>
        /// <param name="originName"></param>
        /// <returns></returns>
        public static string FormatFileName(string originName)
        {
            string destName = originName;

            // Windows中不能作为文件名的字符
            destName = destName.Replace("\\", " ");
            destName = destName.Replace("/", " ");
            destName = destName.Replace(":", " ");
            destName = destName.Replace("*", " ");
            destName = destName.Replace("?", " ");
            destName = destName.Replace("\"", " ");
            destName = destName.Replace("<", " ");
            destName = destName.Replace(">", " ");
            destName = destName.Replace("|", " ");

            // 转义字符
            destName = destName.Replace("\a", "");
            destName = destName.Replace("\b", "");
            destName = destName.Replace("\f", "");
            destName = destName.Replace("\n", "");
            destName = destName.Replace("\r", "");
            destName = destName.Replace("\t", "");
            destName = destName.Replace("\v", "");

            // 控制字符
            destName = Regex.Replace(destName, @"\p{C}+", string.Empty);

            // 如果只有空白字符、dot符
            if (destName == " " || destName == ".")
            {
                return "[empty title]";
            }

            // 移除前导和尾部的空白字符、dot符
            int i, j;
            for (i = 0; i < destName.Length; i++)
            {
                if (destName[i] != ' ' && destName[i] != '.')
                {
                    break;
                }
            }
            for (j = destName.Length - 1; j >= 0; j--)
            {
                if (destName[j] != ' ' && destName[j] != '.')
                {
                    break;
                }
            }
            return destName.Substring(i, j - i + 1);
        }

    }
}
