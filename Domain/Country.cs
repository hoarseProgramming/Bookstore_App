using System;
using System.Collections.Generic;

namespace Bookstore_App.Domain;

public partial class Country
{
    public int Id { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
