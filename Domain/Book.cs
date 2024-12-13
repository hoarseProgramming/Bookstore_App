namespace Bookstore_App.Domain;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int LanguageId { get; set; }

    public int PrimaryAudienceId { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public int PublishingHouseId { get; set; }

    public virtual ICollection<InventoryBalance> InventoryBalances { get; set; } = new List<InventoryBalance>();

    public virtual Language Language { get; set; } = null!;

    public virtual PrimaryAudience PrimaryAudience { get; set; } = null!;

    public virtual PublishingHouse PublishingHouse { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    //TODO: Exclude from migration?
    public string ConcatenatedLastNames
    {
        get
        {
            if (Authors.Count > 2)
            {
                return Authors.FirstOrDefault().LastName;
            }
            else
            {
                var listOfAuthors = new List<Author>();
                foreach (var author in Authors)
                {
                    listOfAuthors.Add(author);
                }

                string authors = string.Empty;

                for (int i = 0; i < listOfAuthors.Count; i++)
                {
                    if (i == 0)
                    {
                        authors += listOfAuthors[i].LastName;
                    }
                    else
                    {
                        authors += $", {listOfAuthors[i].LastName}";
                    }
                }
                return authors;
            }
        }
    }

}
