using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace SaidOut.StringExtensions.Tests
{

    public class EnumerableExtension_Tests
    {

        [Test]
        public void ToDelimitatedString_CollectionIsNull_ReturnEmptyString()
        {
            var actual = ((IEnumerable<int>)null).ToDelimitatedString(",");

            Assert.That(actual, Is.EqualTo(string.Empty));
        }


        [TestCase("1", new[] { 1 })]
        [TestCase("1, 2", new[] { 1, 2 })]
        [TestCase("1, 2, 3", new[] { 1, 2, 3 })]
        public void ToDelimitatedStringWithNoParams_CollectionHasElements_ReturnExpectedString(string expected, int[] values)
        {
            var actual = values.ToDelimitatedString();

            Assert.That(actual, Is.EqualTo(expected));
        }


        [TestCase("1", ", ", null, new[] { 1 })]
        [TestCase("1, 2", ", ", null, new[] { 1, 2 })]
        [TestCase("1, 2, 3, 4, 5", ", ", null, new[] { 1, 2, 3, 4, 5 })]
        [TestCase("1", "; ", null, new[] { 1 })]
        [TestCase("1; 2", "; ", null, new[] { 1, 2 })]
        [TestCase("1; 2; 3; 4; 5", "; ", null, new[] { 1, 2, 3, 4, 5 })]
        [TestCase("1", ", ", " and ", new[] { 1 })]
        [TestCase("1 and 2", ", ", " and ", new[] { 1, 2 })]
        [TestCase("1, 2, 3, 4 and 5", ", ", " and ", new[] { 1, 2, 3, 4, 5 })]
        [TestCase("1", ", ", " and ", new[] { 1 })]
        [TestCase("1 and 2", ", ", " and ", new[] { 1, 2 })]
        [TestCase("1, 2, 3, 4 and 5", ", ", " and ", new[] { 1, 2, 3, 4, 5 })]
        public void ToDelimitatedStringWithTwoParams_CollectionHasElements_ReturnExpectedString(string expected, string delimiter, string endDelimiter, int[] values)
        {
            var actual = values.ToDelimitatedString(delimiter, endDelimiter);

            Assert.That(actual, Is.EqualTo(expected));
        }


        [Test]
        public void ToDelimitatedWithStringWithFunc_CollectionIsNull_ReturnEmptyString()
        {
            var actual = ((IEnumerable<int>)null).ToDelimitatedString(it => "t" + it, ",");

            Assert.That(actual, Is.EqualTo(string.Empty));
        }


        private const string IntToSingleQuoateFunc = "IntToSingleQuoateFunc";
        private const string IntToExcHashFunc = "IntToExcHashFunc";
        private static readonly Dictionary<string, Func<int, string>> ConstToFuncMapping = new Dictionary <string, Func<int, string>>()
        {
            { IntToSingleQuoateFunc, val => $"'{val}'"},
            { IntToExcHashFunc, val => $"!{val}#" }
        };


        [TestCase("'1'", IntToSingleQuoateFunc, new[] { 1 })]
        [TestCase("!1#, !2#", IntToExcHashFunc, new[] { 1, 2 })]
        [TestCase("'1', '2', '3'", IntToSingleQuoateFunc, new[] { 1, 2, 3 })]
        public void ToDelimitatedStringWithFuncNoParams_CollectionHasElements_ReturnExpectedString(string expected, string funcKey, int[] values)
        {
            var func = ConstToFuncMapping[funcKey];

            var actual = values.ToDelimitatedString(func);

            Assert.That(actual, Is.EqualTo(expected));
        }


        [TestCase("'1'", IntToSingleQuoateFunc, ", ", null, new[] { 1 })]
        [TestCase("!1#, !2#", IntToExcHashFunc, ", ", null, new[] { 1, 2 })]
        [TestCase("'1', '2', '3', '4', '5'", IntToSingleQuoateFunc, ", ", null, new[] { 1, 2, 3, 4, 5 })]
        [TestCase("!1#", IntToExcHashFunc, "; ", null, new[] { 1 })]
        [TestCase("'1'; '2'", IntToSingleQuoateFunc, "; ", null, new[] { 1, 2 })]
        [TestCase("!1#; !2#; !3#; !4#; !5#", IntToExcHashFunc, "; ", null, new[] { 1, 2, 3, 4, 5 })]
        [TestCase("'1'", IntToSingleQuoateFunc, ", ", " and ", new[] { 1 })]
        [TestCase("!1# and !2#", IntToExcHashFunc, ", ", " and ", new[] { 1, 2 })]
        [TestCase("'1', '2', '3', '4' and '5'", IntToSingleQuoateFunc, ", ", " and ", new[] { 1, 2, 3, 4, 5 })]
        [TestCase("!1#", IntToExcHashFunc, ", ", " and ", new[] { 1 })]
        [TestCase("'1' and '2'", IntToSingleQuoateFunc, ", ", " and ", new[] { 1, 2 })]
        [TestCase("!1#, !2#, !3#, !4# and !5#", IntToExcHashFunc, ", ", " and ", new[] { 1, 2, 3, 4, 5 })]
        public void ToDelimitatedStringWithFuncTwoParams_CollectionHasElements_ReturnExpectedString(string expected,
            string funcKey,
            string delimiter,
            string endDelimiter,
            int[] values)
        {
            var func = ConstToFuncMapping[funcKey];

            var actual = values.ToDelimitatedString(func, delimiter, endDelimiter);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}