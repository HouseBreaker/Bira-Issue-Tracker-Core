using System.Diagnostics;
using BiraIssueTrackerCore.Web.Models;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
    public class HomeController : Controller
    {
	    private readonly IIssueService issueService;

	    public HomeController(IIssueService issueService)
	    {
		    this.issueService = issueService;
	    }

		public ActionResult Index()
		{
			//var issues = issueService.All<IssueHomePageViewModel>();

		    return View();
	    }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
