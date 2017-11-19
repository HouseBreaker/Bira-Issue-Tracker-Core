using BiraIssueTrackerCore.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiraIssueTrackerCore.Data.Models
{
	public class Issue
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(200)]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Required]
		public State State { get; set; }

		[Required]
		public ApplicationUser Author { get; set; }
		
		public ApplicationUser Assignee { get; set; }

		[Required]
		public ICollection<IssueTag> IssueTags { get; set; }

		[Required]
		public DateTime Date { get; set; }
	}
}