using BiraIssueTrackerCore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class IssuesController : Controller
	{
		public IssuesController()
		{
		}

		[TempData]
		public string ErrorMessage { get; set; }

		[HttpGet]
		public IActionResult Index()
		{
			var issues = new Issue[]
			{
				new Issue() { Title = "test" }
			};

			return View(issues);
		}
	}
}
