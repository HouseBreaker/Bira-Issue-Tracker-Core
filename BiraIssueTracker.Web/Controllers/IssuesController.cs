using System.Collections.Generic;
using System.Threading.Tasks;
using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class IssuesController : Controller
	{
		private readonly IIssueService issueService;

		public IssuesController(IIssueService issueService)
		{
			this.issueService = issueService;
		}

		[TempData]
		public string ErrorMessage { get; set; }

		[HttpGet]
		public IActionResult Index()
		{
			var issues = issueService.All<IssueViewModel>();
			SetAuthorizationState(issues);

			return View(issues);
		}

		[HttpGet]
		[Authorize]
		public IActionResult Mine()
		{
			var issues = issueService.All<IssueViewModel>(User.Identity.Name);

			return View(issues);
		}

		private void SetAuthorizationState(IEnumerable<IssueViewModel> issues)
		{
			foreach (var issue in issues)
			{
				var isAuthenticated = User.Identity.IsAuthenticated;

				if (!isAuthenticated)
				{
					continue;
				}

				var isAdmin = User.IsInRole("Administrators");
				var userIsAuthor = issue.AuthorEmail == User.Identity.Name;
				var userIsAssignee = issue.AssigneeEmail == User.Identity.Name;

				var userIsAuthorizedToEdit = isAdmin || userIsAuthor || userIsAssignee;

				if (!userIsAuthorizedToEdit)
				{
					continue;
				}

				issue.UserIsAuthor = userIsAuthor;
				issue.UserIsAssignee = userIsAssignee;
			}
		}
	}
}
