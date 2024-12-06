using System;
using System.Collections.Generic;

namespace Bookstore_App;

public partial class PublishingHouse
{
    public int Id { get; set; }

    public string PublishingHouseName { get; set; } = null!;

    public int AddressId { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
