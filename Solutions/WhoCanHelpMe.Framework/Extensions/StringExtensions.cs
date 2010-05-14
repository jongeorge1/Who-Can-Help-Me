namespace WhoCanHelpMe.Framework.Extensions
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Text;

    #endregion

    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Switches the IsNullOrEmpty to a more readable format
        /// </summary>
        /// <param name="theString">
        /// The the string.
        /// </param>
        /// <returns>
        /// True if the string is null or empty
        /// </returns>
        public static bool IsNullOrEmpty(this string theString)
        {
            return string.IsNullOrEmpty(theString);
        }

        /// <summary>
        /// Formats a string with the specified args
        /// </summary>
        /// <param name="theString">
        /// The the string.
        /// </param>
        /// <param name="args">
        /// The format args.
        /// </param>
        /// <returns>
        /// The formatted string
        /// </returns>
        public static string FormatWith(this string theString, params object[] args)
        {
            return string.Format(theString, args);
        }

        /// <summary>
        /// Retuns the frist N Characters of a string
        /// </summary>
        /// <param name="theString">
        /// The the string.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static string FirstNCharacters(this string theString, int index)
        {
            int length = theString.Trim().Length;

            if (index >= length)
            {
                throw new InvalidOperationException("string must be longer than the index value");
            }

            return theString.Substring(0, index);
        }

        /// <summary>
        /// Returns the last N Characters of a string.
        /// </summary>
        /// <remarks>
        /// "Hello World ".LastNCharacters(5) returns "World"
        /// </remarks>
        /// <param name="theString">
        /// The the string.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static string LastNCharacters(this string theString, int index)
        {
            int length = theString.Trim().Length;

            if (index > length)
            {
                throw new InvalidOperationException("string must be longer than the index value");
            }

            return theString.Substring((length - index), index);
        }

        /// <summary>
        /// Appends an Html Line break and the string
        /// to a string builder if the string is
        /// not null or empty
        /// </summary>
        /// <param name="sb">
        /// The string builder.
        /// </param>
        /// <param name="theString">
        /// The the string.
        /// </param>
        public static void AppendHtmlLineIfNotEmpty(this StringBuilder sb, string theString)
        {
            if (!theString.IsNullOrEmpty())
            {
                sb.AppendFormat("<BR />{0}", theString);
            }
        }

        /// <summary>
        /// Firsts the N words. 
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <param name="numWords">The num words.</param>
        /// <returns>
        /// The first N words from a string, if shorter then the original string is returned.
        /// </returns>
        public static string FirstNWords(this string theString, int numWords)
        {
            StringBuilder sb = new StringBuilder();

            if (theString != null && numWords >= 0)
            {
                string[] words = theString.Split(' ');

                IEnumerator enumerator = words.GetEnumerator();

                int count = 0;
                while (count < numWords)
                {
                    if (count != 0)
                    {
                        sb.Append(" ");
                    }

                    if (enumerator.MoveNext())
                    {
                        sb.Append(enumerator.Current);
                        enumerator.MoveNext();
                        count++;
                    }
                    else
                    {
                        // die quietly
                        break;
                    }
                }
            }

            return sb.ToString();
        }
    }
}