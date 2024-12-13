using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class StoreEntityTypeConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__stores__3213E83FDC8E05D5");

            builder.ToTable("stores");

            builder.HasIndex(e => e.StoreName, "UQ__stores__0E3E451DE3B83ACC").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AddresId).HasColumnName("addresID");
            builder.Property(e => e.StoreName)
                .HasMaxLength(50)
                .HasColumnName("storeName");

            builder.HasOne(d => d.Addres).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AddresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__stores__addresID__5535A963");
        }
    }
}
