using Bookstore_App.Domain;
using System.Collections.ObjectModel;

namespace Bookstore_App.Presentation.ViewModel;


public class BookViewModel : ViewModelBase
{
    private readonly Book model;

    public BookViewModel(Book model)
    {
        this.model = model;
        this.Authors = new ObservableCollection<Author>(model.Authors);
        this.SubCategories = new ObservableCollection<SubCategory>(model.SubCategories);
        this.InventoryBalances = new ObservableCollection<InventoryBalance>(model.InventoryBalances);
        this.ConcatenatedLastNames = GenerateLastNames();
    }
    public string Isbn13
    {
        get => model.Isbn13;
        set
        {
            model.Isbn13 = value;
            RaisePropertyChanged();
        }
    }

    public string Title
    {
        get => model.Title;
        set
        {
            model.Title = value;
            RaisePropertyChanged();
        }
    }


    public int LanguageId
    {
        get => model.LanguageId;
        set
        {
            model.LanguageId = value;
            RaisePropertyChanged();
        }
    }

    public int PrimaryAudienceId
    {
        get => model.PrimaryAudienceId;
        set
        {
            model.PrimaryAudienceId = value;
            RaisePropertyChanged();
        }
    }

    public decimal Price
    {
        get => model.Price;
        set
        {
            model.Price = value;
            RaisePropertyChanged();
        }
    }

    public DateOnly ReleaseDate
    {
        get => model.ReleaseDate;
        set
        {
            model.ReleaseDate = value;
            RaisePropertyChanged();
        }
    }

    public int PublishingHouseId
    {
        get => model.PublishingHouseId;
        set
        {
            model.PublishingHouseId = value;
            RaisePropertyChanged();
        }
    }

    public ObservableCollection<InventoryBalance> InventoryBalances { get; }

    public Language Language
    {
        get => model.Language;
        set
        {
            model.Language = value;
            RaisePropertyChanged();
        }
    }

    public PrimaryAudience PrimaryAudience
    {
        get => model.PrimaryAudience;
        set
        {
            model.PrimaryAudience = value;
            RaisePropertyChanged();
        }
    }

    public PublishingHouse PublishingHouse
    {
        get => model.PublishingHouse;
        set
        {
            model.PublishingHouse = value;
            RaisePropertyChanged();
        }
    }

    public ObservableCollection<Author> Authors { get; }

    public ObservableCollection<SubCategory> SubCategories { get; }

    public bool IsAdded = false;

    public bool IsEdited = false;

    private string _concatenatedLastNames;
    public string ConcatenatedLastNames
    {
        get => _concatenatedLastNames;
        set
        {
            _concatenatedLastNames = value;
            RaisePropertyChanged();
        }
    }
    private string GenerateLastNames()
    {
        if (Authors.Count == 1)
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
    public void UpdateLastNames()
    {
        ConcatenatedLastNames = GenerateLastNames();
    }

    public string ConcatenatedSubCategories
    {
        get
        {
            if (SubCategories.Count > 2)
            {
                return SubCategories.FirstOrDefault().SubCategoryName;
            }
            else
            {
                var listOfSubCategories = new List<SubCategory>();
                foreach (var subCategory in SubCategories)
                {
                    listOfSubCategories.Add(subCategory);
                }

                string concatenatedSubCategories = string.Empty;

                for (int i = 0; i < SubCategories.Count; i++)
                {
                    if (i == 0)
                    {
                        concatenatedSubCategories += listOfSubCategories[i].SubCategoryName;
                    }
                    else
                    {
                        concatenatedSubCategories += $", {listOfSubCategories[i].SubCategoryName}";
                    }
                }
                return concatenatedSubCategories;
            }
        }
    }

    public Book GetModel()
    {
        return model;
    }

}



