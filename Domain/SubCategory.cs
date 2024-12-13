using System;
using System.Collections.Generic;

namespace Bookstore_App.Domain;

public partial class SubCategory
{
    public int Id { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public int? MajorCategoryId { get; set; }

    public virtual MajorCategory? MajorCategory { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
