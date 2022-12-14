using System.Globalization;
using System.Windows.Data;

namespace AdventureWorks.View.Converter;

public class DollarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is Dollar dollar)
        {
            return dollar.AsPrimitive().ToString("###,###,###,###.00");
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}