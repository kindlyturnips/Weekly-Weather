using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Weekly_Weather.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        //Configuration for Connection String
        private readonly IConfiguration _configuration;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Forecast> Forecast { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the primary key for the 'Locations' entity.

            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            /*modelBuilder.Entity<Location>()                
                   .HasOne(f => f.Forecast)
                   .WithOne(l => l.Location)
                   .HasForeignKey<Forecast>(l => l.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);*/



            base.OnModelCreating(modelBuilder);


        }

    }
}
