namespace WhoCanHelpMe.Web.Controllers.Error
{
    #region Using Directives

    using System.Web.Mvc;

    using Shared.Mappers.Contracts;

    #endregion

    /// <summary>
    /// error controller.
    /// </summary>
    public class ErrorController : BaseController
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public ErrorController(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        /// <summary>
        /// Action that deals with Unhandled Server Exceptions
        /// </summary>
        /// <returns>
        /// Unhandled Error View.
        /// </returns>
        public ActionResult Error()
        {
            this.Response.StatusCode = 500;
            this.Response.StatusDescription = "Internal Server Error";
            this.Response.TrySkipIisCustomErrors = true;

            var viewModel = this.pageViewModelBuilder.Get();

            return this.View(viewModel);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult InvalidInput()
        {
            this.Response.StatusCode = 400;
            this.Response.StatusDescription = "Bad Request";
            this.Response.TrySkipIisCustomErrors = true;

            var viewModel = this.pageViewModelBuilder.Get();

            return this.View(viewModel);
        }

        /// <summary>
        /// Not found action.
        /// </summary>
        /// <returns>
        /// Not found view
        /// </returns>
        public ActionResult NotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.StatusDescription = "Not Found";
            this.Response.TrySkipIisCustomErrors = true;

            var viewModel = this.pageViewModelBuilder.Get();

            return this.View(viewModel);
        }
    }
}