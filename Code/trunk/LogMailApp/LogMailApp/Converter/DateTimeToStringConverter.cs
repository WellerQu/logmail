using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LogMailApp.Properties;

namespace LogMailApp.Converter
{
    class DateTimeToStringConverter : IValueConverter
    {
        private const string NO_CONVERTER = "";

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
                if (newValue.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    finalString = Resources.Today;
                }
                else if (newValue.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString())
                {
                    finalString = Resources.Yesterday;
                }
                else
                {
                    finalString = newValue.ToString("MM-dd");
                }
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
