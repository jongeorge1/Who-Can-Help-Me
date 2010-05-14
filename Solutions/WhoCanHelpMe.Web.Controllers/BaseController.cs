namespace WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    #endregion

    public class BaseController : Controller
    {
        /// <summary>
        /// Stores the site master name
        /// </summary>
        const string MasterName = "Site";

        /// <summary>
        /// Returns a view result with default view and master name
        /// </summary>
        /// <returns>
        /// View Result
        /// </returns>
        protected new ViewResult View()
        {
            return this.View(string.Empty, MasterName);
        }

        /// <summary>
        /// Returns a view result with specified view name and the default master name
        /// </summary>
        /// <param name="viewName">
        /// The view name.
        /// </param>
        /// <returns>
        /// View Result
        /// </returns>
        protected new ViewResult View(string viewName)
        {
            return this.View(viewName, MasterName);
        }

        /// <summary>
        /// Returns a view result with default view and master name
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// View Result
        /// </returns>
        protected new ViewResult View(object model)
        {
            return this.View(string.Empty, MasterName, model);
        }
    }

}