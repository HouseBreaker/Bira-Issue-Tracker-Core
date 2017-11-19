using System.Collections.Generic;
using System.Linq;
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

	    public IEnumerable<Issue> GetAllIssues()
	    {
		    return this.context.Issues.ToArray();
	    }
    }
}
