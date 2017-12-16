using System;
using System.Collections.Generic;
using System.Linq;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
	public class IssuesController : Controller
	{
		private readonly IIssueService issueService;
		private readonly IUserService userService;

		public IssuesController(IIssueService issueService, IUserService userService)
		{
			this.issueService = issueService;
			this.userService = userService;
		}

		public IActionResult Index()
		{
			var issues = issueService.All<IssueViewModel>()
				.ToArray();
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
			if (issue == null)
			{
				return RedirectToAction("Index");
			}

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
		public IActionResult New()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public IActionResult New(IssueCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			var assigneeExists = userService.Exists(model.AssigneeEmail);
			if (!assigneeExists)
			{
				return RedirectToAction("Index");
			}

			var tagNames = SplitTags(model.Tags).ToArray();

			var issue = issueService.Create(
				model.Title,
				model.Description,
				model.State,
				User.Identity.Name,
				model.AssigneeEmail,
				DateTime.Now,
				tagNames
			);

			return RedirectToAction("Details", new { id = issue.Id });
		}

		[Authorize]
		public IActionResult Edit(int id)
		{
			var issue = issueService.ById<IssueEditViewModel>(id);

			if (issue == null)
			{
				return RedirectToAction("Index");
			}

			if (User.IsInRole("Administrators") || issueService.IsAuthor(id, User.Identity.Name))
			{
				return View("Edit", issue);
			}

			if (issueService.IsAssignee(id, User.Identity.Name))
			{
				return View("EditState", issue);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, IssueEditViewModel model)
		{
			if (!issueService.Exists(id))
			{
				return RedirectToAction("Index");
			}

			if (!ModelState.IsValid)
			{
				return RedirectToAction("Details", new {id});
			}

			if (!User.IsInRole("Administrators") && !issueService.IsAuthor(id, User.Identity.Name))
			{
				return RedirectToAction("Index");
			}

			var assigneeExists = userService.Exists(model.AssigneeEmail);
			if (!assigneeExists)
			{
				return RedirectToAction("Index");
			}

			issueService.Edit(id,
				model.Title,
				model.Description,
				model.State,
				model.AssigneeEmail,
				SplitTags(model.Tags)
			);

			return RedirectToAction("Details", new {id});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditState(int id, IssueEditViewModel model)
		{
			if (!issueService.Exists(id))
			{
				return RedirectToAction("Index");
			}

			if (!User.IsInRole("Administrators") && !issueService.IsAssignee(id, User.Identity.Name))
			{
				return RedirectToAction("Index");
			}

			issueService.Edit(id, model.State);

			return RedirectToAction("Details", new { id });
		}

		[Authorize]
		public IActionResult Delete(int id)
		{
			if (!issueService.Exists(id))
			{
				return RedirectToAction("Index");
			}

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

		public IActionResult By(string id)
		{
			if (!userService.Exists(id))
			{
				return RedirectToAction("Index");
			}

			var issues = issueService.ByAuthor<IssueViewModel>(id);

			ViewData["Email"] = id;

			return View(issues);
		}

		public IActionResult AssignedTo(string id)
		{
			if (!userService.Exists(id))
			{
				return RedirectToAction("Index");
			}

			var issues = issueService.ByAssignee<IssueViewModel>(id);

			ViewData["Email"] = id;

			return View(issues);
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

				issue.UserCanEdit = userIsAuthor || isAdmin;
				issue.UserCanChangeState = userIsAssignee;
			}
		}

		private static IEnumerable<string> SplitTags(string tagsString)
		{
			var splitTags = tagsString
				.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
				.Select(a => a.Trim());

			return splitTags;
		}
	}
}