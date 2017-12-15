using System;
using AutoMapper;
using BiraIssueTrackerCore.Data;
using BiraIssueTrackerCore.Models.Identity;
using BiraIssueTrackerCore.Services;
using BiraIssueTrackerCore.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BiraIssueTrackerCore.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<IssueTrackerDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.Password.RequiredLength = 1;
				})
				.AddEntityFrameworkStores<IssueTrackerDbContext>()
				.AddDefaultTokenProviders();

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<IIssueService, IssueService>();
			services.AddTransient<IUserService, UserService>();

			services.AddMvc();
			services.AddAutoMapper();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				DbInitializer.Seed(serviceProvider);

				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
				name: "defaultNoAction",
				template: "{controller=Home}/",
				defaults: new { action = "Index" });

				routes.MapRoute(
				name: "default",
				template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}