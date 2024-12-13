using System;
using System.Collections.Generic;

namespace Bookstore_App.Domain;

public partial class PrimaryAudience
{
    public int Id { get; set; }

    public string PrimaryAudienceName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
