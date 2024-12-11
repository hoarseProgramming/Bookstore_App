using Bookstore_App.Commands;
using Bookstore_App.Services;
using System.Collections;
using System.Collections.ObjectModel;

namespace Bookstore_App.ViewModel
{
    class InventoryViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private bool _isInventoryMode = true;

        public bool IsInventoryMode
        {
            get => _isInventoryMode = true;
            set
            {
                _isInventoryMode = value;
                ShouldOpenAddBookCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        private bool isPossibleToAddBooks = false;
        private int numberOfBooksInCatalog = 0;

        private ObservableCollection<Store> _stores = new();

        public ObservableCollection<Store> Stores
        {
            get => _stores;
            set
            {
                _stores = value;
                RaisePropertyChanged();
            }
        }
        private Store _activeStore;

        public Store ActiveStore

        {
            get => _activeStore;
            set
            {
                _activeStore = value;
                ShouldOpenAddBookCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<InventoryBalance> _inventoryBalances = new();

        public ObservableCollection<InventoryBalance> InventoryBalances
        {
            get => _inventoryBalances;
            set
            {
                _inventoryBalances = value;
                RaisePropertyChanged();
            }
        }

        private IList _selectedInventoryBalances = new ArrayList();

        public IList SelectedInventoryBalances

        {
            get => _selectedInventoryBalances;
            set
            {
                _selectedInventoryBalances = value;
                RaisePropertyChanged();
                RemoveInventoryBalanceCommand.RaiseCanExecuteChanged();
            }
        }

        private InventoryBalance _activeInventoryBalance;

        public InventoryBalance ActiveInventoryBalance

        {
            get => _activeInventoryBalance;
            set
            {
                _activeInventoryBalance = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Book> _books = new();

        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                RaisePropertyChanged();
            }
        }

        private Book _selectedBook;

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand ShouldOpenAddBookCommand { get; }
        public EventHandler ShouldOpenAddBookMessage;

        public DelegateCommand ShouldAddBookCommand { get; }
        public EventHandler ShouldAddBookMessage;

        public DelegateCommand RemoveInventoryBalanceCommand { get; }
        public InventoryViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            ShouldOpenAddBookCommand = new DelegateCommand(ShouldOpenAddBook, CanOpenAddBook);
            ShouldAddBookCommand = new DelegateCommand(ShouldAddBook, CanAddBook);
            RemoveInventoryBalanceCommand = new DelegateCommand(RemoveInventoryBalance, CanRemoveInventoryBalance);

            Stores.Add(new Store() { StoreName = "Loading" });

        }
        private void RemoveInventoryBalance(object obj)
        {
            var inventoryBalancesToRemove = new List<InventoryBalance>();

            foreach (var inventoryBalance in SelectedInventoryBalances)
            {
                inventoryBalancesToRemove.Add((InventoryBalance)inventoryBalance);
            }

            for (int i = 0; i < inventoryBalancesToRemove.Count; i++)
            {
                InventoryBalances.Remove(inventoryBalancesToRemove[i]);
            }

            ShouldOpenAddBookCommand.RaiseCanExecuteChanged();
        }

        private bool CanRemoveInventoryBalance(object? args) => SelectedInventoryBalances.Count > 0;


        public async Task AddBookAsync(int unitsInStock)
        {
            InventoryBalances.Add(new InventoryBalance() { Book = SelectedBook, Isbn13 = SelectedBook.Isbn13, Store = ActiveStore, StoreId = ActiveStore.Id, UnitsInStock = unitsInStock });

            //SelectedBook = Books.FirstOrDefault();
            isPossibleToAddBooks = InventoryBalances.Count < numberOfBooksInCatalog ? true : false;
            ShouldOpenAddBookCommand.RaiseCanExecuteChanged();
        }
        //TODO: Jadu, ska man ha de här?
        private void ShouldAddBook(object obj) => ShouldAddBookMessage.Invoke(this, EventArgs.Empty);
        private bool CanAddBook(object? arg) => true;

        //Delete book = Set ispossible to add book to true and raisecanexecutechanged on should open addbookcommand


        private void ShouldOpenAddBook(object obj) => ShouldOpenAddBookMessage.Invoke(this, EventArgs.Empty);

        private bool CanOpenAddBook(object? arg) => IsInventoryMode && ActiveStore is not null && InventoryBalances.Count < numberOfBooksInCatalog ? true : false;

        public async Task GetAndSetBooks()
        {
            var books = await DataManager.GetBooksAsync();

            numberOfBooksInCatalog = books.Count;

            var newBooks = new ObservableCollection<Book>();

            foreach (var book in books)
            {
                if (!(InventoryBalances.Any(i => i.Isbn13 == book.Isbn13)))
                {
                    newBooks.Add(book);
                }
            }

            Books = newBooks;

            SelectedBook = Books.FirstOrDefault();

            isPossibleToAddBooks = InventoryBalances.Count < numberOfBooksInCatalog ? true : false;
            ShouldOpenAddBookCommand.RaiseCanExecuteChanged();
        }

        //TODO: Make async / Command
        public async Task GetAndSetStoresAsync()
        {
            var stores = await DataManager.GetStoresAsync();

            var currentStores = new ObservableCollection<Store>();

            foreach (var store in stores)
            {
                currentStores.Add(store);
            }

            Stores = currentStores;
        }

        //TODO: Make async! Make as Command!?
        public async Task GetAndSetInventoryBalancesAsync(Store store)
        {
            var defaultInventoryBalance = new InventoryBalance() { Book = new Book() { Title = "Loading" } };
            InventoryBalances = new ObservableCollection<InventoryBalance>() { defaultInventoryBalance };


            ObservableCollection<InventoryBalance> newInventoryBalances = new();

            var inventoryBalances = await DataManager.GetInventoryBalancesAsync(store);

            foreach (var inventoryBalance in inventoryBalances)
            {
                newInventoryBalances.Add(inventoryBalance);
            }

            InventoryBalances = newInventoryBalances;
        }

        //Make to command
        //public async void AddBookToTinventory(Store store, Book book, int numberOfUnits)
        //{
        //    DataManager.AddInventoryBalance(store, book, numberOfUnits);
        //}
    }
}
