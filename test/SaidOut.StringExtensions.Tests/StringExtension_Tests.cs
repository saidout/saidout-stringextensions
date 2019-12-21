using System;
using NUnit.Framework;

namespace SaidOut.StringExtensions.Tests
{

    public class StringExtension_Tests
    {

        #region Truncate
        [TestCase("1", 3)]
        [TestCase("12", 3)]
        [TestCase("123", 3)]
        public void Truncate_ValueLengthIsLessThanMaxLength_ReturnValue(string value, int maxLength)
        {
            var actual = value.Truncate(maxLength);

            Assert.That(actual, Is.EqualTo(value));
        }


        [Test]
        public void Truncate_ValueIsNull_ReturnEmptyString()
        {
            var actual = ((string)null).Truncate(4);

            Assert.That(actual, Is.EqualTo(string.Empty));
        }


        [TestCase("12345", 3, "...", "...")]
        [TestCase("12345", 3, "\u2026", "12\u2026")]
        [TestCase("1234", 3, ".", "12.")]
        [TestCase("1234", 4, "...", "1234")]
        [TestCase("1234567890", 6, "--", "1234--")]
        [TestCase("1234", 3, "", "123")]
        [TestCase("1234", 2, null, "12")]
        public void Truncate_ValueLengthIsGreaterThenMaxLength_ReturnTruncatedStringWithSymbolAtEnd(string value, int maxLength, string sybmol, string expectedValue)
        {
            var actual = value.Truncate(maxLength, sybmol);

            Assert.That(actual, Is.EqualTo(expectedValue));
        }


        [TestCase("1234", 3, "....")]
        [TestCase("12", 4, ".....")]
        public void Truncate_TruncateSymbolLengthIsGreaterThanMaxLength_ThrowsArgumentException(string value, int maxLength, string sybmol)
        {
            var ex = Assert.Throws<ArgumentException>(() => value.Truncate(maxLength, sybmol));
            Assert.That(ex.ParamName, Is.EqualTo("truncateSymbol"));
            Assert.That(ex.Message, Does.Contain("maxLength").And.Contain("truncateSymbol"));
        }


