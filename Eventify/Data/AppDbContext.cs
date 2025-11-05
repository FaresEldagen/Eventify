using Eventify.Data.Configurations;
using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

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
