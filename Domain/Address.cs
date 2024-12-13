namespace Bookstore_App.Domain;

public partial class Address
{
    public int Id { get; set; }

    public string Line1 { get; set; } = null!;

    public string? Line2 { get; set; }

    public string? Line3 { get; set; }

    public int CityId { get; set; }

    public string ZipOrPostcode { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual ICollection<PublishingHouse> PublishingHouses { get; set; } = new List<PublishingHouse>();

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
