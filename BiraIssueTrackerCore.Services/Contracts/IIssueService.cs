using System;
using System.Collections.Generic;
using BiraIssueTrackerCore.Data.Models;

namespace BiraIssueTrackerCore.Services.Contracts
{
    public interface IIssueService
    {
	    void Create(string title,
		    string description,
		    State state,
		    string authorId,
		    string assigneeId,
		    DateTime date,
		    IEnumerable<Tag> tags);

	    void Edit(int id,
			string title = null,
			string description = null,
			State state = default(State),
			string authorEmail = null,
			string assigneeEmail = null,
		    DateTime date = default(DateTime));

	    void Delete(int id);

	    TModel ById<TModel>(int id);

		IEnumerable<TModel> All<TModel>();

	    IEnumerable<TModel> ByAuthor<TModel>(string authorEmail);

	    IEnumerable<TModel> ByAssignee<TModel>(string assigneeEmail);

	    IEnumerable<TModel> ByTag<TModel>(string tagSlug);
    }
}
