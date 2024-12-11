using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Services
{
    internal static class DataManager
    {
        public static async Task<List<Store>> GetStoresAsync()
        {
            //TODO: Track start and end of operation, add debuginfo
            using var db = new BookstoreCompanyContext();

            var stores = await db.Stores.ToListAsync();

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

        //TODO: Try catch if book is already in DB
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

        //TODO: Remove?
        //internal static async Task<Book> GetBookAsync(Book selectedBook)
        //{
        //    using var db = new BookstoreCompanyContext();

        //    var book = await db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Isbn13 == selectedBook.Isbn13);

        //    return book;
        //}

        //TODO: Use view to get Names of authors?
        //public static async Task<List<BookInfo>> GetBookInfosAsync(List<Book> books)
        //{
        //    using var db = new BookstoreCompanyContext();

        //    var bookInfos = new List<BookInfo>();

        //    foreach (var book in books)
        //    {
        //        var bookInfo = await db.BookInfos
        //            .Include(bi => bi.AuthorS)
        //            .FirstOrDefaultAsync(b => b.Isbn13 == book.Isbn13);
        //        bookInfos.Add(bookInfo);
        //    }

        //    return bookInfos;
        //}
    }
}
