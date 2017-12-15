using System.ComponentModel.DataAnnotations;

namespace BiraIssueTrackerCore.Web.Models.IssueTracker
{
	public class UserListViewModel
	{
		[Display(Name = "Full Name")]
		public string FullName { get; set; }

		public string Email { get; set; }

		[Display(Name = "Created Issues")]
		public int CreatedIssuesCount { get; set; }

		[Display(Name = "Assigned Issues")]
		public int AssignedIssuesCount { get; set; }
	}
}