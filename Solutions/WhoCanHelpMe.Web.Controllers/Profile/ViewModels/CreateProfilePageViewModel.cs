namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using NHibernate.Validator.Constraints;

    using Shared.ViewModels;

    #endregion

    public class CreateProfilePageViewModel : PageViewModel
    {
        [NotNull]
        [NotEmpty]
        public string FirstName { get; set; }

        [NotNull]
        [NotEmpty]
        public string LastName { get; set; }
    }
}