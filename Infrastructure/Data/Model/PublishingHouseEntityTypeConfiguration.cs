using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class PublishingHouseEntityTypeConfiguration : IEntityTypeConfiguration<PublishingHouse>
    {
        public void Configure(EntityTypeBuilder<PublishingHouse> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__publishi__3213E83FCA87BC5A");

            builder.ToTable("publishingHouses");

            builder.HasIndex(e => e.PublishingHouseName, "UQ__publishi__4DF6717797A2B710").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AddressId).HasColumnName("addressId");
            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            builder.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            builder.Property(e => e.PublishingHouseName)
                .HasMaxLength(50)
                .HasColumnName("publishingHouseName");

            builder.HasOne(d => d.Address).WithMany(p => p.PublishingHouses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__publishin__addre__5165187F");
        }
    }
}
