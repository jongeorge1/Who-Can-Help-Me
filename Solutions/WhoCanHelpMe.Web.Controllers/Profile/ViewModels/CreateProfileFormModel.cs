namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using NHibernate.Validator.Constraints;

    #endregion

    public class CreateProfileFormModel
    {
        [NotNull(Message = "*")]
        [NotEmpty(Message = "*")]
        public string FirstName { get; set; }

        [NotNull(Message = "*")]
        [NotEmpty(Message = "*")]
        public string LastName { get; set; }
    }
}