using System;
using System.Collections.Generic;
using System.Linq;
using BiraIssueTrackerCore.Models;

namespace BiraIssueTrackerCore.Services.Contracts
{
	public interface IIssueService
	{
		Issue Create(string title,
			string description,
			State state,
			string authorId,
			string assigneeId,
			DateTime date,
			IEnumerable<string> tagNames);

		Issue Edit(int id,
			string title,
			string description,
			State state,
			string assigneeEmail,
			IEnumerable<string> tagStrings
		);

		Issue Edit(int id, State state);

		void Delete(int id);

		bool Exists(int id);

		TModel ById<TModel>(int id);

		IQueryable<TModel> All<TModel>();

		IQueryable<TModel> ByState<TModel>(State state);

		IQueryable<TModel> ByAuthor<TModel>(string authorEmail);

		IQueryable<TModel> ByAssignee<TModel>(string assigneeEmail);

		IQueryable<TModel> ByTag<TModel>(string tagSlug);

		IQueryable<TModel> ByStates<TModel>(params State[] states);

		bool IsAuthor(int issueId, string email);

		bool IsAssignee(int issueId, string email);
	}
}