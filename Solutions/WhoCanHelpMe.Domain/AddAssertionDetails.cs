namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    using NHibernate.Validator.Constraints;

    #endregion

    [DebuggerDisplay("{UserName} - {CategoryId} - {TagName}")]
    public class AddAssertionDetails : ValidatableValueObject
    {
        public int CategoryId { get; set; }

        [NotNull]
        [NotEmpty]
        public string TagName { get; set; }

        [NotNull]
        [NotEmpty]
        public string UserName { get; set; }
    }
}
