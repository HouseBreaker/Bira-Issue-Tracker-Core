using System.ComponentModel.DataAnnotations;

namespace BiraIssueTrackerCore.Web.Models.Identity.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
