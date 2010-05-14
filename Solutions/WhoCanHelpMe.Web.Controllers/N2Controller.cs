namespace WhoCanHelpMe.Web.Controllers
{
    using System.Web.Mvc;
    using N2;
    using N2.Web.Mvc;

    /// <summary>
    /// Base N2 controller
    /// </summary>
    /// <typeparam name="T">
    /// The content item type
    /// </typeparam>
    public class N2Controller<T> : ContentController<T> where T : ContentItem
    {
        /// <summary>
        /// Stores the site master name
        /// </summary>
        private const string MasterName = "Site";

        /// <summary>
        /// Default controller action 
        /// </summary>
        /// <returns>
        /// The default view with the current CMS item as the model
        /// </returns>
        public override ActionResult Index()
        {
            return View(CurrentItem);
        }

        /// <summary>
        /// Returns a view result with default view and master name
        /// </summary>
        /// <returns>
        /// View Result
        /// </returns>
        protected new ViewResultBase View()
        {
            return View(string.Empty, MasterName, null);
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
        protected new ViewResultBase View(string viewName)
        {
            return View(viewName, MasterName, null);
        }

        /// <summary>
        /// Returns a view result with the specified view name, default master name
        /// and the specified model
        /// </summary>
        /// <param name="viewName">
        /// The view name.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// View Result
        /// </returns>
        protected new ViewResult View(string viewName, object model)
        {
            return View(viewName, MasterName, model);
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
            return View(string.Empty, MasterName, model);
        }
    }
}