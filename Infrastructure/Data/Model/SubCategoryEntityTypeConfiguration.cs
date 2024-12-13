using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class SubCategoryEntityTypeConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__subCateg__3213E83F5EAFA942");

            builder.ToTable("subCategories");

            builder.HasIndex(e => e.SubCategoryName, "UQ__subCateg__A247483439038CD5").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.MajorCategoryId).HasColumnName("majorCategoryId");
            builder.Property(e => e.SubCategoryName)
                .HasMaxLength(50)
                .HasColumnName("subCategoryName");

            builder.HasOne(d => d.MajorCategory).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.MajorCategoryId)
                .HasConstraintName("FK__subCatego__major__403A8C7D");
        }
    }
}
