using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BiraIssueTracker.Web.Models;

namespace BiraIssueTracker.Web.Controllers
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
