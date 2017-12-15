using System.ComponentModel.DataAnnotations;

namespace BiraIssueTrackerCore.Models
{
	public enum State
	{
		Open,
		[Display(Name = "In Progress")]
		InProgress,
		Done,
		Closed
	}
}