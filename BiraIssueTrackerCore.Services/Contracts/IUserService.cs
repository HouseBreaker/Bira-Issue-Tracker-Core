using System.Linq;

namespace BiraIssueTrackerCore.Services.Contracts
{
	public interface IUserService
	{
		TModel ByEmail<TModel>(string email);

		bool Exists(string email);

		IQueryable<TModel> All<TModel>();

		IQueryable<TModel> StartingWithEmail<TModel>(string email);
	}
}
