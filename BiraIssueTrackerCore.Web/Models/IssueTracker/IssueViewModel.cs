using System;
using System.Collections.Generic;
using System.ComponentModel;
using BiraIssueTrackerCore.Models;

namespace BiraIssueTrackerCore.Web.Models.IssueTracker
{
    public class IssueViewModel
    {
		public int Id { get; set; }
		
		public string Title { get; set; }
		
		public string Description { get; set; }
		
		public State State { get; set; }
		
		[DisplayName("Author")]
		public string AuthorEmail { get; set; }

	    [DisplayName("Assignee")]
		public string AssigneeEmail { get; set; }

	    public DateTime Date { get; set; }

		public ICollection<TagViewModel> Tags { get; set; }

	    public bool UserCanEdit { get; set; }

		public bool UserCanChangeState { get; set; }
	}
}
