using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Controllers;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BiraIssueTrackerCore.Tests.Controllers
{
	[TestFixture]
	public class IssuesControllerTests
	{
		[Test]
		public void IndexAction_ShouldReturnSuccessfully()
		{
			var mockIssueService = new Mock<IIssueService>();
			var issues = new[]
			{
				new IssueViewModel {AuthorEmail = "gosho@gmail.com", AssigneeEmail = "pesho@gmail.com"},
				new IssueViewModel {AuthorEmail = "someoneElse@gmail.com", AssigneeEmail = "gosho@gmail.com"},
				new IssueViewModel {AuthorEmail = "someoneElse@gmail.com", AssigneeEmail = "someoneElse@gmail.com"},
			};

			mockIssueService
				.Setup(service => service.All<IssueViewModel>())
				.Returns(PopulateIssues(
					issues));

			var issuesController =
				new IssuesController(mockIssueService.Object, null)
				{
					ControllerContext = SetupControllerContextMock("gosho@gmail.com", new string[0])
				};

			var result = issuesController.Index();

			Assert.That(result, Is.TypeOf<ViewResult>());

			var viewResult = (ViewResult) result;

			Assert.That(viewResult.Model, Is.AssignableTo<IEnumerable<IssueViewModel>>());
			var model = ((IEnumerable<IssueViewModel>) viewResult.ViewData.Model).ToArray();

			Assert.That(model.Length, Is.EqualTo(3));
			Assert.That(model[0].UserCanEdit, Is.True);

			Assert.That(model[1].UserCanChangeState, Is.True);

			Assert.That(model[2].UserCanEdit, Is.False);
			Assert.That(model[2].UserCanChangeState, Is.False);
		}

		[Test]
		public void MineAction_ShouldReturnSuccessfully()
		{
			var mockIssueService = new Mock<IIssueService>();

			var issues = new[]
			{
				new IssueViewModel {AuthorEmail = "gosho@gmail.com", AssigneeEmail = "pesho@gmail.com"},
			};

			mockIssueService
				.Setup(service => service.ByAuthor<IssueViewModel>("gosho@gmail.com"))
				.Returns(PopulateIssues(issues));

			var issuesController =
				new IssuesController(mockIssueService.Object, null)
				{
					ControllerContext = SetupControllerContextMock("gosho@gmail.com", new string[0])
				};

			var result = issuesController.Mine();

			Assert.That(result, Is.TypeOf<ViewResult>());

			var viewResult = (ViewResult) result;

			Assert.That(viewResult.Model, Is.AssignableTo<IEnumerable<IssueViewModel>>());
			var model = (IEnumerable<IssueViewModel>) viewResult.ViewData.Model;

			Assert.That(model.Count(), Is.EqualTo(1));
		}

		[Test]
		public void NewAction_ValidInput_ShouldReturnRedirectToActionResult()
		{
			var mockIssueService = new Mock<IIssueService>();
			var mockUserService = new Mock<IUserService>();

			var issuesController =
				new IssuesController(mockIssueService.Object, mockUserService.Object)
				{
					ControllerContext = SetupControllerContextMock("gosho@gmail.com", new string[0])
				};

			var issueViewModel = new IssueCreateViewModel
			{
				Title = "New issue",
				Description = "New description",
				AssigneeEmail = "gosho@gmail.com",
				State = State.InProgress,
				Tags = "one, two, three"
			};

			var result = issuesController.New(issueViewModel);

			Assert.That(result, Is.TypeOf<RedirectToActionResult>());
		}

		[Test]
		public void NewAction_InvalidAssigneeEmail_ShouldReturnRedirectToActionResult()
		{
			var mockIssueService = new Mock<IIssueService>();
			var mockUserService = new Mock<IUserService>();

			mockUserService
				.Setup(ctx => ctx.Exists("idontexist@gmail.com")).Returns(false);

			var issuesController =
				new IssuesController(mockIssueService.Object, mockUserService.Object)
				{
					ControllerContext = SetupControllerContextMock("gosho@gmail.com", new string[0])
				};

			var issueViewModel = new IssueCreateViewModel
			{
				Title = "New issue",
				Description = "New description",
				AssigneeEmail = "idontexist@gmail.com",
				State = State.InProgress,
				Tags = "one, two, three"
			};

			var result = issuesController.New(issueViewModel);

			Assert.That(result, Is.TypeOf<RedirectToActionResult>());
		}

		[Test]
		public void NewAction_InvalidInput_ShouldReturnRedirectToActionResult()
		{
			var mockIssueService = new Mock<IIssueService>();
			var mockUserService = new Mock<IUserService>();

			var issuesController =
				new IssuesController(mockIssueService.Object, mockUserService.Object)
				{
					ControllerContext = SetupControllerContextMock("gosho@gmail.com", new string[0])
				};

			var issueViewModel = new IssueCreateViewModel
			{
				Title = null,
				Description = null,
				AssigneeEmail = "pesho@gmail.com",
				State = State.InProgress,
				Tags = "one, two, three"
			};

			var result = issuesController.New(issueViewModel);

			Assert.That(result, Is.TypeOf<RedirectToActionResult>());
		}

		private static ControllerContext SetupControllerContextMock(string email, string[] roles)
		{
			var controllerContextMock = new Mock<ControllerContext>();
			var httpContext = new DefaultHttpContext();

			controllerContextMock.Object.HttpContext = httpContext;
			httpContext.User = new GenericPrincipal(
				new GenericIdentity(email),
				roles
			);

			return controllerContextMock.Object;
		}

		private static IQueryable<IssueViewModel> PopulateIssues(IEnumerable<IssueViewModel> issues)
		{
			return issues.AsQueryable();
		}
	}
}