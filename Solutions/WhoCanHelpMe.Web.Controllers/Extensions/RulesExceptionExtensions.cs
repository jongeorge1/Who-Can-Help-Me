namespace WhoCanHelpMe.Web.Controllers.Extensions
{
    #region Using Directives

    using System.Web.Mvc;
    using xVal.ServerSide;

    #endregion

    public static class RulesExceptionExtensions
    {
        public static void AddModelStateErrors(this RulesException exception, ModelStateDictionary modelState, object obj)
        {
            exception.AddModelStateErrors(modelState, obj.GetType().Name);
        }

        public static void AddModelStateErrors(this RulesException exception, ModelStateDictionary modelState)
        {
            exception.AddModelStateErrors(modelState, string.Empty);
        }
    }
}