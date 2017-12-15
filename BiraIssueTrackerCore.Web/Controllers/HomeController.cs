using System.Diagnostics;
using System.Linq;
using AutoMapper;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Web.Models;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.Identity;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IIssueService issueService;
		private readonly IUserService userService;

		public HomeController(IIssueService issueService, IUserService userService)
		{
			this.issueService = issueService;
			this.userService = userService;
		}

		public ActionResult Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return View();
			}

			var currentUserEmail = User.Identity.Name;

			var states = new[] { State.Open, State.InProgress };
			var openIssues = issueService.ByStates<IssueViewModel>(states);

			var assignedIssues = openIssues.Where(i => i.AssigneeEmail == currentUserEmail);

			var username = userService.ByEmail<UserViewModel>(currentUserEmail).FullName.Split(' ')[0];

			var viewmodel = new IssueHomePageViewModel
			{
				Username = username,
				OpenIssueCount = openIssues.Count(),
				AssignedOpenIssuesCount = assignedIssues.Count()
			};

			return View(viewmodel);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}