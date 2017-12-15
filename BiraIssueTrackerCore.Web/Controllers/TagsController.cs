using System.Linq;
using BiraIssueTrackerCore.Services.Contracts;
using BiraIssueTrackerCore.Web.Models.IssueTracker;
using Microsoft.AspNetCore.Mvc;

namespace BiraIssueTrackerCore.Web.Controllers
{
    public class TagsController : Controller
    {
	    private readonly ITagService tagService;

	    public TagsController(ITagService tagService)
	    {
		    this.tagService = tagService;
	    }

        public IActionResult Index()
        {
	        var tags = tagService.All<TagListViewModel>().ToArray();

	        return View(tags);
        }
    }
}