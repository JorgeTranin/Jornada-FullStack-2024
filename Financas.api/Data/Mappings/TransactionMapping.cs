using Financas.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financas.api.Data.Mappings
{

    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.CreateAt)
                .IsRequired();

            builder.Property(t => t.PaidOrReceivedAt)
                .HasMaxLength(160);

            builder.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.Amount)
                .IsRequired();

            builder.Property(t => t.CategoryId)
                .IsRequired();

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(160);

            // Configure the navigation property and the relationship
            builder.HasOne(t => t.Category)
                .WithMany() // Adjust based on your actual relationship, e.g., .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
