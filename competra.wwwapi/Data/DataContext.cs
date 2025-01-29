using competra.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Activity = competra.wwwapi.Models.Activity;

namespace competra.wwwapi.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.development.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); // Log SQL queries
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seeder seeder = new Seeder();
            modelBuilder.Entity<Activity>().HasData(seeder.ActivityList);
            modelBuilder.Entity<User>().HasData(seeder.Users);
            modelBuilder.Entity<Match>().HasData(seeder.MatchList);
            modelBuilder.Entity<UserGroup>().HasData(seeder.UserGroupList);
            modelBuilder.Entity<UserActivity>().HasData(seeder.UserActivityList);
            modelBuilder.Entity<Group>().HasData(seeder.GroupList);







            modelBuilder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Group)
                .WithMany(g => g.Activities)
                .HasForeignKey(a => a.GroupId);

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.Activity)
                .WithMany(a => a.UserActivities)
                .HasForeignKey(ua => ua.ActivityId);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Activity)
                .WithMany(a => a.Matches)
                .HasForeignKey(m => m.ActivityId)
            .OnDelete(DeleteBehavior.Restrict);



        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Match> Matches { get; set; }

    }
}
