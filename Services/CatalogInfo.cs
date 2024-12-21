using Bookstore_App.Domain;
using Bookstore_App.Presentation.ViewModel;
using System.Collections.ObjectModel;

namespace Bookstore_App.Services;


public class CatalogInfo
{
    public CatalogInfo
        (ObservableCollection<BookViewModel>? books, ObservableCollection<Author>? authors,
        ObservableCollection<SubCategory>? subCategories, ObservableCollection<Language>? languages,
        ObservableCollection<PrimaryAudience>? primaryAudiences, ObservableCollection<PublishingHouse>? publishingHouses)
    {
        Books = books;
        Authors = authors;
        SubCategories = subCategories;
        Languages = languages;
        PrimaryAudiences = primaryAudiences;
        PublishingHouses = publishingHouses;
    }

    public ObservableCollection<BookViewModel>? Books { get; set; }
    public ObservableCollection<Author>? Authors { get; set; }
    public ObservableCollection<SubCategory>? SubCategories { get; set; }
    public ObservableCollection<Language>? Languages { get; set; }
    public ObservableCollection<PrimaryAudience>? PrimaryAudiences { get; set; }
    public ObservableCollection<PublishingHouse>? PublishingHouses { get; set; }
}


