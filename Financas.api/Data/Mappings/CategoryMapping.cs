
using Financas.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financas.api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>

    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Caterories");
            builder.HasKey(key => key.Id);
            builder.Property(x => x.Title)
                .IsRequired(true)
                .HasMaxLength(80);

            builder.Property(x => x.Description).IsRequired(false)
                .HasMaxLength(255);
            builder.Property(x => x.UserId).IsRequired(true)
                .HasMaxLength(160);
        }
    }
}