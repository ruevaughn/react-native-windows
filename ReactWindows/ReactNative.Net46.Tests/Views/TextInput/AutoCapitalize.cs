using NUnit.Framework;
using ReactNative.Views.TextInput;
using System.Globalization;

namespace ReactNative.Tests.Views.TextInput
{
    [TestFixture]
    public class AutoCapitalizeTests
    {
        [Test]
        public void FromStringTest()
        {
            Assert.That(AutoCapitalize.FromString("default"), Is.EqualTo(AutoCapitalizeMode.Sentences));
            Assert.That(AutoCapitalize.FromString(null), Is.EqualTo(AutoCapitalizeMode.Sentences));
            Assert.That(AutoCapitalize.FromString(string.Empty), Is.EqualTo(AutoCapitalizeMode.Sentences));
            Assert.That(AutoCapitalize.FromString(""), Is.EqualTo(AutoCapitalizeMode.Sentences));
            Assert.That(AutoCapitalize.FromString("sentences"), Is.EqualTo(AutoCapitalizeMode.Sentences));
            Assert.That(AutoCapitalize.FromString("words"), Is.EqualTo(AutoCapitalizeMode.Words));
            Assert.That(AutoCapitalize.FromString("characters"), Is.EqualTo(AutoCapitalizeMode.Characters));
            Assert.That(AutoCapitalize.FromString("none"), Is.EqualTo(AutoCapitalizeMode.None));
        }

        [Test]
        public void ConvertToCharactersTest()
        {
            CultureInfo culture = new CultureInfo("en-US", false);
            Assert.That(AutoCapitalize.Convert(null, culture, AutoCapitalizeMode.Characters), Is.Null);
            Assert.That(AutoCapitalize.Convert(string.Empty, culture, AutoCapitalizeMode.Characters), Is.Empty);
            Assert.That(AutoCapitalize.Convert("", culture, AutoCapitalizeMode.Characters), Is.EqualTo(""));
            Assert.That(AutoCapitalize.Convert("hello", culture, AutoCapitalizeMode.Characters), Is.EqualTo("HELLO"));
            Assert.That(AutoCapitalize.Convert("hel lo", culture, AutoCapitalizeMode.Characters), Is.EqualTo("HEL LO"));
            Assert.That(AutoCapitalize.Convert("hEl Lo", culture, AutoCapitalizeMode.Characters), Is.EqualTo("HEL LO"));
            Assert.That(AutoCapitalize.Convert("1h #el - lo", culture, AutoCapitalizeMode.Characters), Is.EqualTo("1H #EL - LO"));
        }

        [Test]
        public void ConvertToNoneTest()
        {
            CultureInfo culture = new CultureInfo("en-US", false);
            Assert.That(AutoCapitalize.Convert(null, culture, AutoCapitalizeMode.None), Is.Null);
            Assert.That(AutoCapitalize.Convert(string.Empty, culture, AutoCapitalizeMode.None), Is.Empty);
            Assert.That(AutoCapitalize.Convert("helLo", culture, AutoCapitalizeMode.None), Is.EqualTo("helLo"));
            Assert.That(AutoCapitalize.Convert("1h #el - lo", culture, AutoCapitalizeMode.None), Is.EqualTo("1h #el - lo"));
        }

        [Test]
        public void ConvertToWordsTest()
        {
            CultureInfo culture = new CultureInfo("en-US", false);
            Assert.That(AutoCapitalize.Convert(null, culture, AutoCapitalizeMode.Words), Is.Null);
            Assert.That(AutoCapitalize.Convert(string.Empty, culture, AutoCapitalizeMode.Words), Is.Empty);
            Assert.That(AutoCapitalize.Convert("", culture, AutoCapitalizeMode.Words), Is.EqualTo(""));
            Assert.That(AutoCapitalize.Convert("hello", culture, AutoCapitalizeMode.Words), Is.EqualTo("Hello"));
            Assert.That(AutoCapitalize.Convert("HelLo tHere to ALL", culture, AutoCapitalizeMode.Words), Is.EqualTo("Hello There To ALL"));
            Assert.That(AutoCapitalize.Convert("1h #el - lo", culture, AutoCapitalizeMode.Words), Is.EqualTo("1H #El - Lo"));
        }

        [Test]
        public void ConvertToSentensesTest()
        {
            CultureInfo culture = new CultureInfo("en-US", false);
            Assert.That(AutoCapitalize.Convert(null, culture, AutoCapitalizeMode.Sentences), Is.Null);
            Assert.That(AutoCapitalize.Convert(string.Empty, culture, AutoCapitalizeMode.Sentences), Is.Empty);
            Assert.That(AutoCapitalize.Convert("", culture, AutoCapitalizeMode.Sentences), Is.EqualTo(""));
            Assert.That(AutoCapitalize.Convert("hello", culture, AutoCapitalizeMode.Sentences), Is.EqualTo("Hello"));
            Assert.That(AutoCapitalize.Convert("HelLo tHere to ALL", culture, AutoCapitalizeMode.Sentences), Is.EqualTo("HelLo tHere to ALL"));
            Assert.That(AutoCapitalize.Convert("1h #el - lo", culture, AutoCapitalizeMode.Sentences), Is.EqualTo("1H #el - lo"));
            Assert.That(AutoCapitalize.Convert("hello, how are you? i'm fine, you? i'm good. nice weather!", culture, AutoCapitalizeMode.Sentences), Is.EqualTo("Hello, how are you? I'm fine, you? I'm good. Nice weather!"));
        }

    }
}
