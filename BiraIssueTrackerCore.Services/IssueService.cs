using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
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

		public IEnumerable<TModel> All<TModel>(string authorId = null)
		{
			var query = this.context.Issues.AsQueryable();

			if (authorId != null)
			{
				query = query.Where(i => i.Author.Email == authorId);
			}

			return query
				.ProjectTo<TModel>()
				.ToArray();
		}
	}
}