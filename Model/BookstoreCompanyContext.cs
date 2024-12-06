using Microsoft.EntityFrameworkCore;

namespace Bookstore_App;

public partial class BookstoreCompanyContext : DbContext
{
    public BookstoreCompanyContext()
    {
    }

    public BookstoreCompanyContext(DbContextOptions<BookstoreCompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookInfo> BookInfos { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<InventoryBalance> InventoryBalances { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MajorCategory> MajorCategories { get; set; }

    public virtual DbSet<PrimaryAudience> PrimaryAudiences { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Initial Catalog=bookstoreCompany;Integrated Security=True;Trust Server Certificate=True;Server SPN=localhost");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__addresse__3213E83F3496B822");

            entity.ToTable("addresses");

            entity.HasIndex(e => e.Line1, "UQ__addresse__1CB231F8581BC362").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.Line1)
                .HasMaxLength(50)
                .HasColumnName("line1");
            entity.Property(e => e.Line2)
                .HasMaxLength(50)
                .HasColumnName("line2");
            entity.Property(e => e.Line3)
                .HasMaxLength(50)
                .HasColumnName("line3");
            entity.Property(e => e.ZipOrPostcode)
                .HasMaxLength(40)
                .HasColumnName("zipOrPostcode");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__addresses__cityI__4D94879B");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__authors__3213E83FCA5294DB");

            entity.ToTable("authors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__books__3BF79E03FA9B6461");

            entity.ToTable("books");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.LanguageId).HasColumnName("languageId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PrimaryAudienceId).HasColumnName("primaryAudienceId");
            entity.Property(e => e.PublishingHouseId).HasColumnName("publishingHouseID");
            entity.Property(e => e.ReleaseDate).HasColumnName("releaseDate");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Language).WithMany(p => p.Books)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__languageI__59063A47");

            entity.HasOne(d => d.PrimaryAudience).WithMany(p => p.Books)
                .HasForeignKey(d => d.PrimaryAudienceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__primaryAu__59FA5E80");

            entity.HasOne(d => d.PublishingHouse).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__publishin__5AEE82B9");

            entity.HasMany(d => d.Authors).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__books_aut__autho__6477ECF3"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
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

            entity.HasMany(d => d.SubCategories).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksSubCategory",
                    r => r.HasOne<SubCategory>().WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__books_sub__subCa__693CA210"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
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
        });

        modelBuilder.Entity<BookInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("BookInfo");

            entity.Property(e => e.AuthorS).HasColumnName("Author(s)");
            entity.Property(e => e.InventoryStatus)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("Inventory status");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.MajorCategory)
                .HasMaxLength(50)
                .HasColumnName("Major category");
            entity.Property(e => e.PriceSek)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("Price (SEK)");
            entity.Property(e => e.PrimaryAudience)
                .HasMaxLength(50)
                .HasColumnName("Primary audience");
            entity.Property(e => e.PublishingHouse)
                .HasMaxLength(50)
                .HasColumnName("Publishing house");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release date");
            entity.Property(e => e.SubcategoryIes)
                .HasMaxLength(4000)
                .HasColumnName("Subcategory(ies)");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UnitsInStock).HasColumnName("Units in stock");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cities__3213E83F0C9F8365");

            entity.ToTable("cities");

            entity.HasIndex(e => e.CityName, "UQ__cities__AEE8ADD10BFC0865").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .HasColumnName("cityName");
            entity.Property(e => e.CountryId).HasColumnName("countryID");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__cities__countryI__49C3F6B7");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__countrie__3213E83FDFBE60BA");

            entity.ToTable("countries");

            entity.HasIndex(e => e.CountryName, "UQ__countrie__0756ED8C1A2A1757").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("countryName");
        });

        modelBuilder.Entity<InventoryBalance>(entity =>
        {
            entity.HasKey(e => new { e.Isbn13, e.StoreId }).HasName("PK__inventor__1A1DEF62A8C48F77");

            entity.ToTable("inventoryBalances");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.StoreId).HasColumnName("storeId");
            entity.Property(e => e.UnitsInStock).HasColumnName("unitsInStock");

            entity.HasOne(d => d.Book).WithMany(p => p.InventoryBalances)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventory__ISBN1__5DCAEF64");

            entity.HasOne(d => d.Store).WithMany(p => p.InventoryBalances)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventory__store__5FB337D6");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__language__3213E83F035AB49B");

            entity.ToTable("languages");

            entity.HasIndex(e => e.Language1, "UQ__language__EFADA5D9E7EABCF6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Language1)
                .HasMaxLength(50)
                .HasColumnName("language");
        });

        modelBuilder.Entity<MajorCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__majorCat__3213E83F67F27A7E");

            entity.ToTable("majorCategories");

            entity.HasIndex(e => e.MajorCategoryName, "UQ__majorCat__FAD8B212DAFAE6EE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MajorCategoryName)
                .HasMaxLength(50)
                .HasColumnName("majorCategoryName");
        });

        modelBuilder.Entity<PrimaryAudience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__primaryA__3213E83FD7F89D61");

            entity.ToTable("primaryAudiences");

            entity.HasIndex(e => e.PrimaryAudienceName, "UQ__primaryA__19D5AED5296E9FF0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PrimaryAudienceName)
                .HasMaxLength(50)
                .HasColumnName("primaryAudienceName");
        });

        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__publishi__3213E83FCA87BC5A");

            entity.ToTable("publishingHouses");

            entity.HasIndex(e => e.PublishingHouseName, "UQ__publishi__4DF6717797A2B710").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PublishingHouseName)
                .HasMaxLength(50)
                .HasColumnName("publishingHouseName");

            entity.HasOne(d => d.Address).WithMany(p => p.PublishingHouses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__publishin__addre__5165187F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stores__3213E83FDC8E05D5");

            entity.ToTable("stores");

            entity.HasIndex(e => e.StoreName, "UQ__stores__0E3E451DE3B83ACC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddresId).HasColumnName("addresID");
            entity.Property(e => e.StoreName)
                .HasMaxLength(50)
                .HasColumnName("storeName");

            entity.HasOne(d => d.Addres).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AddresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__stores__addresID__5535A963");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subCateg__3213E83F5EAFA942");

            entity.ToTable("subCategories");

            entity.HasIndex(e => e.SubCategoryName, "UQ__subCateg__A247483439038CD5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MajorCategoryId).HasColumnName("majorCategoryId");
            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(50)
                .HasColumnName("subCategoryName");

            entity.HasOne(d => d.MajorCategory).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.MajorCategoryId)
                .HasConstraintName("FK__subCatego__major__403A8C7D");
        });

        modelBuilder.Entity<TitlesPerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlesPerAuthor");

            entity.Property(e => e.InventoryValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Inventory Value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
