using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__addresse__3213E83F3496B822");

            builder.ToTable("addresses");

            builder.HasIndex(e => e.Line1, "UQ__addresse__1CB231F8581BC362").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CityId).HasColumnName("cityId");
            builder.Property(e => e.Line1)
                .HasMaxLength(50)
                .HasColumnName("line1");
            builder.Property(e => e.Line2)
                .HasMaxLength(50)
                .HasColumnName("line2");
            builder.Property(e => e.Line3)
                .HasMaxLength(50)
                .HasColumnName("line3");
            builder.Property(e => e.ZipOrPostcode)
                .HasMaxLength(40)
                .HasColumnName("zipOrPostcode");

            builder.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__addresses__cityI__4D94879B");
        }
    }
}
