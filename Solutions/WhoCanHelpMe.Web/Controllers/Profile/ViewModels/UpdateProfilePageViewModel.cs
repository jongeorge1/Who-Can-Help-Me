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
    public class UpdateProfilePageViewModel : PageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProfilePageViewModel"/> class.
        /// </summary>
        public UpdateProfilePageViewModel()
        {
            this.Assertions = new List<ProfileAssertionViewModel>();
            this.Categories = new List<CategoryViewModel>();
            this.FormModel = new AddAssertionFormModel();
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

        /// <summary>
        /// Gets or sets Categories.
        /// </summary>
        public IList<CategoryViewModel> Categories { get; set; }

        /// <summary>
        /// Gets or sets the form model.
        /// </summary>
        public AddAssertionFormModel FormModel { get; set; }
    }
}