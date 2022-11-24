using System;
using System.Collections.Generic;

namespace SaidOut.StringExtensions
{

    /// <summary>Extension to convert a Hex string into a byte array or a byte array into a Hex string.</summary>
    public static class HexExtension
    {

        private static readonly char[] NibbleToStringMappingUpperCase = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static readonly char[] NibbleToStringMappingLowerCase = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        private static readonly Dictionary<char, byte> CharToNibbleMapping = new Dictionary<char, byte>
        {
            { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'A', 10 }, { 'a', 10}, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 }, { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }
        };


        /// <summary>Create a hex string from a byte array.</summary>
        /// <param name="value">The byte array to create a hex string from.</param>
        /// <param name="upperCase">If the hex string should be in uppercasing, if <b>false</b> the hex string will be in lowercasing.</param>
        /// <returns>Hex string representation of <paramref name="value"/>.</returns>
        public static string ToHexString(this byte[] value, bool upperCase = true)
        {
            if (value == null)
                return string.Empty;

            var mapping = upperCase ? NibbleToStringMappingUpperCase : NibbleToStringMappingLowerCase;
            var hexString = new char[value.Length * 2];

            var charIndex = 0;
            for (var i = 0; i < value.Length; i += 1, charIndex += 2)
            {
                hexString[charIndex] = mapping[value[i] >> 4];
                hexString[charIndex + 1] = mapping[value[i] & 0x0F];
            }

            return new string(hexString);
        }


        /// <summary>Create a byte array from a hex string.</summary>
        /// <param name="value">The hex string the byte array should be create from.</param>
        /// <param name="shouldReturnNullIfConversionFailed">If null should be returned if <paramref name="value"/> does not contain a Hex string instead of throwing an exception.</param>
        /// <returns>A byte array created from the hex string.</returns>
        /// <exception cref="ArgumentException">If <paramref name="value"/> does not contain a hex string and <paramref name="shouldReturnNullIfConversionFailed"/> is set to <b>false</b>.</exception>
        public static byte[] FromHexStringToByteArray(this string value, bool shouldReturnNullIfConversionFailed = true)
        {
            if (value == null)
                return new byte[0];

            value = value.StartsWith("0x")
                ? value.Substring(2)
                : value;

            var output = new byte[value.Length / 2];
            if (value.Length % 2 != 0)
            {
                if (shouldReturnNullIfConversionFailed) return null;
                throw new ArgumentException(ExceptionMessages.HexStringInvalidLength, nameof(value));
            }

            var outputIndex = 0;
            for (var idx = 0; idx < value.Length; idx += 2, outputIndex += 1)
            {
                try
                {
                    var highNibble = CharToNibbleMapping[value[idx]];
                    var lowNibble = CharToNibbleMapping[value[idx + 1]];

                    output[outputIndex] = (byte)((highNibble << 4) | lowNibble);
                }
                catch (KeyNotFoundException)
                {
                    if (shouldReturnNullIfConversionFailed) return null;
                    throw new ArgumentException(ExceptionMessages.HexStringHasIllegalCharacter, nameof(value));
                }
            }

            return output;
        }
    }
}