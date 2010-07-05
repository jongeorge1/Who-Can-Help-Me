namespace WhoCanHelpMe.Framework.Extensions
{
	using System;
	using System.Collections.Generic;

	public static class EnumerableExtensions
	{
		public static void Each<T>(this IEnumerable<T> items, Action<T> action)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}

			if (action == null)
			{
				throw new ArgumentNullException("action");
			}

			foreach (T local in items)
			{
				action(local);
			}
		}
	}
}