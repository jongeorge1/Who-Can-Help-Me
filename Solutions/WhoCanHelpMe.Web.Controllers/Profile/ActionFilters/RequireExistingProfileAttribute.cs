namespace WhoCanHelpMe.Web.Controllers.Profile.ActionFilters
{
    #region Using Directives

    using System.Web.Mvc;

    #endregion

    public class RequireExistingProfileAttribute : ProfileCheckBaseAttribute
    {
        public RequireExistingProfileAttribute(string redirectController, string redirectAction)
            : base(redirectController, redirectAction)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var profile = this.GetProfile();

            if (profile == null)
            {
                this.DoRedirect(filterContext);
            }
        }
    }
}