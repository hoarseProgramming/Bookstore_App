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
        public static List<Store> GetStores()
        {
            //TODO: Track start and end of operation, add debuginfo, FIX ERROR HANDLING

            var stores = new List<Store>();

            using var db = new BookstoreCompanyContext();

            stores = db.Stores.ToList();

            return stores;
        }

        public static List<InventoryBalance> GetInventoryBalancesForStore(Store store)
        {
            using var db = new BookstoreCompanyContext();

            var inventoryBalances = db.InventoryBalances
                .Include(i => i.Store)
                .Include(i => i.Book)
                .ThenInclude(b => b.Authors)
                .Where(i => i.Store.Equals(store))
                .ToList();

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

        public static List<Book> GetBooksForInventory()
        {
            using var db = new BookstoreCompanyContext();

            return db.Books.Include(b => b.Authors).ToList();
        }

        public static void UpdateInventoryBalances(List<InventoryBalance> updatedInventoryBalances, int id)
        {
            using var db = new BookstoreCompanyContext();

            var inventoryBalances = db.InventoryBalances.Where(i => i.StoreId == id).ToList();

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

            db.SaveChanges();
        }

        public static CatalogInfo GetCatalogInfo()
        {
            using var db = new BookstoreCompanyContext();

            var books = new ObservableCollection<BookViewModel>
                (db.Books
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
                .Select(o => new BookViewModel(o))
                .ToList()
                );

            var authors = new ObservableCollection<Author>(db.Authors.ToList());

            var subCategories = new ObservableCollection<SubCategory>(db.SubCategories.Include(s => s.MajorCategory).ToList());

            var languages = new ObservableCollection<Language>(db.Languages.ToList());

            var primaryAudiences = new ObservableCollection<PrimaryAudience>(db.PrimaryAudiences.ToList());

            var publishingHouses = new ObservableCollection<PublishingHouse>
                (db.PublishingHouses
                .Include(p => p.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.Country)
                .ToList()
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

    }
}
