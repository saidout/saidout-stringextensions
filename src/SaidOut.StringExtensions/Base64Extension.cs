using System;

namespace SaidOut.StringExtensions
{

    /// <summary>Extension to convert a Base64 encoded string into a byte array or a byte array into a Base64 encoded string.</summary>
    public static class Base64Extension
    {

        /// <summary>Create a Base64 encoded string from <paramref name="value"/>.</summary>
        /// <param name="value">The bytes the Base64 string should be created from. If <b>null</b> an empty string is returned.</param>
        /// <returns>A Base64 encoded string.</returns>
        public static string ToBase64String(this byte[] value)
        {
            if (value == null)
                return string.Empty;

            return Convert.ToBase64String(value);
        }


        /// <summary>Create a byte array from a Base64 encoded string.</summary>
        /// <param name="value">A Base64 encoded string.</param>
        /// <param name="shouldReturnNullIfConversionFailed">If null should be returned if <paramref name="value"/> does not contain a Base64 encoded string instead of throwing an exception.</param>
        /// <returns>Byte array representation of the Base64 encoded string in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="value"/> does not contain a Base64 string and <paramref name="shouldReturnNullIfConversionFailed"/> is set to <b>false</b>.</exception>
        public static byte[] FromBase64StringToByteArray(this string value, bool shouldReturnNullIfConversionFailed = true)
        {
            if (value == null)
                return new byte[0];

            try
            {
                return Convert.FromBase64String(value);
            }
            catch (FormatException ex)
            {
                if (shouldReturnNullIfConversionFailed)
                    return null;

                throw new ArgumentException(ex.Message, nameof(value), ex);
            }
        }


        /// <summary>Create a Base64 URL encoded string from <paramref name="value"/>.</summary>
        /// <param name="value">The bytes the Base64 URL string should be created from. If <b>null</b> an empty string is returned.</param>
        /// <param name="removePadding">If padding should be removed, if it is included it will be percent encoded.</param>
        /// <remarks>
        /// A 'base64url' with URL and Filename Safe Alphabet as specified in "RFC 4648 §5 'Table 2: The "URL and Filename safe" Base 64 Alphabet'".
        /// If it's padded, the padding will be percent encoded, i.e. %3D.
        /// </remarks>
        /// <returns>A Base64 URL encoded string.</returns>
        public static string ToBase64UrlString(this byte[] value, bool removePadding = true)
        {
            if (value == null)
                return string.Empty;

            var base64Uri = Convert.ToBase64String(value).Replace("+", "-").Replace("/", "_");
            return removePadding
                ? base64Uri.TrimEnd('=')
                : base64Uri.Replace("=", "%3D");
        }


        /// <summary>Create a byte array from a Base64 URL encoded string.</summary>
        /// <param name="value">A Base64 URL encoded string.</param>
        /// <param name="shouldReturnNullIfConversionFailed">If null should be returned if <paramref name="value"/> does not contain a Base64 URL encoded string instead of throwing an exception.</param>
        /// <remarks>Expect a 'base64url' with URL and Filename Safe Alphabet as specified in "RFC 4648 §5 'Table 2: The "URL and Filename safe" Base 64 Alphabet'".</remarks>
        /// <returns>Byte array representation of the Base64 URL encoded string in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="value"/> does not contain a Base64 URL string and <paramref name="shouldReturnNullIfConversionFailed"/> is set to <b>false</b>.</exception>
        public static byte[] FromBase64UrlStringToByteArray(this string value, bool shouldReturnNullIfConversionFailed = true)
        {
            if (value == null)
                return new byte[0];

            if (value.Contains("+") || value.Contains("/"))
            {
                if (shouldReturnNullIfConversionFailed)
                    return null;

                throw new ArgumentException(ExceptionMessages.Base64UrlIllegalCharacter, nameof(value));
            }

            value = value.Replace("%3D", "=");
            var paddingRequired = value.Length % 4;
            var padding = paddingRequired == 0 ? string.Empty : new string('=', 4 - paddingRequired);
            var base64 = value.Replace("-", "+").Replace("_", "/") + padding;

            try
            {
                return Convert.FromBase64String(base64);
            }
            catch (FormatException ex)
            {
                if (shouldReturnNullIfConversionFailed)
                    return null;

                if (ex.Message.Contains("illegal"))
                    throw new ArgumentException(ExceptionMessages.Base64UrlIllegalCharacter, nameof(value));

                throw new ArgumentException(ex.Message, nameof(value), ex);
            }
        }
    }
}