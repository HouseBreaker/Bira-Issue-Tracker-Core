namespace BiraIssueTrackerCore.Services.Contracts
{
	public interface IUserService
	{
		TModel ByEmail<TModel>(string email);

		bool Exists(string email);
	}
}
