using Microsoft.AspNetCore.Identity;

namespace BiraIssueTrackerCore.Models.Identity
{
    public class ApplicationUser : IdentityUser
	{
	    public string FullName { get; set; }
    }
}
