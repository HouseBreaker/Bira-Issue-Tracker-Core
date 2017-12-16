using System.Linq;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.Identity;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
    public class UsersController : Controller
    {
	    private readonly IUserService userService;

	    public UsersController(IUserService userService)
	    {
		    this.userService = userService;
	    }

        public IActionResult Index()
        {
	        var users = userService.All<UserListViewModel>().ToArray();

	        return View(users);
        }

	    public IActionResult Profile(string id)
	    {
		    if (!userService.Exists(id))
		    {
			    return RedirectToAction("Index");
		    }

		    var user = userService.ByEmail<UserProfileViewModel>(id);

		    int solvedIssuesRatio;
		    if (!user.AssignedIssues.Any())
		    {
			    solvedIssuesRatio = 100;
		    }
		    else
		    {
			    solvedIssuesRatio = (int)(user.AssignedIssues.Count(a => a.State == State.Done || a.State == State.Closed) / (double)user.AssignedIssues.Count() * 100);
		    }

		    user.SolvedIssuesRatio = solvedIssuesRatio;

			return View(user);
	    }

		[HttpPost]
	    public JsonResult FindByEmail(string id)
	    {
		    var users = userService.StartingWithEmail<UserDropdownDto>(id)
				.Select(dto => dto.Email)
			    .ToArray();

		    return Json(users);
	    }
	}
}