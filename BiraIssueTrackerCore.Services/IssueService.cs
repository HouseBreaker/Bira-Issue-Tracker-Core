using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Services.Contracts;

namespace BiraIssueTrackerCore.Services
{
	public class IssueService : IIssueService
	{
		private readonly IssueTrackerDbContext context;

		public IssueService(IssueTrackerDbContext context)
		{
			this.context = context;
		}

		public void Create(
			string title,
			string description,
			State state,
			string authorId,
			string assigneeId,
			DateTime date,
			IEnumerable<Tag> tags)
		{
			var issue = new Issue(title, description, state, authorId, assigneeId, date, tags);

			context.Issues.Add(issue);

			context.SaveChanges();
		}

		public void Edit(int id, string title = null, string description = null, State state = State.New, string authorEmail = null,
			string assigneeEmail = null, DateTime date = new DateTime())
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			var issue = context.Issues.Find(id);

			context.Issues.Remove(issue);

			context.SaveChanges();
		}

		public TModel ById<TModel>(int id)
		{
			return this.context
				.Issues
				.Where(i => i.Id == id)
				.ProjectTo<TModel>()
				.Single();
		}

		public IEnumerable<TModel> All<TModel>()
		{
			return By<TModel>(i => true)
				.ToArray();
		}

		public IEnumerable<TModel> ByAuthor<TModel>(string authorEmail)
		{
			var query = this.context.Issues.AsQueryable();

			if (authorEmail != null)
			{
				return By<TModel>(i => i.Author.Email == authorEmail).ToArray();
			}

			return query
				.ProjectTo<TModel>()
				.ToArray();
		}

		public IEnumerable<TModel> ByAssignee<TModel>(string assigneeEmail)
		{
			var query = this.context.Issues.AsQueryable();

			if (assigneeEmail != null)
			{
				return By<TModel>(i => i.Assignee.Email == assigneeEmail).ToArray();
			}

			return query
				.ProjectTo<TModel>()
				.ToArray();
		}

		private IEnumerable<TModel> By<TModel>(Expression<Func<Issue, bool>> predicate)
		{
			var query = this.context.Issues.AsQueryable();

			query = query.Where(predicate);

			return query
				.ProjectTo<TModel>();
		}
	}
}