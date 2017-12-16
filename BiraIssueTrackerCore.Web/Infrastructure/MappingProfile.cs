using System.Linq;
using AutoMapper;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Models.Identity;
using BiraIssueTrackerCore.Web.Models.Identity;
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

			CreateMap<ApplicationUser, UserListViewModel>()
				.ForMember(
					ulvm => ulvm.CreatedIssuesCount,
					cfg => cfg.MapFrom(user => user.CreatedIssues.Count)
				)
				.ForMember(
					ulvm => ulvm.AssignedIssuesCount,
					cfg => cfg.MapFrom(user => user.AssignedIssues.Count)
				);

			CreateMap<ApplicationUser, UserProfileViewModel>()
				.ForMember(
					upvm => upvm.CreatedIssues,
					cfg => cfg.MapFrom(u => u.CreatedIssues)
				)
				.ForMember(
					upvm => upvm.AssignedIssues,
					cfg => cfg.MapFrom(u => u.AssignedIssues)
				);

			CreateMap<Issue, IssueEditViewModel>()
				.ForMember(
					ievm => ievm.Tags,
					cfg => cfg.MapFrom(i => string.Join(", ", i.IssueTags.Select(t => t.Tag.Name)))
				);
		}
	}
}