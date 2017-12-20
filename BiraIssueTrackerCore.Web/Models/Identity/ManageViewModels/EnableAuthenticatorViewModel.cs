using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Web.Infrastructure;

namespace BiraIssueTrackerCore.Web.Models.Identity.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
            [Required]
            [StringLength(1, ErrorMessage = PasswordConfiguration.ErrorMessage, MinimumLength = PasswordConfiguration.MinLength)]
            [DataType(DataType.Text)]
            [Display(Name = "Verification Code")]
            public string Code { get; set; }

            [ReadOnly(true)]
            public string SharedKey { get; set; }

            public string AuthenticatorUri { get; set; }
    }
}
