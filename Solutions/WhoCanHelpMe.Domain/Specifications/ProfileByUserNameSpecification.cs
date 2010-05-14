namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// The profile by user name specification.
    /// </summary>
    public class ProfileByUserNameSpecification : QuerySpecification<Profile>
    {
        /// <summary>
        /// The user name.
        /// </summary>
        private readonly string userName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileByUserNameSpecification"/> class.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        public ProfileByUserNameSpecification(string userName)
        {
            this.userName = userName;
        }

        public string UserName
        {
            get { return this.userName; }
        }

        public override Expression<Func<Profile, bool>> MatchingCriteria
        {
            get { return p => p.UserName == this.userName; }
        }
    }
}