using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class MajorCategoryEntityTypeConfiguration : IEntityTypeConfiguration<MajorCategory>
    {
        public void Configure(EntityTypeBuilder<MajorCategory> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__majorCat__3213E83F67F27A7E");

            builder.ToTable("majorCategories");

            builder.HasIndex(e => e.MajorCategoryName, "UQ__majorCat__FAD8B212DAFAE6EE").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.MajorCategoryName)
                .HasMaxLength(50)
                .HasColumnName("majorCategoryName");
        }
    }
}
