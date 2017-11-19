using System;
using System.Collections.Generic;
using System.Text;
using BiraIssueTrackerCore.Data.Models;

namespace BiraIssueTrackerCore.Services.Contracts
{
    public interface IIssueService
    {
	    IEnumerable<Issue> GetAllIssues();
    }
}
