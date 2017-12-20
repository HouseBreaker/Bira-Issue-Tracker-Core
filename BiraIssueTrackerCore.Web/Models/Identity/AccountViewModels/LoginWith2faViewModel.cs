using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Web.Infrastructure;

namespace BiraIssueTrackerCore.Web.Models.Identity.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = PasswordConfiguration.ErrorMessage, MinimumLength = PasswordConfiguration.MinLength)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
