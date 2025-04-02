using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mypaddocks.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int ? (int)value : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Handle conversion from string to int safely
            if (value is string stringValue && int.TryParse(stringValue, out int result))
            {
                return result;
            }
            return 0; // Default to 0 if parsing fails
        }
    }
}
