using competra.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Activity = competra.wwwapi.Models.Activity;

namespace competra.wwwapi.Data
{
    public class DataContext : DbContext
    {
        private string _connectionString;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Group-Activity relationship
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Activities)
                .WithOne()
                .HasForeignKey(a => a.GroupId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify delete behavior

            // Group-UserGroup relationship
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId }); // Composite key

            modelBuilder.Entity<UserGroup>()
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(ug => ug.GroupId);

            modelBuilder.Entity<UserGroup>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ug => ug.UserId);

            // User-UserGroup relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserGroups)
                .WithOne()
                .HasForeignKey(ug => ug.UserId);

            // User-UserActivity relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserActivities)
                .WithOne()
                .HasForeignKey(ua => ua.UserId);

            // Activity-UserActivity relationship
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.UserActivities)
                .WithOne()
                .HasForeignKey(ua => ua.ActivityId);

            // Activity-Match relationship
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Matches)
                .WithOne()
                .HasForeignKey(m => m.ActivityId);


        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Match> Matches { get; set; }

    }
}
