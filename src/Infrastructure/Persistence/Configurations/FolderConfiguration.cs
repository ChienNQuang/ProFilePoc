using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Infrastructure.Persistence.Configurations;

public class PhysicalFolderConfiguration : IEntityTypeConfiguration<PhysicalFolder>
{
    public void Configure(EntityTypeBuilder<PhysicalFolder> builder)
    {
        // builder.HasOne(f => f.Locker)
        //     .WithMany()
        //     .HasForeignKey(f => f.LockerId);
    }
}