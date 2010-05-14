using System;
using System.Collections.Generic;

namespace NHibernate.Linq.Tests
{
	public static class MyEnumerableExtensions
	{
		public static void Each<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var t in items)
			{
				action(t);
			}
		}
	}
}