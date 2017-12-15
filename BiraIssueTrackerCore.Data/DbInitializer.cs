using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiraIssueTrackerCore.Models;
using BiraIssueTrackerCore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BiraIssueTrackerCore.Data
{
	public static class DbInitializer
	{
		public static async void Seed(IServiceProvider serviceProvider)
		{
			using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<IssueTrackerDbContext>();

				if (context.Database.GetPendingMigrations().Any())
				{
					return;
				}

				// TODO: move sample data out into JSON files
				if (!context.Users.Any())
				{
					var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
					var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

					await CreateUser(userManager, "admin@gmail.com", "123", "Vladi Admina");
					await CreateUser(userManager, "gosho@gmail.com", "123", "George Petrov");
					await CreateUser(userManager, "pesho@gmail.com", "123", "Peter Ivanov");
					await CreateUser(userManager, "merry@gmail.com", "123", "Maria Petrova");

					await CreateRole(roleManager, "Administrators");
					await AddUserToRole(userManager, "admin@gmail.com", "Administrators");

					context.SaveChanges();
				}

				if (!context.Issues.Any())
				{
					CreateIssue(context,
						"HTTP 400 error",
						"Adding an author with incorrect date format will return an HTTP 400 response code.",
						State.Open,
						"admin@gmail.com",
						"gosho@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "user input"),
							FindTagByNameOrCreate(context, "network"),
							FindTagByNameOrCreate(context, "date"),
							FindTagByNameOrCreate(context, "HTTP 400"),
							FindTagByNameOrCreate(context, "author"),
						}
					);

					CreateIssue(context,
						"\'00\' birthday causes exception",
						"Adding an author with a birth date which has day 00 will add the author with a birth date of the last day of the previous month",
						State.Open,
						"pesho@gmail.com",
						"merry@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "birth date"),
							FindTagByNameOrCreate(context, "form"),
							FindTagByNameOrCreate(context, "error"),
							FindTagByNameOrCreate(context, "input validation"),
						}
					);

					CreateIssue(context,
						"negative birthday causes exception",
						"Adding an author with a birth date the day of which is negative will add the author with a birth date which is the same but the day is non negative",
						State.Open,
						"merry@gmail.com",
						"gosho@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "birthday"),
							FindTagByNameOrCreate(context, "error"),
							FindTagByNameOrCreate(context, "input validation"),
						}
					);

					CreateIssue(context,
						"no last name causes exception",
						"Adding an author with a valid first name and date, but no last name throws an unhandled exception \"Last name out of range\" insteaof validating the input before sending it.",
						State.Open,
						"gosho@gmail.com",
						"admin@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "form"),
							FindTagByNameOrCreate(context, "last name"),
							FindTagByNameOrCreate(context, "date"),
						}
					);

					CreateIssue(context,
						"missing first name causes exception",
						"Adding an author with a valid last name and date, but no first name throws an unhandled exception \"First name out of range\" instead of validating the input before sending it.",
						State.Open,
						"pesho@gmail.com",
						"gosho@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "form"),
							FindTagByNameOrCreate(context, "last name"),
							FindTagByNameOrCreate(context, "date"),
						}
					);

					CreateIssue(context,
						"short first name causes exception",
						"If first name is too short, the system throws an unhandled exception \"First name out of range\" instead of validating the input before sending it.",
						State.Open,
						"admin@gmail.com",
						"admin@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "first name"),
							FindTagByNameOrCreate(context, "error"),
							FindTagByNameOrCreate(context, "form"),
						}
					);

					CreateIssue(context,
						"short last name causes exception",
						"If last name is too short, the system throws an unhandled exception \"Last name out of range\" instead of validating the input before sending it.",
						State.Open,
						"merry@gmail.com",
						"gosho@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "last name"),
							FindTagByNameOrCreate(context, "error"),
							FindTagByNameOrCreate(context, "form"),
						}
					);

					CreateIssue(context,
						"Wrong first name upper limit",
						"First name upper limit is 239 characters instead of 240",
						State.Open,
						"merry@gmail.com",
						"admin@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "back end"),
							FindTagByNameOrCreate(context, "form"),
							FindTagByNameOrCreate(context, "error"),
						}
					);

					CreateIssue(context,
						"Wrong last name upper limit",
						"Last name upper limit is 237 characters instead of 240",
						State.Open,
						"gosho@gmail.com",
						"gosho@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "back end"),
							FindTagByNameOrCreate(context, "form"),
							FindTagByNameOrCreate(context, "error"),
						}
					);

					CreateIssue(context,
						"Tags don't work",
						"Last name upper limit is 237 characters instead of 240",
						State.Open,
						"gosho@gmail.com",
						"gosho@gmail.com",
						new SortedSet<Tag>
						{
							FindTagByNameOrCreate(context, "back end"),
							FindTagByNameOrCreate(context, "form"),
							FindTagByNameOrCreate(context, "error"),
						}
					);

					context.SaveChanges();
				}
			}
		}

		private static Tag FindTagByNameOrCreate(IssueTrackerDbContext db, string name)
		{
			var foundTag = db.Tags.SingleOrDefault(t => t.Name == name);

			if (foundTag == null)
			{
				var newTag = new Tag(name);
				db.Tags.Add(newTag);

				db.SaveChanges();

				return newTag;
			}

			return foundTag;
		}

		private static async Task CreateUser(UserManager<ApplicationUser> userManager,
			string email, string password, string fullName)
		{
			var user = new ApplicationUser
			{
				UserName = email,
				Email = email,
				FullName = fullName
			};

			var userCreateResult = await userManager.CreateAsync(user, password);
			if (!userCreateResult.Succeeded)
			{
				throw new Exception(string.Join("; ", userCreateResult.Errors));
			}
		}

		private static void CreateIssue(IssueTrackerDbContext context, string title, string description, State state,
			string author, string assignee, ISet<Tag> tags)
		{
			var authorAsUser = context.Users.SingleOrDefault(u => u.UserName == author);
			var assigneeAsUser = context.Users.SingleOrDefault(u => u.UserName == assignee);

			var issue = new Issue
			{
				Title = title,
				Description = description,
				State = state,
				Author = authorAsUser,
				Assignee = assigneeAsUser,
				Date = DateTime.Now
			};

			issue.IssueTags = tags
				.Select(tag => new IssueTag { Issue = issue, Tag = tag })
				.ToArray();

			context.Issues.Add(issue);
		}

		private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
		{
			var roleCreateResult = await roleManager.CreateAsync(new IdentityRole(roleName));
			if (!roleCreateResult.Succeeded)
			{
				throw new InvalidOperationException(string.Join("; ", roleCreateResult.Errors));
			}
		}

		private static async Task AddUserToRole(UserManager<ApplicationUser> userManager, string username, string roleName)
		{
			var user = await userManager.FindByEmailAsync(username);

			var addRoleResult = await userManager.AddToRoleAsync(user, roleName);
			if (!addRoleResult.Succeeded)
			{
				throw new InvalidOperationException(string.Join("; ", addRoleResult.Errors));
			}
		}
	}
}