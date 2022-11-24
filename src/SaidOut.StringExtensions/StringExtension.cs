using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SaidOut.StringExtensions
{

    /// <summary>Extension for truncating a string, make sure a string ends with a symbol and replace keywords in a string with their corresponding values.</summary>
    public static class StringExtension
    {

        /// <summary>Three ASCII periods (...)</summary>
        public const string EllipsisAsciiSymbol = "...";
        /// <summary>The UTF-8 character representing ellipsis (\u2026)</summary>
        public const string EllipsisUtf8Symbol = "\u2026";


        /// <summary>Will truncate text so that it will fit inside <paramref name="maxLength"/> including the <paramref name="truncateSymbol"/>.</summary>
        /// <param name="value">The value that should be truncate if it would exceed the <paramref name="maxLength"/>.</param>
        /// <param name="maxLength">If <paramref name="value"/> length exceeds the value then the string will be truncated.</param>
        /// <param name="truncateSymbol">The symbol to place at the end if the value has been truncated.</param>
        /// <returns>
        /// A truncated version of the <paramref name="value"/> if its length exceeds the <paramref name="maxLength"/>, otherwise the <paramref name="value"/> is returned unchanged.
        /// If value is <b>null</b> then an empty string is returned.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If <paramref name="maxLength"/> is less than one.</exception>
        /// <exception cref="System.ArgumentException">If <paramref name="maxLength"/> is less than the length of <paramref name="truncateSymbol"/> .</exception>
        public static string Truncate(this string? value, int maxLength, string? truncateSymbol = EllipsisAsciiSymbol)
        {
            if (maxLength < 1)
                throw new ArgumentOutOfRangeException(nameof(maxLength), string.Format(ExceptionMessages.ParamCannotBeLessThan_ParamName_MinValue_ActualValue, nameof(maxLength), 1, maxLength));

            truncateSymbol ??= string.Empty;
            if (truncateSymbol.Length > maxLength)
                throw new ArgumentException(string.Format(ExceptionMessages.ParamAStringLengthCannotBeGreaterThanValueOfParamB_ParamAStrLen_ParamBValue, nameof(truncateSymbol), nameof(maxLength)), nameof(truncateSymbol));

            if (value is null)
                return string.Empty;

            if (value.Length > maxLength)
                return value[..(maxLength - truncateSymbol.Length)] + truncateSymbol;

            return value;
        }


        /// <summary>Make sure that the <paramref name="input"/> ends with symbol by appending it to the <paramref name="input"/> if it does not end with symbol.</summary>
        /// <param name="input">The input which <paramref name="symbol"/> should be appended to if it's missing.</param>
        /// <param name="symbol">The symbol that the <paramref name="input"/> should end with.</param>
        /// <example>
        /// Symbol: /
        /// Input: testA/testB  => testA/testB/
        /// Input: testA/testB/ => testA/testB/
        /// </example>
        /// <returns>A string guaranteed to end with symbol.</returns>
        public static string AppendSymbolIfMissing(this string? input, string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(string.Format(ExceptionMessages.StringParamCannotBeNullOrEmpty_ParamName, nameof(symbol)), nameof(symbol));

            input ??= string.Empty;
            return input.EndsWith(symbol)
                ? input
                : input + symbol;
        }


        /// <summary>Use the <paramref name="keyValues"/> object to replace text matching the properties in keyValues with their corresponding property value.</summary>
        /// <param name="input">The string where keys should be replaced with the corresponding value.</param>
        /// <param name="keyValues">An object where the properties will be used as keys and the property’s value will be the text that will replace keys that are found in <paramref name="input"/>.</param>
        /// <param name="keyPrefix">A prefix that should be added to the key before searching for the key in <paramref name="input"/>, can be null or empty if no prefix should be used.</param>
        /// <param name="keySuffix">A suffix that should be appended to the key before searching for the key in <paramref name="input"/>, can be null or empty if no suffix should be used.</param>
        /// <returns>A string where keys has been replaced with the corresponding value.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="keyValues"/> is null.</exception>
        public static string ReplaceKeyWithValue(this string? input, object? keyValues, string keyPrefix, string keySuffix)
        {
            ArgumentNullException.ThrowIfNull(keyValues);
            if (string.IsNullOrWhiteSpace(input))
                return input ?? string.Empty;

            var sb = new StringBuilder(input);
            foreach (var (key, value) in ExtractKeyValues(keyValues))
            {
                sb.Replace(keyPrefix + key + keySuffix, value?.ToString());
            }

            return sb.ToString();
        }



        private static Dictionary<string, object?> ExtractKeyValues(object keyValues)
        {
            var props = keyValues.GetType().GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return props.ToDictionary(prop => prop.Name, prop => prop.GetValue(keyValues));
        }
    }
}