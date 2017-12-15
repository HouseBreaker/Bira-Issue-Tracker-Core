using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BiraIssueTrackerCore.Models.Identity
{
    public class ApplicationUser : IdentityUser
	{
	    public string FullName { get; set; }

		public ICollection<Issue> CreatedIssues { get; set; }

		public ICollection<Issue> AssignedIssues { get; set; }
	}
}
