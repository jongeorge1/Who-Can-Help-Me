namespace WhoCanHelpMe.Framework.Traversal
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

	using Framework.Contracts.Specifications;

    using Enumerable;

    using Mapper;

    #endregion

    public static class TraversalExtensions
    {
        public static IEnumerable<T> OneAtATime<T>(this IEnumerable<T> items)
        {
            return items.Select(x => x);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            items.Each(action);
        }

        public static void AddRange<T>(this IList<T> observableCollection, IEnumerable<T> collection)
        {
            collection.ForEach(observableCollection.Add);
        }

        public static void Replace<T>(this IList<T> observableCollection, IEnumerable<T> collection)
        {
            observableCollection.Clear();
            collection.ForEach(observableCollection.Add);
        }

        public static IEnumerable<T> AllSatisfying<T>(this IEnumerable<T> items, ISpecification<T> specification)
        {
            return items.Where(specification.IsSatisfiedBy);
        }
    }
}