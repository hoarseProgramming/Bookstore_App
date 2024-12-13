using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class PrimaryAudienceEntityTypeConfiguration : IEntityTypeConfiguration<PrimaryAudience>
    {
        public void Configure(EntityTypeBuilder<PrimaryAudience> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__primaryA__3213E83FD7F89D61");

            builder.ToTable("primaryAudiences");

            builder.HasIndex(e => e.PrimaryAudienceName, "UQ__primaryA__19D5AED5296E9FF0").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.PrimaryAudienceName)
                .HasMaxLength(50)
                .HasColumnName("primaryAudienceName");
        }
    }
}
