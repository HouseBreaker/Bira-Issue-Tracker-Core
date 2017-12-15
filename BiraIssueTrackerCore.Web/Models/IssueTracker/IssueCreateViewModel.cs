using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiraIssueTrackerCore.Web.Models.IssueTracker
{
	public class IssueCreateViewModel
	{
		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public string State { get; set; }

		[DisplayName("Assignee")]
		[DataType(DataType.EmailAddress)]
		public string AssigneeEmail { get; set; }

		[DisplayName("Tags (comma-separated)")]
		public string Tags { get; set; }
	}
}