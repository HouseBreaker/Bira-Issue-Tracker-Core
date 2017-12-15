using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Web.Models.IssueTracker;

namespace BiraIssueTrackerCore.Web.Models.Identity
{
	public class UserProfileViewModel
	{
		[Display(Name = "Full Name")]
		public string FullName { get; set; }

		public string Email { get; set; }

		[Display(Name = "Created Issues")]
		public IEnumerable<IssueViewModel> CreatedIssues { get; set; }

		[Display(Name = "Assigned Issues")]
		public IEnumerable<IssueViewModel> AssignedIssues { get; set; }

		public int SolvedIssuesRatio { get; set; }
	}
}