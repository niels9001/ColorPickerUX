using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ColorPickerUX.Converters
{

    public sealed class ParameterToSnippetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string Unformatted = (string)value;
            string Formatted = Unformatted.Replace("%1", "143")
                .Replace("%2", "51")
                .Replace("%3", "47")
                .Replace("%4", "1")
                .Replace("%5", "8f332f")
                .Replace("%6", "2")
                .Replace("%7", "67")
                .Replace("%8", "56")
                .Replace("%9", "56");       
            return Formatted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}