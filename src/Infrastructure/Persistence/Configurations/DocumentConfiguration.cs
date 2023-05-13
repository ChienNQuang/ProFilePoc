using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
  public void Configure(EntityTypeBuilder<Document> builder)
  {
      // builder.Property(x => x.PhysicalFolderId)
      //     .IsRequired();
  }
}