using Eventify.Data.Configurations;
using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<EventPhoto> EventPhotos { get; set; }
        public DbSet<VenuePhoto> VenuePhotos { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Organizier> Organiziers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(EventConfiguration).Assembly);


        }
    }
}
