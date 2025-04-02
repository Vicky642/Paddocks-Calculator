using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Mypaddocks.Converters
{
   public class ContrastColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is SolidColorBrush backgroundBrush)
                {
                    double luminance = (0.2126 * backgroundBrush.Color.R +
                                     0.7152 * backgroundBrush.Color.G +
                                     0.0722 * backgroundBrush.Color.B) / 255;
                    return luminance > 0.5 ? Brushes.Black : Brushes.White;
                }
                return Brushes.Black;
            }
            catch
            {
                return Brushes.Black;
            }
        }
       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
