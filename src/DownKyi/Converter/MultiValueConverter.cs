using System;
using System.Globalization;
using System.Windows.Data;

namespace DownKyi.Converter
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3)
            {
                string pageTipText = values[0]?.ToString();
                int currentPage = System.Convert.ToInt32(values[1]);
                int pageCount = System.Convert.ToInt32(values[2]);

                return $"{pageTipText}:{currentPage}/{pageCount}";
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
