namespace Bookstore_App.Domain;

public partial class InventoryBalance
{
    public string Isbn13 { get; set; } = null!;

    public int StoreId { get; set; }

    public int UnitsInStock { get; set; }

    public virtual Book? Book { get; set; } = null!;

    public virtual Store? Store { get; set; } = null!;
}
