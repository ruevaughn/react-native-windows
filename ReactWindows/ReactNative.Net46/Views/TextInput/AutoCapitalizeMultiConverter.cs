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
        string source = null;

        /// <inheritedoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (source == null)
            {
                var text = (values[0] as ReactTextBox)?.Text;
                source = AutoCapitalize.Convert(text, culture, (AutoCapitalizeMode)parameter);
            }

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
