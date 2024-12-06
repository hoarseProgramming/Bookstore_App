using System;
using System.Collections.Generic;

namespace Bookstore_App;

public partial class BookInfo
{
    public string Isbn13 { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? AuthorS { get; set; }

    public string Language { get; set; } = null!;

    public string? SubcategoryIes { get; set; }

    public string MajorCategory { get; set; } = null!;

    public string PrimaryAudience { get; set; } = null!;

    public decimal PriceSek { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public string PublishingHouse { get; set; } = null!;

    public int? UnitsInStock { get; set; }

    public string InventoryStatus { get; set; } = null!;
}
