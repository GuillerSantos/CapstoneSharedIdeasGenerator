using Microsoft.EntityFrameworkCore;
using CapstoneIdeaGenerator.Server.Models;

namespace CapstoneIdeaGenerator.Server.DbContext
{
    public class WebAppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Capstones> Capstones { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<ActivityLogs> ActivityLogs { get; set; }

        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Admins>()
                .HasKey(a => a.AdminId);

            builder.Entity<ActivityLogs>()
                .HasKey(al => al.ActivityLogsId);

            builder.Entity<ActivityLogs>()
                .HasOne(al => al.Admins)
                .WithMany()
                .HasForeignKey(al => al.AdminId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Capstones>()
                .HasKey(c => c.CapstoneId);

            builder.Entity<Capstones>()
                .HasMany(c => c.Ratings)
                .WithOne(r => r.Capstones)
                .HasForeignKey(r => r.CapstoneId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Ratings>()
                .HasKey(r => r.RatingId);

            builder.Entity<Ratings>()
                .HasOne(r => r.Capstones)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.CapstoneId);
        }
    }
}
