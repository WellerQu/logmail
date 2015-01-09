using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogMailApp.Converter
{
    class DateToShortStringConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string finalString = string.Empty;
            string paramterStr = parameter != null ? parameter.ToString() : "0";
            int offset = 0;

            DateTime? current = value as DateTime?;

            if (current != null && int.TryParse(paramterStr, out offset))
            {
                DateTime newValue = current.Value.AddDays(0 - offset);
                finalString = newValue.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return finalString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
