namespace BiraIssueTrackerCore.Models
{
	public class IssueTag
	{
		public int IssueId { get; set; }

		public Issue Issue { get; set; }

		public int TagId { get; set; }

		public Tag Tag { get; set; }
	}
}