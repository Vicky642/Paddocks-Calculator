using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mypaddocks.Converters
{
    public class CoordinatesFormatterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double[] coords && coords.Length == 4)
            {
                return $"TL:({coords[0]:N1},{coords[1]:N1}) BR:({coords[2]:N1},{coords[3]:N1})";
            }
            //string k = y.ToString("C", new CultureInfo("en-KE"));
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
