using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Infrastructure.EntityConfigurations
{
    public class USAZCTAEntityTypeConfiguration : IEntityTypeConfiguration<USAZCTA>
    {
        public void Configure(EntityTypeBuilder<USAZCTA> builder)
        {
            builder.HasKey(x => x.Zcta);

            builder.Property(x => x.Zcta)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x=>x.StateCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.County)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(x => x.Latitude)
                .IsRequired();

            builder.Property(x => x.Longitude)
                .IsRequired();

        }
    }
}
