using System.Threading.Tasks;
using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Services.Contracts;
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
			var issues = issueService.GetAllIssues();

			return View(issues);
		}
	}
}
