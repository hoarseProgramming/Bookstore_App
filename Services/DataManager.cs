using Bookstore_App.Domain;
using Bookstore_App.Infrastructure.Data.Model;
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

        //TODO: Remove?
        //internal static async Task<Book> GetBookAsync(Book selectedBook)
        //{
        //    using var db = new BookstoreCompanyContext();

        //    var book = await db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Isbn13 == selectedBook.Isbn13);

        //    return book;
        //}


    }
}
