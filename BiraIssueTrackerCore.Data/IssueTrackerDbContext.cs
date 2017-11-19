using BiraIssueTrackerCore.Data.Models;
using BiraIssueTrackerCore.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BiraIssueTrackerCore.Data
{
    public class IssueTrackerDbContext : IdentityDbContext<ApplicationUser>
    {
        public IssueTrackerDbContext(DbContextOptions<IssueTrackerDbContext> options)
            : base(options)
        {
        }

		public DbSet<Issue> Issues { get; set; }

		public DbSet<Tag> Tags { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IssueTag>()
				.ToTable("IssueTags")
				.HasKey(it => new { it.IssueId, it.TagId });
		}
	}
}
