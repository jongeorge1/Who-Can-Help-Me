namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Diagnostics;

    using Shared.ViewModels;

    #endregion

    /// <summary>
    /// The profile view model.
    /// </summary>
    [DebuggerDisplay("{FirstName} {LastName}")]
    public class ViewProfilePageViewModel : PageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProfilePageViewModel"/> class.
        /// </summary>
        public ViewProfilePageViewModel()
        {
            this.Assertions = new List<ProfileAssertionViewModel>();
        }

        /// <summary>
        /// Gets or sets Assertions.
        /// </summary>
        public IList<ProfileAssertionViewModel> Assertions { get; set; }

        /// <summary>
        /// Gets or sets FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        public string LastName { get; set; }
    }
}