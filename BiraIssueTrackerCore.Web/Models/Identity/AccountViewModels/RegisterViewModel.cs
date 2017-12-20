using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Web.Infrastructure;
using BiraIssueTrackerCore.Web.Attributes;

namespace BiraIssueTrackerCore.Web.Models.Identity.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Full Name")]
		[FullName(ErrorMessage = "The Full Name must consist of at least 2 names separated by a space")]
		public string FullName { get; set; }

        [Required]
        [StringLength(PasswordConfiguration.MaxLength, ErrorMessage = PasswordConfiguration.ErrorMessage, MinimumLength = PasswordConfiguration.MinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = PasswordConfiguration.ConfirmationMessage)]
        public string ConfirmPassword { get; set; }
    }
}
