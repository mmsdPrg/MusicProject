using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicProject.Areas.Identity.Data;
using MusicProject.Models;

namespace MusicProject.Data
{
    public class DB_Music : IdentityDbContext<ApplicationUser>
    {
        public DB_Music(DbContextOptions<DB_Music> options)
            : base(options)
        {
        }
        public DbSet<Music> Music { get; set; }
        public DbSet<MusicImage> MusicImage { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
