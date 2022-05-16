using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinService.Domain.USAZCTAAggregates;

namespace PinService.Infrastructure.EntityConfigurations
{
    public class USAZCTAEntityTypeConfiguration : IEntityTypeConfiguration<USAZcta>
    {
        public void Configure(EntityTypeBuilder<USAZcta> builder)
        {
            builder.HasKey(x => x.Zcta);

            builder.Property(x => x.Zcta)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.StateCode)
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
