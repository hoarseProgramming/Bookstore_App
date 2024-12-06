using System;
using System.Collections.Generic;

namespace Bookstore_App;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
