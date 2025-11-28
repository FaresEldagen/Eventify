using Eventify.Data.Configurations;
using Eventify.Models.Entities;
using Eventify.Seeding_Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApplication2.Controllers;

namespace Eventify.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<EventPhoto> EventPhotos { get; set; }
        public DbSet<VenuePhoto> VenuePhotos { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly);
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new EventPhotoConfiguration());
            builder.ApplyConfiguration(new OrganizerConfiguration());
            builder.ApplyConfiguration(new OwnerConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new VenueConfiguration());
            builder.ApplyConfiguration(new VenuePhotoConfiguration());

            // Seeding Data
            builder.ApplyConfiguration(new RoleSeedData());
            builder.ApplyConfiguration(new OrganizerSeedData());
            //builder.ApplyConfiguration(new OwnerSeedData());
            //builder.ApplyConfiguration(new UserRoleSeedData());

            //builder.ApplyConfiguration(new VenuesSeedData());
            //builder.ApplyConfiguration(new VenuePhotoSeedData());
            //builder.ApplyConfiguration(new EventsSeedData());
            //builder.ApplyConfiguration(new EventPhotoSeedData());
            //builder.ApplyConfiguration(new PaymentsSeedData());
        }
    }
}
