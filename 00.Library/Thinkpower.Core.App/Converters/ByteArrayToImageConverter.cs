using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Converters
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    byte[] bytes = (byte[])value;
                    ImageSource retImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
                    return retImageSource;
                }

                if (parameter != null)
                {
                    string fillerIcon = (string)parameter;
                    ImageSource retImageSource = ImageSource.FromFile(fillerIcon);
                    return retImageSource;
                }
            }
            catch (Exception)
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
