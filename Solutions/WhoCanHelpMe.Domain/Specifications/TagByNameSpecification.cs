namespace WhoCanHelpMe.Domain.Specifications
{
    using System.Linq;
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    using SharpArch.Futures.Core.Specifications;

    #endregion

    public class TagByNameSpecification : QuerySpecification<Tag>
    {
        private readonly string name;

        public TagByNameSpecification(string name)
        {
            this.name = name;
        }
        
        public string Name
        {
            get { return this.name; }
        }

        public override Expression<Func<Tag, bool>> MatchingCriteria
        {
            get { return t => t.Name.ToLower() == this.name.ToLower(); }
        }
    }
}
