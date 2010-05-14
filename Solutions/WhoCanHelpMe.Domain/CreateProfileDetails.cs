namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    using NHibernate.Validator.Constraints;

    #endregion

    [DebuggerDisplay("{FirstName} {LastName} - {UserName}")]
    public class CreateProfileDetails : ValidatableValueObject
    {
        [NotNull, NotEmpty]
        public string FirstName { get; set; }

        [NotNull, NotEmpty]
        public string LastName { get; set; }

        [NotNull, NotEmpty]
        public string UserName { get; set; }
    }
}
