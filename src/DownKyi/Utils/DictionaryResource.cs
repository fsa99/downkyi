using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Resources;

namespace DownKyi.Utils
{
    public static class DictionaryResource
    {

        /// <summary>
        /// 从资源获取颜色的16进制字符串
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetColor(string resourceKey)
        {
            return Application.Current == null ? "#00000000" : ((Color)Application.Current.Resources[resourceKey]).ToString();
        }

        /// <summary>
        /// 从资源获取字符串
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetString(string resourceKey)
        {
            return Application.Current == null ? "" : (string)Application.Current.Resources[resourceKey];
        }

        /// <summary>
        /// 根据languageCode切换界面语言
        /// </summary>
        /// <param name="languageCode">语言-国家代码</param>
        public static void LoadLanguage(string languageCode)
        {
            LoadXamlDictionary(languageCode, "Languages");
        }

        /// <summary>
        /// 获取当前的 Language 资源
        /// </summary>
        /// <returns></returns>
        public static string GetCurLanguage()
        {
            string package = "Languages";
            var languagesUri = GetXamlMergedDictionary(package)[0].Source;
            return System.IO.Path.GetFileName(languagesUri.LocalPath);
        }

        /// <summary>
        /// 获取所有的 Language 资源
        /// </summary>
        /// <returns></returns>
        public static string GetAllLanguage()
        {
            string package = "Languages";
            

            return string.Empty;
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="theme">主题</param>
        public static void LoadTheme(string theme)
        {
            LoadXamlDictionary(theme, "Themes");
        }

        /// <summary>
        /// 获取当前 资源字典（已经合并的）
        /// </summary>
        /// <param name="package">文件所在包路径</param>
        /// <returns></returns>
        private static List<ResourceDictionary> GetXamlMergedDictionary(string package)
        {
            var dictionariesToRemove = new List<ResourceDictionary>();

            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            {
                if (dictionary.Source.ToString().Contains($"{package}"))
                {
                    dictionariesToRemove.Add(dictionary);
                }
            }

            return dictionariesToRemove;
        }


        /// <summary>
        /// 更换xaml资源字典
        /// </summary>
        /// <param name="fileName">xaml文件名，不包含扩展名</param>
        /// <param name="package">文件所在包（路径）</param>
        private static void LoadXamlDictionary(string fileName, string package)
        {
            if (string.IsNullOrEmpty(fileName) == false)
            {
                var dictionariesToRemove = new List<ResourceDictionary>();

                foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
                {
                    if (dictionary.Source.ToString().Contains($"{package}"))
                    {
                        dictionariesToRemove.Add(dictionary);
                    }
                }

                foreach (var item in dictionariesToRemove)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(item);
                }

                var languageDictionary = new ResourceDictionary()
                {
                    Source = new Uri($"pack://application:,,,/DownKyi;component/{package}/{fileName}.xaml")
                };

                Application.Current.Resources.MergedDictionaries.Add(languageDictionary);
            }
        }
    }
}
