using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Converters
{
    public class Base64ToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    var base64Str = (string)value;
                    if (!string.IsNullOrEmpty(base64Str))
                    {
                        byte[] fileBytes = System.Convert.FromBase64String(base64Str);
                        ImageSource retImageSource = ImageSource.FromStream(() => new MemoryStream(fileBytes));
                        return retImageSource;
                    }
                }

                if (parameter != null)
                {
                    string fillerIcon = (string)parameter;
                    ImageSource retImageSource = ImageSource.FromFile(fillerIcon);
                    return retImageSource;
                }
            }
            catch (Exception ex)
            {

            }

            return null;


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
