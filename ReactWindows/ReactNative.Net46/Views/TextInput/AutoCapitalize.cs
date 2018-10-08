using ReactNative.Common;
using ReactNative.Tracing;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ReactNative.Views.TextInput
{
    /// <summary>
    /// Mode that tell TextInput to automatically capitalize certain characters
    /// </summary>
    public enum AutoCapitalizeMode
    {
        /// <summary>
        /// don't auto capitalize anything
        /// </summary>
        None,
        /// <summary>
        /// first letter of each sentence (default)
        /// </summary>
        Sentences,
        /// <summary>
        /// first letter of each word
        /// </summary>
        Words,
        /// <summary>
        /// all characters
        /// </summary>
        Characters
    }

    /// <summary>
    /// Incapsulaes helpers to perform autoCapitalize
    /// </summary>
    public static class AutoCapitalize
    {
        /// <summary>
        /// Converts string representaion of mode to <see cref="AutoCapitalizeMode"/>
        /// </summary>
        /// <param name="mode">string representation of mode</param>
        /// <returns></returns>
        public static AutoCapitalizeMode FromString(string mode)
        {
            if (string.IsNullOrEmpty(mode))
            {
                return AutoCapitalizeMode.Sentences;
            }

            switch (mode)
            {
                case "words":
                    return AutoCapitalizeMode.Words;
                case "characters":
                    return AutoCapitalizeMode.Characters;
                case "none":
                    return AutoCapitalizeMode.None;
                default:
                case "sentences":
                    return AutoCapitalizeMode.Sentences;
            }
        }

        /// <summary>
        /// Converts received text accordingly to <see cref="AutoCapitalizeMode"/> 
        /// </summary>
        /// <param name="text">text that should be converted</param>
        /// <param name="culture"> culture info that should be used for conversion</param>
        /// <param name="mode">conversion mode</param>
        /// <returns>converted text</returns>
        public static string Convert(string text, CultureInfo culture, AutoCapitalizeMode mode = AutoCapitalizeMode.Sentences)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            if (culture == null)
            {
                culture = CultureInfo.CurrentUICulture;
            }

            switch (mode)
            {
                case AutoCapitalizeMode.Characters:
                    return CharacterConvertStrategy(text, culture);
                case AutoCapitalizeMode.None:
                    return text;
                case AutoCapitalizeMode.Words:
                    return WordConvertStrategy(text, culture);
                case AutoCapitalizeMode.Sentences:
                default:
                    return SentencesConvertStrategy(text, culture);
            }
        }

        /// <summary>
        /// Build converter for autoCapitalize
        /// </summary>
        /// <param name="mode"><see cref="AutoCapitalizeMode"/></param>
        /// <returns>Instance that binds converter to the control</returns>
        internal static BindingBase GetBinder(AutoCapitalizeMode mode)
        {
            // Trick! To bind converter to control w/o Path we use MultiBinding where:
            //  1st binding is done to RelativeSource.Self
            //  2nd binding is done to fake source
            // MultiBinding contains reference to desired converter
            var multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(new Binding
            {
                RelativeSource = RelativeSource.Self,
                Mode = BindingMode.OneTime
            });
            multiBinding.Bindings.Add(new Binding("MyValue"));

            multiBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            multiBinding.Converter = new AutoCapitalizeMultiConverter();
            multiBinding.ConverterParameter = mode;

            return multiBinding;
        }

        private static string CharacterConvertStrategy(string text, CultureInfo culture)
        {
            return culture.TextInfo.ToUpper(text);
        }

        private static string SentencesConvertStrategy(string text, CultureInfo culture)
        {
            if (culture.TextInfo.IsRightToLeft)
            {
                Tracer.Error(ReactConstants.Tag, "Right-To-Left cultures are not supported", new NotImplementedException("Right-To-Left cultures are not supported"));
                return text;
            }

            var IsNewSentense = true;
            var result = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                if (IsNewSentense && char.IsLetter(text[i]))
                {
                    result.Append(culture.TextInfo.ToUpper(text[i]));
                    IsNewSentense = false;
                }
                else
                {
                    result.Append(text[i]);
                }

                if (text[i] == '!' || text[i] == '?' || text[i] == '.')
                {
                    IsNewSentense = true;
                }
            }

            return result.ToString();
        }

        private static string WordConvertStrategy(string text, CultureInfo culture)
        {
            return culture.TextInfo.ToTitleCase(text);
        }
    }
}
