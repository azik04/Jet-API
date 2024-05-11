using Jet_API1.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jet_API1.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.City)
                    .WithMany(x => x.Places)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Region)
                    .WithMany(x => x.Hotel)
                    .HasForeignKey(x => x.RegionId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.City)
                    .WithMany(x => x.Regions)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Hotels).WithMany(x => x.Order).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Regions).WithMany(x => x.Order).HasForeignKey(x => x.RegionId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x=>x.Flight).WithMany(x =>x.Orders).HasForeignKey(x=>x.FlightId).OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Vehicle).WithMany(x => x.Flights).HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        public DbSet<City> City { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public  DbSet<Flight> Flights { get; set; }
    }
}