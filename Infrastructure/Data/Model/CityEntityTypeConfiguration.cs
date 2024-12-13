using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__cities__3213E83F0C9F8365");

            builder.ToTable("cities");

            builder.HasIndex(e => e.CityName, "UQ__cities__AEE8ADD10BFC0865").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CityName)
                .HasMaxLength(50)
                .HasColumnName("cityName");
            builder.Property(e => e.CountryId).HasColumnName("countryID");

            builder.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__cities__countryI__49C3F6B7");
        }
    }
}
