using System.Linq;
using AutoMapper.QueryableExtensions;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Services.Contracts;

namespace BiraIssueTrackerCore.Services
{
	public class UserService : IUserService
	{
		private readonly IssueTrackerDbContext context;

		public UserService(IssueTrackerDbContext context)
		{
			this.context = context;
		}

		public TModel ByEmail<TModel>(string email)
			=> context.Users
			.Where(u => u.Email == email)
			.ProjectTo<TModel>()
			.SingleOrDefault();

	}
}
