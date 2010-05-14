namespace WhoCanHelpMe.Framework.Enumerable
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T local in items)
            {
                action(local);
            }
        }

        /// <summary>
        /// Convenient replacement for a range 'for' loop. e.g. return an array of int from 10 to 20:
        /// int[] tenToTwenty = 10.to(20).ToArray();
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<int> To(this int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                yield return i;
            }
        }
    }
}