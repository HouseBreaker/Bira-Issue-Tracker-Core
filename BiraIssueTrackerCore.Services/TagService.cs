using System.Linq;
using AutoMapper.QueryableExtensions;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Services.Contracts;

namespace BiraIssueTrackerCore.Services
{
	public class TagService : ITagService
	{
		private readonly IssueTrackerDbContext context;

		public TagService(IssueTrackerDbContext context)
		{
			this.context = context;
		}

		public Tag Create(string name)
		{
			var tag = new Tag(name);

			context.Tags.Add(tag);

			context.SaveChanges();

			return tag;
		}

		public Tag ByName(string name)
			=> context.Tags.SingleOrDefault(t => t.Name == name);

		public Tag BySlug(string slug)
			=> context.Tags.SingleOrDefault(t => t.Slug == slug);

		public IQueryable<TModel> All<TModel>()
			=> context.Tags.AsQueryable().ProjectTo<TModel>();
	}
}