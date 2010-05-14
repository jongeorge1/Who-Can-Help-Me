namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Contracts.Specifications;

    #endregion

    public abstract class QuerySpecification<T> : QuerySpecification<T, T>
    {
        public override Converter<T, T> ResultMap
        {
            get { return t => t; }
        }
    }

    public abstract class QuerySpecification<T, TResult> : ILinqSpecification<T, TResult>
    {
        public abstract Converter<T, TResult> ResultMap { get; }

        public virtual Expression<Func<T, bool>> MatchingCriteria
        {
            get { return null; }
        }

        public virtual IQueryable<TResult> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            if (this.MatchingCriteria != null)
            {
                return candidates.Where(this.MatchingCriteria).ToList().ConvertAll(this.ResultMap).AsQueryable();
            }

            return candidates.ToList().ConvertAll(this.ResultMap).AsQueryable();
        }
    }
}