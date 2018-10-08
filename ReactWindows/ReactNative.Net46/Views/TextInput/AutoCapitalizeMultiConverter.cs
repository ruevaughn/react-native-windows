using System;
using System.Globalization;
using System.Windows.Data;

namespace ReactNative.Views.TextInput
{
    /// <summary>
    /// Converters text accordingly to specified mode
    /// </summary>
    public class AutoCapitalizeMultiConverter : IMultiValueConverter
    {
        string source;

        /// <inheritedoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return source;
        }

        /// <inheritedoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            source = AutoCapitalize.Convert(value as string, culture, (AutoCapitalizeMode)parameter);
            return new object[] { null, source };
        }
    }
}
