using System.Collections.Generic;
using System.Linq;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiraIssueTrackerCore.Services
{
	public class IssueService : IIssueService
	{
		private readonly IssueTrackerDbContext context;

		public IssueService(IssueTrackerDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Issue> GetAllIssues() =>
			this.context
			.Issues
			.Include(i => i.Author)
			.Include(i => i.Assignee)
			.Include(i => i.IssueTags)
				.ThenInclude(it => it.Tag)
			.ToArray();
	}
}