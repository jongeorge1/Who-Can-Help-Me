namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;

    #endregion

    public class ProfileByIdSpecification : QuerySpecification<Profile>
    {
        private readonly int id;

        public ProfileByIdSpecification(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get { return this.id; }
        }

        public override System.Linq.Expressions.Expression<Func<Profile, bool>> MatchingCriteria
        {
            get { return p => p.Id == this.id; }
        }
    }
}
