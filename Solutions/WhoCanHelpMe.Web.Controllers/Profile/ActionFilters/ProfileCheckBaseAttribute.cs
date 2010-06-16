namespace WhoCanHelpMe.Web.Controllers.Profile.ActionFilters
{
    #region Using Directives

    using System.Web.Mvc;
    using System.Web.Routing;

    using Domain;
    using Domain.Contracts.Tasks;

    using Microsoft.Practices.ServiceLocation;

    using WhoCanHelpMe.Framework.Security;

    #endregion

    public abstract class ProfileCheckBaseAttribute : ActionFilterAttribute
    {
        private readonly IIdentityService identityService;

        private readonly IProfileTasks profileTasks;

        private readonly string redirectAction;

        private readonly string redirectController;

        protected ProfileCheckBaseAttribute(
            string redirectController,
            string redirectAction)
        {
            this.redirectController = redirectController;
            this.redirectAction = redirectAction;
            this.identityService = ServiceLocator.Current.GetInstance<IIdentityService>();
            this.profileTasks = ServiceLocator.Current.GetInstance<IProfileTasks>();
        }

        protected void DoRedirect(ActionExecutingContext filterContext)
        {
            var routeValues = new
                {
                    controller = this.redirectController,
                    action = this.redirectAction
                };

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(routeValues));
        }

        protected Profile GetProfile()
        {
            var identity = this.identityService.GetCurrentIdentity();

            return this.profileTasks.GetProfileByUserName(identity.Name);
        }
    }
}