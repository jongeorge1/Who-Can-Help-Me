namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    using SharpArch.Futures.Core.Specifications;

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
            // HACK: Fugly hack here. The NHib Linq provider doesn't convert string.Substring)( correctly - the SQL substring 
            // function it maps to is 1-based, wheras the .NET version is 0 based. So I've got to use 1 as the startIndex for 
            // this to work properly.
            get { return t => t.Name.Substring(1, this.startingCharacters.Length).ToLowerInvariant() == this.startingCharacters.ToLowerInvariant(); }
        }
    }
}
