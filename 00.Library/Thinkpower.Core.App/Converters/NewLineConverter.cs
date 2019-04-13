using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Converters
{
    public class NewLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return str
                .Replace("\\r\\n", Environment.NewLine)
                .Replace("\\n", Environment.NewLine)
                .Replace("\\r", Environment.NewLine);

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
