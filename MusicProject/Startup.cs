using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicProject.Areas.Identity.Data;
using MusicProject.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject
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
            services.Configure<CookiePolicyOptions>(z =>
            {
                z.CheckConsentNeeded = x => false;
                z.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddSession();
            services.AddControllersWithViews();
            var map = new MapperConfiguration(z => z.AddProfile(typeof(ProfileMapper)));
            IMapper mapper = map.CreateMapper();
            services.AddSingleton(mapper);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<ApplicationUser>userManager,RoleManager<IdentityRole>roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
         
            createRole(userManager, roleManager).Wait();

        }

        private async Task createRole(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<string> Roles = new List<string>() { "Admin", "User" };

            foreach (var item in Roles)
            {
                if(await roleManager.RoleExistsAsync(item) == false)
                {
                    IdentityRole identityRole = new IdentityRole(item);
                    await roleManager.CreateAsync(identityRole);
                }
            }
            ApplicationUser user = await userManager.FindByNameAsync("Admin");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = "Admin",
                    Name = "محمد صالح ",
                    Family = "مسعود",
                    UserName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumber = "09391227164",
                    PhoneNumberConfirmed = true,
                };
                await userManager.CreateAsync(user,"1380420mM");
            }
            if(await userManager.IsInRoleAsync(user, "Admin") == false)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }


        }
    }
}
