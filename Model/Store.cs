using System;
using System.Collections.Generic;

namespace Bookstore_App;

public partial class Store
{
    public int Id { get; set; }

    public string StoreName { get; set; } = null!;

    public int AddresId { get; set; }

    public virtual Address Addres { get; set; } = null!;

    public virtual ICollection<InventoryBalance> InventoryBalances { get; set; } = new List<InventoryBalance>();
}
