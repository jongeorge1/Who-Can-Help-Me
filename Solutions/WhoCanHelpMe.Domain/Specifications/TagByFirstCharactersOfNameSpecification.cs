namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    #endregion

    public class TagByFirstCharactersOfNameSpecification : QuerySpecification<Tag>
    {
        private readonly string startingCharacters;

        public TagByFirstCharactersOfNameSpecification(string startingCharacters)
        {
            this.startingCharacters = startingCharacters;
        }

        public string StartingCharacters
        {
            get { return this.startingCharacters; }
        }

        public override Expression<Func<Tag, bool>> MatchingCriteria
        {
            get { return t => t.Name.StartsWith(this.startingCharacters, StringComparison.CurrentCultureIgnoreCase); }
        }
    }
}
