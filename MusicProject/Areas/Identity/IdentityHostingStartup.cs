﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicProject.Areas.Identity.Data;
using MusicProject.Data;

[assembly: HostingStartup(typeof(MusicProject.Areas.Identity.IdentityHostingStartup))]
namespace MusicProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DB_Music>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DB_MusicConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DB_Music>();


                services.Configure<IdentityOptions>(z =>
                {
                    z.Password.RequiredLength = 8;
                    z.Password.RequireNonAlphanumeric = false;
                    z.Password.RequireLowercase = false;
                    z.Password.RequireUppercase = false;
                    z.Password.RequiredUniqueChars = 0;
                    z.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                    z.Lockout.MaxFailedAccessAttempts = 3;

                });
            });

            
        }
    }
}