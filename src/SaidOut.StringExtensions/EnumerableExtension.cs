using System;
using System.Collections.Generic;
using System.Text;

namespace SaidOut.StringExtensions
{

    /// <summary>Extension creates string representation out of collection.</summary>
    public static class EnumerableExtension
    {

        /// <summary>Create a string representation for a <paramref name="collection"/> where each element is separated with the delimiter specified.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection that string representation should be created for.</param>
        /// <param name="delimiter">The delimiter to use between each element in the <paramref name="collection"/>.</param>
        /// <param name="endDelimiter">
        /// The delimiter to use between the two last elements in the <paramref name="collection"/>.
        /// If it's <b>null</b> then the <paramref name="delimiter"/> will be used as <paramref name="endDelimiter"/>.
        /// </param>
        /// <remarks>Will use <see cref="object.ToString"/> to get the string representation for each element in the <paramref name="collection"/>.</remarks>
        /// <returns>The string representation of the collection using <paramref name="delimiter"/> and <paramref name="endDelimiter"/> as delimiter between values in the collection.</returns>
        public static string ToDelimitatedString<T>(this IEnumerable<T> collection, string delimiter = ", ", string endDelimiter = null)
        {
            if (collection == null)
                return string.Empty;

            var sb = new StringBuilder();
            using (var enumerator = collection.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    sb.Append(enumerator.Current);
                }

                if (endDelimiter == null)
                {
                    while (enumerator.MoveNext())
                    {
                        sb.Append(delimiter + enumerator.Current);
                    }
                }
                else
                {
                    var hasNextValue = enumerator.MoveNext();
                    while (hasNextValue)
                    {
                        var current = enumerator.Current;
                        hasNextValue = enumerator.MoveNext();
                        if (!hasNextValue)
                        {
                            sb.Append(endDelimiter + current);
                            break;
                        }

                        sb.Append(delimiter + current);
                    }
                }
            }

            return sb.ToString();
        }


        /// <summary>Create a string representation for a <paramref name="collection"/> where each element is separated with the delimiter specified.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection that string representation should be created for.</param>
        /// <param name="func">Function to use when converting an element into its string representation.</param>
        /// <param name="delimiter">The delimiter to use between each element in the <paramref name="collection"/>.</param>
        /// <param name="endDelimiter">
        /// The delimiter to use between the two last elements in the <paramref name="collection"/>.
        /// If it's <b>null</b> then the <paramref name="delimiter"/> will be used as <paramref name="endDelimiter"/>.
        /// </param>
        /// <returns>The string representation of the collection using <paramref name="delimiter"/> and <paramref name="endDelimiter"/> as delimiter between values in the collection.</returns>
        public static string ToDelimitatedString<T>(this IEnumerable<T> collection, Func<T, string> func, string delimiter = ", ", string endDelimiter = null)
        {
            if (collection == null)
                return string.Empty;

            var sb = new StringBuilder();
            using (var enumerator = collection.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    sb.Append(func.Invoke(enumerator.Current));
                }

                if (endDelimiter == null)
                {
                    while (enumerator.MoveNext())
                    {
                        sb.Append(delimiter + func.Invoke(enumerator.Current));
                    }
                }
                else
                {
                    var hasNextValue = enumerator.MoveNext();
                    while (hasNextValue)
                    {
                        var current = enumerator.Current;
                        hasNextValue = enumerator.MoveNext();
                        if (!hasNextValue)
                        {
                            sb.Append(endDelimiter + func.Invoke(current));
                            break;
                        }

                        sb.Append(delimiter + func.Invoke(current));
                    }
                }
            }

            return sb.ToString();
        }
    }
}