using Microsoft.EntityFrameworkCore;
using PenService.Domain.USAZCTAAggregates;
using PenService.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Infrastructure
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
