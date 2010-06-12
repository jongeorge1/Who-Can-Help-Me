namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using NHibernate.Validator.Constraints;

    #endregion

    public class AddAssertionFormModel
    {
        public int CategoryId { get; set; }

        [NotNull(Message = "*")]
        [NotEmpty(Message = "*")]
        public string TagName { get; set; }
    }
}