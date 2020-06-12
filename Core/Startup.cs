using Camera_Shop.Database;
using Camera_Shop.Models;
using Camera_Shop.Models.Classes;
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

               services.AddDbContext<CameraContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DEV")));
               
               services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<CameraContext>();

               services.Configure<IdentityOptions>(options =>
               {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
               });
               
               services.AddHttpContextAccessor();
          }

          // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if(env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
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

                    /*endpoints.MapControllerRoute(
                         name: "CatalogMap",
                         pattern: "Catalog?{cameraId=id}",
                         new
                         {
                              controller = "Catalog",
                              action = "Edit",
                              cameraId = ""
                         });*/
                    
                    endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}");
               });
          }
     }
}