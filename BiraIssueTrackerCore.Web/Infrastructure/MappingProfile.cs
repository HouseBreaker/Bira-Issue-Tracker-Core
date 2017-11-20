using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Web.Models.IssueTracker;

namespace BiraIssueTrackerCore.Web.Infrastructure
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CreateMap<IEnumerable<Issue>, IssueHomePageViewModel>()
			//	.ForMember(
			//		ihvm => ihvm.OpenIssueCount,
			//		cfg => cfg.MapFrom(
			//			issues => issues.Count(i => i.State == State.Open)
			//		)
			//	)
			//	.ForMember(
			//		ihvm => ihvm.AssignedOpenIssuesCount,
			//		cfg => cfg.MapFrom(
			//			issues => 999
			//		)
			//	);

			CreateMap<Issue, IssueViewModel>()
				.ForMember(
					ivm => ivm.Tags,
					cfg => cfg.MapFrom(
						issue => issue.IssueTags.Select(it => it.Tag)
					)
				);
		}
	}
}