﻿using System.Linq;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
	public class IssuesController : Controller
	{
		private readonly IIssueService issueService;
		public IssuesController(IIssueService issueService)
		{
			this.issueService = issueService;
		}

		[TempData]
		public string ErrorMessage { get; set; }
		
		public IActionResult Index()
		{
			var issues = issueService.All<IssueViewModel>().ToArray();
			SetAuthorizationState(issues);

			return View(issues);
		}

		[Authorize]
		public IActionResult Mine()
		{
			var issues = issueService.ByAuthor<IssueViewModel>(User.Identity.Name).ToArray();
			SetAuthorizationState(issues);

			return View(issues);
		}

		[Authorize]
		public IActionResult AssignedToMe()
		{
			var issues = issueService.ByAssignee<IssueViewModel>(User.Identity.Name).ToArray();
			SetAuthorizationState(issues);

			return View(issues);
		}

		public IActionResult Details(int id)
		{
			var issue = issueService.ById<IssueViewModel>(id);
			SetAuthorizationState(issue);

			return View(issue);
		}

		public IActionResult Tagged(string id)
		{
			var issues = issueService.ByTag<IssueViewModel>(id).ToArray();
			SetAuthorizationState(issues);

			ViewData["tagSlug"] = id;

			return View(issues);
		}
		
		[Authorize]
		public IActionResult Delete(int id)
		{
			var issue = issueService.ById<IssueViewModel>(id);

			return View(issue);
		}

		[HttpPost]
		[Authorize]
		public IActionResult DeleteConfirm(int id)
		{
			if (!issueService.Exists(id))
			{
				return RedirectToAction("Index");
			}

			issueService.Delete(id);

			return RedirectToAction("Index");
		}

		private void SetAuthorizationState(params IssueViewModel[] issues)
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
