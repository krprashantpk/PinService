using Microsoft.EntityFrameworkCore;
using PinService.Domain.USAZCTAAggregates;
using PinService.Infrastructure.EntityConfigurations;

namespace PinService.Infrastructure
{
    public class PinServiceContext : DbContext
    {
        public PinServiceContext(DbContextOptions<PinServiceContext> options) : base(options) { }

        public DbSet<USAZcta> USAZctas { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<USAZcta>(new USAZCTAEntityTypeConfiguration());
        }
    }
}
