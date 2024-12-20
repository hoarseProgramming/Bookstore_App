using Bookstore_App.Domain;
using Bookstore_App.Infrastructure.Data.Model;
using Bookstore_App.Presentation.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Bookstore_App.Services
{
    internal static class DataManager
    {
        public static async Task<List<Store>> GetStoresAsync()
        {
            //TODO: Track start and end of operation, add debuginfo, FIX ERROR HANDLING

            var stores = new List<Store>();

            await Task.Run(() =>
            {
                using var db = new BookstoreCompanyContext();
                try
                {
                    stores = db.Stores.ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Failed to get stores");
                }
            });

            return stores;
        }

        public static async Task<List<InventoryBalance>> GetInventoryBalancesAsync(Store store)
        {
            using var db = new BookstoreCompanyContext();

            var inventoryBalances = await db.InventoryBalances
                .Include(i => i.Store)
                .Include(i => i.Book)
                .ThenInclude(b => b.Authors)
                .Where(i => i.Store.Equals(store))
                .ToListAsync();

            return inventoryBalances;
        }

        public static async Task AddInventoryBalance(Store store, Book book, int numberOfUnits)
        {
            using var db = new BookstoreCompanyContext();

            var inventoryBalance = new InventoryBalance()
            {
                Book = book,
                Store = store,
                UnitsInStock = numberOfUnits
            };

            db.InventoryBalances.Update(inventoryBalance);

            await db.SaveChangesAsync();
        }

        public static async Task<List<Book>> GetBooksAsync()
        {
            using var db = new BookstoreCompanyContext();

            var books = await db.Books.Include(b => b.Authors).ToListAsync();

            return books;
        }

        public static async Task UpdateInventoryBalancesAsync(List<InventoryBalance> updatedInventoryBalances, int id)
        {
            using var db = new BookstoreCompanyContext();

            var inventoryBalances = await db.InventoryBalances.Where(i => i.StoreId == id).ToListAsync();

            foreach (var inventoryBalance in inventoryBalances)
            {
                if (!(updatedInventoryBalances.Any(i => i.Isbn13.Equals(inventoryBalance.Isbn13) && i.StoreId.Equals(inventoryBalance.StoreId))))
                {
                    db.InventoryBalances.Remove(inventoryBalance);
                }
            }

            foreach (var inventoryBalance in updatedInventoryBalances)
            {
                if (!(inventoryBalances.Any(i => i.Isbn13.Equals(inventoryBalance.Isbn13) && i.StoreId.Equals(inventoryBalance.StoreId))))
                {
                    inventoryBalance.Store = null;
                    inventoryBalance.Book = null;
                    inventoryBalances.Add(inventoryBalance);
                    db.InventoryBalances.Add(inventoryBalance);
                }
            }

            for (int i = 0; i < updatedInventoryBalances.Count; i++)
            {
                if (inventoryBalances.Any(ib => ib.Isbn13.Equals(updatedInventoryBalances[i].Isbn13) && ib.StoreId.Equals(updatedInventoryBalances[i].StoreId)))
                {
                    inventoryBalances[i].UnitsInStock = updatedInventoryBalances[i].UnitsInStock;
                }
            }

            await db.SaveChangesAsync();

        }

        public static async Task<CatalogInfo> GetCatalogInfoAsync()
        {
            using var db = new BookstoreCompanyContext();

            var books = new ObservableCollection<CatalogBook>
                (await db.Books
                .Include(b => b.SubCategories)
                .ThenInclude(s => s.MajorCategory)
                .Include(b => b.Authors)
                .Include(b => b.Language)
                .Include(b => b.PrimaryAudience)
                .Include(b => b.PublishingHouse)
                .ThenInclude(p => p.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.Country)
                .Include(b => b.InventoryBalances)
                .Select(o => new CatalogBook(o))
                .ToListAsync()
                );

            var authors = new ObservableCollection<Author>(await db.Authors.ToListAsync());

            var subCategories = new ObservableCollection<SubCategory>(await db.SubCategories.Include(s => s.MajorCategory).ToListAsync());

            var languages = new ObservableCollection<Language>(await db.Languages.ToListAsync());

            var primaryAudiences = new ObservableCollection<PrimaryAudience>(await db.PrimaryAudiences.ToListAsync());

            var publishingHouses = new ObservableCollection<PublishingHouse>
                (await db.PublishingHouses
                .Include(p => p.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.Country)
                .ToListAsync()
                );

            return new CatalogInfo(books, authors, subCategories, languages, primaryAudiences, publishingHouses);

        }

        public static void UpdateBook(Book book)
        {
            using var db = new BookstoreCompanyContext();

            var bookToUpdate = db.Books.Find(book.Isbn13);

            bookToUpdate.Title = book.Title;
            bookToUpdate.Price = book.Price;
            bookToUpdate.PrimaryAudienceId = book.PrimaryAudienceId;
            bookToUpdate.PublishingHouseId = book.PublishingHouseId;
            bookToUpdate.LanguageId = book.LanguageId;
            bookToUpdate.ReleaseDate = book.ReleaseDate;

            Debug.WriteLine(db.ChangeTracker.DebugView.ShortView);

            db.SaveChanges();

        }

        public static void RemoveBookOrBooks(List<Book> booksToRemove)
        {
            using var db = new BookstoreCompanyContext();

            foreach (var book in booksToRemove)
            {
                db.Books.Remove(book);
            }
            db.ChangeTracker.DetectChanges();
            Debug.WriteLine(db.ChangeTracker.DebugView.LongView);

            db.SaveChanges();
        }



        public static void InsertBook(Book bookToAdd)
        {
            using var db = new BookstoreCompanyContext();

            var listOfAuthors = new List<Author>();
            foreach (var author in bookToAdd.Authors)
            {
                listOfAuthors.Add(author);
            }

            var listOfSubCategories = new List<SubCategory>();
            foreach (var subCategory in bookToAdd.SubCategories)
            {
                listOfSubCategories.Add(subCategory);
            }

            bookToAdd.SubCategories = null;
            bookToAdd.Authors = null;
            db.Books.Add(bookToAdd);

            foreach (var author in listOfAuthors)
            {
                db.Authors.Find(author.Id).Isbn13s.Add(bookToAdd);
            }

            foreach (var subCategory in listOfSubCategories)
            {
                db.SubCategories.Find(subCategory.Id).Isbn13s.Add(bookToAdd);
            }
            db.ChangeTracker.DetectChanges();

            Debug.WriteLine(db.ChangeTracker.DebugView.ShortView);

            db.SaveChanges();

        }

        internal static ObservableCollection<Author> GetAuthors()
        {
            using var db = new BookstoreCompanyContext();

            return new ObservableCollection<Author>(db.Authors.ToList());
        }

        internal static void InsertAuthor(string firstName, string lastName, DateOnly dateOfBirth)
        {
            using var db = new BookstoreCompanyContext();

            db.Authors.Add(new Author()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            });
            Debug.WriteLine(db.ChangeTracker.DebugView.LongView);


            db.SaveChanges();
        }

        internal static void RemoveAuthor(Author selectedAuthor)
        {
            using var db = new BookstoreCompanyContext();

            db.Authors.Remove(selectedAuthor);

            db.SaveChanges();
        }

        internal static void UpdateAuthor(Author selectedAuthor)
        {
            using var db = new BookstoreCompanyContext();

            var author = db.Authors.Find(selectedAuthor.Id);

            author.FirstName = selectedAuthor.FirstName;
            author.LastName = selectedAuthor.LastName;
            author.DateOfBirth = selectedAuthor.DateOfBirth;

            db.SaveChanges();
        }

        internal static void AddAuthorToBook(Author author, Book book)
        {
            using var db = new BookstoreCompanyContext();

            db.Attach(book);
            Debug.WriteLine(db.ChangeTracker.DebugView.ShortView);
            book.Authors.Add(author);
            //author.Isbn13s.Add(book);
            db.ChangeTracker.DetectChanges();
            Debug.WriteLine(db.ChangeTracker.DebugView.ShortView);

            db.SaveChanges();
        }

        internal static void AddNewSubcategoryToBook(Book bookToEdit, SubCategory selectedSubCategory)
        {
            using var db = new BookstoreCompanyContext();

            db.Attach(selectedSubCategory);
            db.Attach(bookToEdit);
            bookToEdit.SubCategories.Clear();
            selectedSubCategory.Isbn13s.Add(bookToEdit);
            db.ChangeTracker.DetectChanges();

            db.SaveChanges();
        }

        public class CatalogInfo
        {
            public CatalogInfo
                (ObservableCollection<CatalogBook>? books, ObservableCollection<Author>? authors,
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

            public ObservableCollection<CatalogBook>? Books { get; set; }
            public ObservableCollection<Author>? Authors { get; set; }
            public ObservableCollection<SubCategory>? SubCategories { get; set; }
            public ObservableCollection<Language>? Languages { get; set; }
            public ObservableCollection<PrimaryAudience>? PrimaryAudiences { get; set; }
            public ObservableCollection<PublishingHouse>? PublishingHouses { get; set; }
        }


        public class CatalogBook : ViewModelBase
        {
            private readonly Book model;

            public CatalogBook(Book model)
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

    }
}
