using System;
using System.Collections.Generic;

namespace Bookstore_App.Domain;

public partial class City
{
    public int Id { get; set; }

    public string? CityName { get; set; }

    public int? CountryId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Country? Country { get; set; }
}
