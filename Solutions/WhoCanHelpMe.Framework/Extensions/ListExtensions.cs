namespace WhoCanHelpMe.Framework.Extensions
{
	using System;
	using System.Collections.Generic;

	public static class ListExtensions
	{
		public static void AddRange<T>(this IList<T> observableCollection, IEnumerable<T> collection)
		{
			if (observableCollection == null)
			{
				throw new ArgumentNullException("observableCollection");
			}

			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}

			collection.Each(observableCollection.Add);
		}
	}
}