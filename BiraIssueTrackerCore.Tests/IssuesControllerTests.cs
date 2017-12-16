using System.Linq;
using System.Security.Principal;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Controllers;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BiraIssueTrackerCore.Tests
{
	[TestFixture]
	public class IssuesControllerTests
	{
		[Test]
		public void IndexAction_ShouldReturnSuccessfully()
		{
			var mockIssueService = new Mock<IIssueService>();
			mockIssueService
				.Setup(service => service.All<IssueViewModel>())
				.Returns(PopulateIssues());

			var issuesController =
				new IssuesController(mockIssueService.Object, null)
				{
					ControllerContext = SetupControllerContextMock("gosho@gmail.com", new string[0])
				};


			var result = issuesController.Index();
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

		private static IQueryable<IssueViewModel> PopulateIssues()
		{
			return new[]
			{
				new IssueViewModel {AuthorEmail = "gosho@gmail.com", AssigneeEmail = "pesho@gmail.com"},
			}.AsQueryable();
		}
	}
}