using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Web.Infrastructure;

namespace BiraIssueTrackerCore.Web.Models.Identity.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordConfiguration.MaxLength, ErrorMessage = PasswordConfiguration.ErrorMessage, MinimumLength = PasswordConfiguration.MinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = PasswordConfiguration.ConfirmationMessage)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
