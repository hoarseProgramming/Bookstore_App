using Bookstore_App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Infrastructure.Data.Model;

public partial class BookstoreCompanyContext
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__authors__3213E83FCA5294DB");

            builder.ToTable("authors");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            builder.Property(e => e.FirstName).HasColumnName("firstName");
            builder.Property(e => e.LastName).HasColumnName("lastName");
        }
    }
}
