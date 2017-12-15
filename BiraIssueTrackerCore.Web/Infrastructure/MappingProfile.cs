using System.Linq;
using AutoMapper;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Web.Models.IssueTracker;

namespace BiraIssueTrackerCore.Web.Infrastructure
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Issue, IssueViewModel>()
				.ForMember(
					ivm => ivm.Tags,
					cfg => cfg.MapFrom(
						issue => issue.IssueTags.Select(it => it.Tag)
					)
				);

			CreateMap<Tag, TagListViewModel>()
				.ForMember(
					tlvm => tlvm.IssuesCount,
					cfg => cfg.MapFrom(tag => tag.IssueTags.Count)
				);
		}
	}
}