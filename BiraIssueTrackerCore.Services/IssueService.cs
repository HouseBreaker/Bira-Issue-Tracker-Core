using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Models;
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

		public void Edit(
			int id,
			string title = null,
			string description = null,
			State state = State.InProgress,
			string authorEmail = null,
			string assigneeEmail = null,
			DateTime date = new DateTime())
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			var issue = context.Issues.Find(id);

			context.Issues.Remove(issue);

			context.SaveChanges();
		}

		public bool Exists(int id)
			=> context.Issues.Any(i => i.Id == id);

		public TModel ById<TModel>(int id)
			=> By<TModel>(i => i.Id == id)
				.Single();

		public IQueryable<TModel> All<TModel>()
			=> By<TModel>();

		public IQueryable<TModel> ByState<TModel>(State state)
			=> By<TModel>(i => i.State == state);

		public IQueryable<TModel> ByAuthor<TModel>(string authorEmail)
			=> By<TModel>(i => i.Author.Email == authorEmail);

		public IQueryable<TModel> ByAssignee<TModel>(string assigneeEmail)
			=> By<TModel>(i => i.Assignee.Email == assigneeEmail);

		public IQueryable<TModel> ByTag<TModel>(string tagSlug)
			=> By<TModel>(i => i.IssueTags.Any(it => it.Tag.Slug == tagSlug));

		public IQueryable<TModel> ByStates<TModel>(params State[] states)
			=> By<TModel>(i => states.Contains(i.State));

		public IQueryable<TModel> ByAuthorAndState<TModel>(string email, IEnumerable<State> states)
			=> By<TModel>(i => i.Author.Email == email && states.Contains(i.State));

		private IQueryable<TModel> By<TModel>(Expression<Func<Issue, bool>> predicate = null)
			=> this.context.Issues
				.AsQueryable()
				.Where(predicate ?? (i => true))
				.ProjectTo<TModel>();
	}
}