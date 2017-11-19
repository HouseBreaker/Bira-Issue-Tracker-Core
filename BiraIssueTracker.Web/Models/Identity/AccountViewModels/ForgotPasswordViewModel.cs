using System.ComponentModel.DataAnnotations;

namespace BiraIssueTrackerCore.Web.Models.Identity.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
