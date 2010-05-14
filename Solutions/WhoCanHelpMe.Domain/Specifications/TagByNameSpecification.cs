namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

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
            get { return t => t.Name.Equals(this.name, StringComparison.CurrentCultureIgnoreCase); }
        }
    }
}
