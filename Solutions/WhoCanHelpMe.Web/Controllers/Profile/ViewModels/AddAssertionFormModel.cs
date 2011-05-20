namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddAssertionFormModel
    {
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [Display(Name ="Tag Name")]
        public string TagName { get; set; }
    }
}