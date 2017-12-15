using BiraIssueTrackerCore.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BiraIssueTrackerCore.Models
{
	public class Issue
	{
		public Issue()
		{
		}

		public Issue(string title, string description, State state, string authorId, string assigneeId, DateTime date, IEnumerable<Tag> tags)
		{
			this.Title = title;
			this.Description = description;
			this.State = state;
			this.AuthorId = authorId;
			this.AssigneeId = assigneeId;
			this.Date = date;

			this.IssueTags = tags
				.Select(tag => new IssueTag { Issue = this, Tag = tag })
				.ToList();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }
		
		public string Description { get; set; }

		[Required]
		public State State { get; set; }
		
		public string AuthorId { get; set; }

		[Required]
		public ApplicationUser Author { get; set; }
		
		public string AssigneeId { get; set; }

		public ApplicationUser Assignee { get; set; }

		[Required]
		public ICollection<IssueTag> IssueTags { get; set; }

		[Required]
		public DateTime Date { get; set; }
	}
}