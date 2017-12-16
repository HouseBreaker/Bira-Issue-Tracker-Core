using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiraIssueTrackerCore.Services
{
	public class IssueService : IIssueService
	{
		private readonly IssueTrackerDbContext context;
		private readonly ITagService tagService;

		public IssueService(IssueTrackerDbContext context, ITagService tagService)
		{
			this.context = context;
			this.tagService = tagService;
		}

		public Issue Create(
			string title,
			string description,
			State state,
			string authorEmail,
			string assigneeEmail,
			DateTime date,
			IEnumerable<string> tagNames)
		{
			var authorId = context.Users.SingleOrDefault(u => u.Email == authorEmail)?.Id;
			var assigneeId = context.Users.SingleOrDefault(u => u.Email == assigneeEmail)?.Id;

			var tags = tagNames
				.Select(tagName => tagService.ByName(tagName) ?? tagService.Create(tagName))
				.ToArray();

			var issue = new Issue(title, description, state, authorId, assigneeId, date, tags);

			context.Issues.Add(issue);

			context.SaveChanges();

			return issue;
		}

		public Issue Edit(int id,
			string title,
			string description,
			State state,
			string assigneeEmail,
			IEnumerable<string> tagStrings
		)
		{
			var issue = context.Issues
				.Include(i => i.IssueTags)
				.SingleOrDefault(i => i.Id == id);

			issue.Title = title;
			issue.Description = description;
			issue.State = state;
			issue.AssigneeId = context.Users.Single(u => u.Email == assigneeEmail).Id;

			var newTags = tagStrings
				.Select(t => tagService.ByName(t) ?? tagService.Create(t))
				.Select(t => new IssueTag { Issue = issue, Tag = t })
				.ToArray();

			issue.IssueTags.Clear();
			context.SaveChanges();

			issue.IssueTags = newTags;
			context.SaveChanges();

			tagService.Prune();

			return issue;
		}

		public Issue Edit(int id, State state)
		{
			var issue = context.Issues.Find(id);

			issue.State = state;

			context.SaveChanges();

			return issue;
		}

		public void Delete(int id)
		{
			var issue = context.Issues.Where(i => i.Id == id);

			context.Issues.Remove(issue.SingleOrDefault());

			context.SaveChanges();

			tagService.Prune();
		}

		public bool Exists(int id)
			=> context.Issues.Any(i => i.Id == id);

		public TModel ById<TModel>(int id)
			=> By<TModel>(i => i.Id == id)
				.SingleOrDefault();

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

		public bool IsAuthor(int issueId, string email)
			=> context.Issues.SingleOrDefault(i => i.Id == issueId && i.Author.Email == email) != null;

		public bool IsAssignee(int issueId, string email)
			=> context.Issues.SingleOrDefault(i => i.Id == issueId && i.Assignee.Email == email) != null;

		public IQueryable<TModel> ByAuthorAndState<TModel>(string email, IEnumerable<State> states)
			=> By<TModel>(i => i.Author.Email == email && states.Contains(i.State));

		private IQueryable<TModel> By<TModel>(Expression<Func<Issue, bool>> predicate = null)
			=> this.context.Issues
				.AsQueryable()
				.Where(predicate ?? (i => true))
				.ProjectTo<TModel>();
	}
}