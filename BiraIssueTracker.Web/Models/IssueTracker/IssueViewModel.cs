using System;
using System.Collections.Generic;
using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Data.Models.Identity;

namespace BiraIssueTrackerCore.Web.Models.IssueTracker
{
    public class IssueViewModel
    {
		public int Id { get; set; }
		
		public string Title { get; set; }
		
		public string Description { get; set; }
		
		public State State { get; set; }
		
		public ApplicationUser Author { get; set; }

		public ApplicationUser Assignee { get; set; }
		
		public ICollection<IssueTag> IssueTags { get; set; }
		
		public DateTime Date { get; set; }
	}
}
