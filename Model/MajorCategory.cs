using System;
using System.Collections.Generic;

namespace Bookstore_App;

public partial class MajorCategory
{
    public int Id { get; set; }

    public string MajorCategoryName { get; set; } = null!;

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
