using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Infrastructure.Persistence.Configurations;

public class LockerConfigurations : IEntityTypeConfiguration<Locker>
{
    public void Configure(EntityTypeBuilder<Locker> builder)
    {
        // builder.HasOne<Room>()
        //     .WithMany();
    }
}