        [TestCase("12", 0, "")]
        [TestCase("12", 0, ".")]
        [TestCase("12", -1, ".")]
        public void Truncate_MaxLengthIsLessOne_ThrowsArgumentOutOfRangeException(string value, int maxLength, string sybmol)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => value.Truncate(maxLength, sybmol));
            Assert.That(ex.ParamName, Is.EqualTo("maxLength"));
            Assert.That(ex.Message, Does.Contain("maxLength").And.Contain("1"));
        }
        #endregion


        #region Constants
        [Test]
        public void EllipsisAsciiSymbol_Constant_ReturnThreePeriods()
        {
            var actual = StringExtension.EllipsisAsciiSymbol;
            
            Assert.That(actual, Is.EqualTo("..."));
        }


        [Test]
        public void EllipsisUtf8Symbol_Constant_ReturnStringOnlyContaingEllipsisUnicodeChar()
        {
            var actual = StringExtension.EllipsisUtf8Symbol;

            Assert.That(actual, Is.EqualTo("\u2026"));
        }
        #endregion


        #region AppendSymbolIfMissing
        [TestCase(null, "SYM", "SYM")]
        [TestCase("", "SYMB", "SYMB")]
        [TestCase("adsSYM", "SYM", "adsSYM")]
        [TestCase("SYMads", "SYM", "SYMadsSYM")]
        [TestCase("pathA/pathB", "/", "pathA/pathB/")]
        [TestCase("pathA/pathB/", "/", "pathA/pathB/")]
        public void AppendSymbolIfMissing_SymbolIsSet_ReturnExpectedValue(string input, string symbol, string expectedValue)
        {
            var actual = input.AppendSymbolIfMissing(symbol);

            Assert.That(actual, Is.EqualTo(expectedValue));
        }


        [TestCase(null)]
        [TestCase("")]
        public void AppendSymbolIfMissing_SymbolIsNullOrEmpty_ThrowsArgumentException(string symbol)
        {
            var ex = Assert.Throws<ArgumentException>(() => "test".AppendSymbolIfMissing(symbol));
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }


        [TestCase(null, '~', "~")]
        [TestCase("", '#', "#")]
        [TestCase("adsSYM$", '$', "adsSYM$")]
        [TestCase("SYMads", '$', "SYMads$")]
        [TestCase("pathA/pathB", '/', "pathA/pathB/")]
        [TestCase("pathA/pathB/", '/', "pathA/pathB/")]
        public void AppendSymbolIfMissing_Symbol_ReturnExpectedValue(string input, char symbol, string expectedValue)
        {
            var actual = input.AppendSymbolIfMissing(symbol);

            Assert.That(actual, Is.EqualTo(expectedValue));
        }
        #endregion


        #region ReplaceKeyWithValue
        [TestCase(null, "$", "$", "")]
        [TestCase("", "$", "$", "")]
        [TestCase("The value keyA.", "", "", "The value valueA.")]
        [TestCase("The value keyA.", null, null, "The value valueA.")]
        [TestCase("The value keyA.", "", null, "The value valueA.")]
        [TestCase("The value keyA.", null, "", "The value valueA.")]
        [TestCase("The value $keyA next value $keyB.", "$", "", "The value valueA next value valueB.")]
        [TestCase("The value keyA$ next value keyB$.", "", "$", "The value valueA next value valueB.")]
        [TestCase("The value #keyA# next value #keyB#.", "#", "#", "The value valueA next value valueB.")]
        [TestCase("The keyA$ value $keyB.", "$", "", "The keyA$ value valueB.")]
        [TestCase("The value {{keyA}} next value {{keyB}}.", "{{", "}}", "The value valueA next value valueB.")]
        [TestCase("The value {{keyA}} next value {{keyB}}.", "{{", "", "The value valueA}} next value valueB}}.")]
        [TestCase("The value {{keyA}} next value {{keyB}}.", "", "}}", "The value {{valueA next value {{valueB.")]
        public void ReplaceKeyWithValue_KeyValuesIsSet_ReturnExpectedValueWhereKeyHasBeenReplacedWithValue(string input,
            string keyPrefix,
            string keySuffix,
            string expectedValue)
        {
            var actual = input.ReplaceKeyWithValue(new {keyA = "valueA", keyB = "valueB"}, keyPrefix, keySuffix);

            Assert.That(actual, Is.EqualTo(expectedValue));
        }


        [TestCase("Key is missing {missingKey}.", "{", "}", "Key is missing {missingKey}.")]
        [TestCase("Key is missing $missingKey$.", "$", "", "Key is missing $missingKey$.")]
        [TestCase("Key is missing $missingKey$.", "", "$", "Key is missing $missingKey$.")]
        public void ReplaceKeyWithValue_KeyIsMissing_ReturnExpectedValueWhereKeyHasNotBeenReplaced(string input,
            string keyPrefix,
            string keySuffix,
            string expectedValue)
        {
            var actual = input.ReplaceKeyWithValue(new { keyA = "valueA" }, keyPrefix, keySuffix);

            Assert.That(actual, Is.EqualTo(expectedValue));
        }


        [TestCase("Value {keyA} is null.", "{", "}", "Value  is null.")]
        [TestCase("Value $keyA$ is null.", "$", "", "Value $ is null.")]
        [TestCase("Value $keyA$ is null.", "", "$", "Value $ is null.")]
        public void ReplaceKeyWithValue_ValueIsNull_ReturnExpectedValueWhereKeyHasBeenReplaced(string input,
            string keyPrefix,
            string keySuffix,
            string expectedValue)
        {
            var actual = input.ReplaceKeyWithValue(new { keyA = (string)null }, keyPrefix, keySuffix);

            Assert.That(actual, Is.EqualTo(expectedValue));
        }


        [Test]
        public void ReplaceKeyWithValue_KeyValuesIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => "test".ReplaceKeyWithValue(null, "{{", "}}"));
            Assert.That(ex.ParamName, Is.EqualTo("keyValues"));
        }
        #endregion
    }
}