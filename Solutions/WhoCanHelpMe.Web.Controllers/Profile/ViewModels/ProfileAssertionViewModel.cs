namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using System.Diagnostics;

    #endregion

    [DebuggerDisplay("{CategoryName} {TagName}")]
    public class ProfileAssertionViewModel
    {
        public string CategoryName { get; set; }

        public int Id { get; set; }

        public string TagName { get; set; }
    }
}