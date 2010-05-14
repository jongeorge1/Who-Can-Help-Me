namespace System.Web.Mvc
{
    #region Using Directives

    using xVal.Html;

    #endregion

    public static class HtmlHelperExtensions
    {
        public static ValidationInfo ClientSideValidationFor<T>(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ClientSideValidation(typeof(T).Name, typeof(T));
        }
    }
}