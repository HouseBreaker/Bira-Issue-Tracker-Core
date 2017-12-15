using BiraIssueTrackerCore.Models;

namespace BiraIssueTrackerCore.Services.Contracts
{
    public interface ITagService
    {
	    Tag Create(string name);

		Tag ByName(string name);

		Tag BySlug(string slug);
    }
}
