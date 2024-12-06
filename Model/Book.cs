using System;
using System.Collections.Generic;

namespace Bookstore_App;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int LanguageId { get; set; }

    public int PrimaryAudienceId { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public int PublishingHouseId { get; set; }

    public virtual ICollection<InventoryBalance> InventoryBalances { get; set; } = new List<InventoryBalance>();

    public virtual Language Language { get; set; } = null!;

    public virtual PrimaryAudience PrimaryAudience { get; set; } = null!;

    public virtual PublishingHouse PublishingHouse { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
