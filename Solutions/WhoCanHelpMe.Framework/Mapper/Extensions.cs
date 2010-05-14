namespace WhoCanHelpMe.Framework.Mapper
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public static class Extensions
    {
        public static IEnumerable<TDestination> MapAllUsing<TSource, TDestination>(
            this IEnumerable<TSource> items,
            IMapper<TSource, TDestination> mapper)
        {
            return items.Select(x => mapper.MapFrom(x));
        }

        public static IList<TDestination> MapAllUsing<TSource, TDestination>(
            this IList<TSource> items,
            IMapper<TSource, TDestination> mapper)
        {
            return items.Select(x => mapper.MapFrom(x)).ToList();
        }

        public static TDestination MapUsing<TSource, TDestination>(
            this TSource source,
            IMapper<TSource, TDestination> mapper)
        {
            return mapper.MapFrom(source);
        }
    }
}