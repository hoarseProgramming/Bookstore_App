using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__language__3213E83F035AB49B");

            builder.ToTable("languages");

            builder.HasIndex(e => e.Language1, "UQ__language__EFADA5D9E7EABCF6").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Language1)
                .HasMaxLength(50)
                .HasColumnName("language");
        }
    }
}
