using Bookstore_App.Domain;
using Bookstore_App.Presentation.Commands;
using Bookstore_App.Services;
using System.Collections;
using System.Collections.ObjectModel;

namespace Bookstore_App.Presentation.ViewModel;

class InventoryViewModel : ViewModelBase
{
    private MainWindowViewModel mainWindowViewModel;

    private bool _isInventoryMode = false;
    public bool IsInventoryMode
    {
        get => _isInventoryMode;
        set
        {
            _isInventoryMode = value;
            RaisePropertyChanged();
            ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
            ShouldSaveInventoryBalancesCommand.RaiseCanExecuteChanged();
        }
    }

    private bool _storesAreLoadedSuccessfully = false;
    public bool StoresAreLoadedSuccessfully
    {
        get => _storesAreLoadedSuccessfully;
        set
        {
            _storesAreLoadedSuccessfully = value;
            ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged();
        }
    }

    private bool _inventoryBalancesAreLoadedSuccessfully = false;
    public bool InventoryBalancesAreLoadedSuccessfully
    {
        get => _inventoryBalancesAreLoadedSuccessfully;
        set
        {
            _inventoryBalancesAreLoadedSuccessfully = value;
            ShouldSaveInventoryBalancesCommand.RaiseCanExecuteChanged();
            ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged();
        }
    }

    private bool _isSaving = false;
    public bool IsSaving
    {
        get => _isSaving;
        set
        {
            _isSaving = value;
            ShouldSaveInventoryBalancesCommand.RaiseCanExecuteChanged();
            ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
            RemoveInventoryBalanceCommand.RaiseCanExecuteChanged();
        }
    }

    private bool isPossibleToAddBooks = false;
    private int numberOfBooksInCatalog = 0;

    private ObservableCollection<Store> _stores;
    public ObservableCollection<Store> Stores
    {
        get => _stores;
        set
        {
            _stores = value;
            RaisePropertyChanged();
        }
    }

    private Store _selectedStore;
    public Store SelectedStore

    {
        get => _selectedStore;
        set
        {
            _selectedStore = value;
            ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
            ShouldSaveInventoryBalancesCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<InventoryBalance> _inventoryBalances = [];
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

    public DelegateCommand ShouldOpenAddInventoryBalanceCommand { get; }
    public Action OpenAddInventoryBalance { get; set; }

    public DelegateCommand ShouldAddInventoryBalanceCommand { get; }

    public DelegateCommand RemoveInventoryBalanceCommand { get; }

    public EventHandler ShouldSaveInventoryBalancesMessage { get; set; }
    public DelegateCommand ShouldSaveInventoryBalancesCommand { get; }

    public Action<string, string> ShowError { get; set; }

    public InventoryViewModel()
    {
        ShouldOpenAddInventoryBalanceCommand = new DelegateCommand(DoOpenAddInventoryBalance, CanOpenAddInventoryBalance);
        RemoveInventoryBalanceCommand = new DelegateCommand(RemoveInventoryBalance, CanRemoveInventoryBalance);
        ShouldSaveInventoryBalancesCommand = new DelegateCommand(ShouldSaveInventoryBalances, CanSaveInventoryBalances);
    }

    public void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        this.mainWindowViewModel = mainWindowViewModel;
    }
    private void ShouldSaveInventoryBalances(object arg) => ShouldSaveInventoryBalancesMessage.Invoke(this, EventArgs.Empty);
    public async Task SaveInventoryBalancesAsync()
    {
        if (InventoryBalances.Any(i => i.UnitsInStock < 0))
        {
            ShowError?.Invoke("Units in stock must be non negative numbers!", "Wrong input!");
        }
        else
        {
            IsSaving = true;

            await Task.Run(() =>
            {
                try
                {
                    DataManager.UpdateInventoryBalances(InventoryBalances.ToList(), SelectedStore.Id);
                }
                catch (Exception)
                {
                    ShowError?.Invoke("Couldn't save changes to inventory balances!", "Error!");
                }
            });

            IsSaving = false;
        }
    }
    private bool CanSaveInventoryBalances(object? arg) => !IsSaving && SelectedStore is not null && InventoryBalancesAreLoadedSuccessfully && StoresAreLoadedSuccessfully;
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

        ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
    }
    private bool CanRemoveInventoryBalance(object? args) => SelectedInventoryBalances.Count > 0 && !IsSaving;
    public void AddInventoryBalance(int unitsInStock)
    {
        InventoryBalances.Add(new InventoryBalance() { Book = SelectedBook, Isbn13 = SelectedBook.Isbn13, Store = SelectedStore, StoreId = SelectedStore.Id, UnitsInStock = unitsInStock });
        isPossibleToAddBooks = InventoryBalances.Count < numberOfBooksInCatalog ? true : false;
        ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
    }
    private void DoOpenAddInventoryBalance(object obj) => OpenAddInventoryBalance?.Invoke();
    private bool CanOpenAddInventoryBalance(object? arg) =>
        IsInventoryMode &&
        SelectedStore is not null &&
        !IsSaving &&
        StoresAreLoadedSuccessfully
        && InventoryBalancesAreLoadedSuccessfully
        && InventoryBalances.Count < numberOfBooksInCatalog ? true : false;
    public async Task GetAndSetStoresAsync()
    {
        SetStoresToMessage("Loading");
        StoresAreLoadedSuccessfully = false;

        await Task.Run(() =>
        {
            try
            {
                Stores = new ObservableCollection<Store>(DataManager.GetStores());
            }
            catch (Exception)
            {
                SetStoresToMessage("Couldn't load stores");
                ShowError?.Invoke("Couldn't load stores", "Error!");
                return;
            }
        });

        StoresAreLoadedSuccessfully = true;
    }
    private void SetStoresToMessage(string message)
    {
        Stores = new ObservableCollection<Store>() { new Store() { StoreName = message } };
    }
    public async Task GetAndSetInventoryBalancesAsync(Store store)
    {
        SetInventoryBalancesToMessage("Loading");
        InventoryBalancesAreLoadedSuccessfully = false;
        await Task.Run(() =>
        {
            try
            {
                InventoryBalances = new ObservableCollection<InventoryBalance>(DataManager.GetInventoryBalancesForStore(store));
            }
            catch (Exception)
            {
                SetInventoryBalancesToMessage("Couldn't load inventory balances");

                ShowError?.Invoke("Couldn't load Inventory balances for store", "Error!");
                return;
            }
        });

        InventoryBalancesAreLoadedSuccessfully = true;
    }
    private void SetInventoryBalancesToMessage(string message)
    {
        InventoryBalances = new ObservableCollection<InventoryBalance>()
        {
            new InventoryBalance() { Book = new Book() { Title = message } }
        };
    }
    public async Task GetAndSetBooksForInventoryView()
    {
        await Task.Run(() =>
        {
            try
            {
                var books = DataManager.GetBooksForInventory();

                numberOfBooksInCatalog = books.Count;

                var newBooks = new ObservableCollection<Book>();

                foreach (var book in books)
                {
                    if (!InventoryBalances.Any(i => i.Isbn13 == book.Isbn13))
                    {
                        newBooks.Add(book);
                    }
                }

                Books = newBooks;

                SelectedBook = Books.FirstOrDefault();

                isPossibleToAddBooks = InventoryBalances.Count < numberOfBooksInCatalog ? true : false;
            }
            catch (Exception ex)
            {
                ShowError?.Invoke("Couldn't load book catalog", "Error!");
            }
        });

        ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
    }
}
