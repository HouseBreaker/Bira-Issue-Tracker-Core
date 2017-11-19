using System;
using System.Collections.Generic;

namespace BiraIssueTrackerCore.Web.Models.IssueTracker
{
    public class IssueViewModel
    {
		public int Id { get; set; }
		
		public string Title { get; set; }
		
		public string Description { get; set; }
		
		public string State { get; set; }
		
		public string AuthorEmail { get; set; }

		public string AssigneeEmail { get; set; }

	    public DateTime Date { get; set; }

		public ICollection<TagViewModel> Tags { get; set; }

	    public bool UserIsAuthor { get; set; }

		public bool UserIsAssignee { get; set; }
	}
}
