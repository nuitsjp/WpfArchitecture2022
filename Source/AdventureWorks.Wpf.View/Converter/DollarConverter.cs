using System.Globalization;
using System.Windows.Data;
using AdventureWorks.Business;

namespace AdventureWorks.Wpf.View.Converter;

/// <summary>
/// Dollar用IValueConverter
/// </summary>
public class DollarConverter : IValueConverter
{
    /// <summary>
    /// Dollarを文字列に変換する。
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is Dollar dollar)
        {
            return dollar.AsPrimitive().ToString("###,###,###,###.00");
        }

        return string.Empty;
    }

    /// <summary>
    /// 未実装
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}