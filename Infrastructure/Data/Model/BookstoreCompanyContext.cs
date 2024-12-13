using Bookstore_App.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bookstore_App.Infrastructure.Data.Model;

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

    //TODO: REMOVE
    //public virtual DbSet<BookInfo> BookInfos { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<InventoryBalance> InventoryBalances { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MajorCategory> MajorCategories { get; set; }

    public virtual DbSet<PrimaryAudience> PrimaryAudiences { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    //TODO: REMOVE
    //public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<BookstoreCompanyContext>().Build();

        var connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = config["ServerName"],
            InitialCatalog = config["DatabaseName"],
            TrustServerCertificate = true,
            IntegratedSecurity = true
        }.ToString();

        optionsBuilder
            .UseSqlServer(connectionString)
#if DEBUG
            .LogTo(
            message => Debug.WriteLine(message),
            LogLevel.Information
            );
#else       
            ;
#endif

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Remove
        //modelBuilder.Entity<TitlesPerAuthor>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToView("TitlesPerAuthor");

        //    entity.Property(e => e.InventoryValue)
        //        .HasColumnType("decimal(38, 2)")
        //        .HasColumnName("Inventory Value");
        //});

        //TODO: Remove
        //modelBuilder.Entity<BookInfo>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToView("BookInfo");

        //    entity.Property(e => e.AuthorS).HasColumnName("Author(s)");
        //    entity.Property(e => e.InventoryStatus)
        //        .HasMaxLength(16)
        //        .IsUnicode(false)
        //        .HasColumnName("Inventory status");
        //    entity.Property(e => e.Isbn13)
        //        .HasMaxLength(13)
        //        .HasColumnName("ISBN13");
        //    entity.Property(e => e.Language).HasMaxLength(50);
        //    entity.Property(e => e.MajorCategory)
        //        .HasMaxLength(50)
        //        .HasColumnName("Major category");
        //    entity.Property(e => e.PriceSek)
        //        .HasColumnType("decimal(9, 2)")
        //        .HasColumnName("Price (SEK)");
        //    entity.Property(e => e.PrimaryAudience)
        //        .HasMaxLength(50)
        //        .HasColumnName("Primary audience");
        //    entity.Property(e => e.PublishingHouse)
        //        .HasMaxLength(50)
        //        .HasColumnName("Publishing house");
        //    entity.Property(e => e.ReleaseDate).HasColumnName("Release date");
        //    entity.Property(e => e.SubcategoryIes)
        //        .HasMaxLength(4000)
        //        .HasColumnName("Subcategory(ies)");
        //    entity.Property(e => e.Title).HasMaxLength(200);
        //    entity.Property(e => e.UnitsInStock).HasColumnName("Units in stock");
        //});

        new AddressEntityTypeConfiguration().Configure(modelBuilder.Entity<Address>());
        new AuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Author>());
        new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
        new CityEntityTypeConfiguration().Configure(modelBuilder.Entity<City>());
        new CountryEntityTypeConfiguration().Configure(modelBuilder.Entity<Country>());
        new InventoryBalanceEntityTypeConfiguration().Configure(modelBuilder.Entity<InventoryBalance>());
        new LanguageEntityTypeConfiguration().Configure(modelBuilder.Entity<Language>());
        new MajorCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<MajorCategory>());
        new PrimaryAudienceEntityTypeConfiguration().Configure(modelBuilder.Entity<PrimaryAudience>());
        new PublishingHouseEntityTypeConfiguration().Configure(modelBuilder.Entity<PublishingHouse>());
        new StoreEntityTypeConfiguration().Configure(modelBuilder.Entity<Store>());
        new SubCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<SubCategory>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
