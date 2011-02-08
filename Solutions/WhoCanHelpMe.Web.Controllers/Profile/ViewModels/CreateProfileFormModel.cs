namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateProfileFormModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}