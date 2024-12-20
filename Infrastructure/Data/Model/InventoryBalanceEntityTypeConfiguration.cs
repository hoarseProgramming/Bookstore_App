using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class InventoryBalanceEntityTypeConfiguration : IEntityTypeConfiguration<InventoryBalance>
    {
        public void Configure(EntityTypeBuilder<InventoryBalance> builder)
        {
            builder.HasKey(e => new { e.Isbn13, e.StoreId }).HasName("PK__inventor__1A1DEF62A8C48F77");

            builder.ToTable("inventoryBalances");

            builder.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            builder.Property(e => e.StoreId).HasColumnName("storeId");
            builder.Property(e => e.UnitsInStock).HasColumnName("unitsInStock");

            builder.HasOne(d => d.Book).WithMany(p => p.InventoryBalances)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__inventory__ISBN1__5DCAEF64");

            builder.HasOne(d => d.Store).WithMany(p => p.InventoryBalances)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventory__store__5FB337D6");
        }
    }
}
