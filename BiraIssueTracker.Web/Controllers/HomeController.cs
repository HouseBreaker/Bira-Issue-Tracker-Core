using System.Diagnostics;
using BiraIssueTrackerCore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
    public class HomeController : Controller
    {
		public ActionResult Index()
	    {
		    return View();
	    }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
