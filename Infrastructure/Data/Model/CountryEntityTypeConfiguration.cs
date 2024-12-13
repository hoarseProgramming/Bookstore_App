using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__countrie__3213E83FDFBE60BA");

            builder.ToTable("countries");

            builder.HasIndex(e => e.CountryName, "UQ__countrie__0756ED8C1A2A1757").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("countryName");
        }
    }
}
