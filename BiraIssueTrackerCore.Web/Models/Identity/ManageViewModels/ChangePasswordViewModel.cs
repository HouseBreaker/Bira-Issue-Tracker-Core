using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Web.Infrastructure;

namespace BiraIssueTrackerCore.Web.Models.Identity.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(PasswordConfiguration.MaxLength, ErrorMessage = PasswordConfiguration.ErrorMessage, MinimumLength = PasswordConfiguration.MinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = PasswordConfiguration.ConfirmationMessage)]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
