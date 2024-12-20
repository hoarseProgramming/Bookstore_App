using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Isbn13).HasName("PK__books__3BF79E03FA9B6461");

            builder.ToTable("books");

            builder.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            builder.Property(e => e.LanguageId).HasColumnName("languageId");
            builder.Property(e => e.Price)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("price");
            builder.Property(e => e.PrimaryAudienceId).HasColumnName("primaryAudienceId");
            builder.Property(e => e.PublishingHouseId).HasColumnName("publishingHouseID");
            builder.Property(e => e.ReleaseDate).HasColumnName("releaseDate");
            builder.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            builder.HasOne(d => d.Language).WithMany(p => p.Books)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__languageI__59063A47");

            builder.HasOne(d => d.PrimaryAudience).WithMany(p => p.Books)
                .HasForeignKey(d => d.PrimaryAudienceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__primaryAu__59FA5E80");

            builder.HasOne(d => d.PublishingHouse).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__publishin__5AEE82B9");

            builder.HasMany(d => d.Authors).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__books_aut__autho__6477ECF3"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__books_aut__ISBN1__628FA481"),
                    j =>
                    {
                        j.HasKey("Isbn13", "AuthorId").HasName("PK__books_au__B315ED185BCD0EE2");
                        j.ToTable("books_authors");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .HasColumnName("ISBN13");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("authorId");
                    });

            builder.HasMany(d => d.SubCategories).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksSubCategory",
                    r => r.HasOne<SubCategory>().WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__books_sub__subCa__693CA210"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__books_sub__ISBN1__6754599E"),
                    j =>
                    {
                        j.HasKey("Isbn13", "SubCategoryId").HasName("PK__books_su__B47598452484FFAD");
                        j.ToTable("books_subCategories");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .HasColumnName("ISBN13");
                        j.IndexerProperty<int>("SubCategoryId").HasColumnName("subCategoryId");
                    });
        }
    }
}
