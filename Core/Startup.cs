using System;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;
using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Camera_Shop
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddMvc();

			services.AddHsts(options =>
			{
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(60);
			});

			services.AddHttpsRedirection(options =>
			{
				//TODO: Change when in production to 443
				options.HttpsPort = 5001;
			});

			services.AddDbContext<CameraContext>(options =>
				 options.UseNpgsql(Configuration.GetConnectionString("DEV")));

			services.AddIdentity<User, Role>()
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<CameraContext>();
				 
			services.AddAuthorization(opt =>
			{
				opt.AddPolicy("Logged", policy => policy.RequireRole("User"));
			});

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 5;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;

				options.User.RequireUniqueEmail = true;
			});

			services.AddHttpContextAccessor();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment() || env.IsStaging())
				app.UseExceptionHandler("/Error"); //TESTING!
				//app.UseDeveloperExceptionPage();
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();

				endpoints.MapControllerRoute(
					 name: "default",
					 pattern: "{controller=Home}/{action=Index}");
			});

			// Open the Electron-Window here
    		//Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
		}
	}
}