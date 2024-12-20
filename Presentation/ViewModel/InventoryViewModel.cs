﻿using Bookstore_App.Domain;
using Bookstore_App.Presentation.Commands;
using Bookstore_App.Services;
using System.Collections;
using System.Collections.ObjectModel;

namespace Bookstore_App.Presentation.ViewModel
{
    class InventoryViewModel : ViewModelBase
    {
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

        public DelegateCommand ShouldOpenAddInventoryBalanceCommand { get; }
        public Action OpenAddInventoryBalance { get; set; }

        public DelegateCommand ShouldAddInventoryBalanceCommand { get; }

        public DelegateCommand RemoveInventoryBalanceCommand { get; }

        public EventHandler ShouldSaveInventoryBalancesMessage { get; set; }
        public DelegateCommand ShouldSaveInventoryBalancesCommand { get; }


        public Action<string, string> ShowError { get; set; }
        public bool IsSaving { get; private set; }

        public InventoryViewModel()
        {
            ShouldOpenAddInventoryBalanceCommand = new DelegateCommand(DoOpenAddInventoryBalance, CanOpenAddInventoryBalance);
            RemoveInventoryBalanceCommand = new DelegateCommand(RemoveInventoryBalance, CanRemoveInventoryBalance);
            ShouldSaveInventoryBalancesCommand = new DelegateCommand(ShouldSaveInventoryBalances, CanSaveInventoryBalances);

            Stores.Add(new Store() { StoreName = "Loading" });

        }

        private void ShouldSaveInventoryBalances(object arg) => ShouldSaveInventoryBalancesMessage.Invoke(this, EventArgs.Empty);

        //TODO: Async command call, how?
        public async Task SaveInventoryBalancesAsync()
        {
            if (InventoryBalances.Any(i => i.UnitsInStock < 0))
            {
                ShowError?.Invoke("Units in stock must be non negative numbers!", "Wrong input!");
            }
            else
            {
                IsSaving = true;
                ShouldSaveInventoryBalancesCommand.RaiseCanExecuteChanged();
                try
                {
                    await DataManager.UpdateInventoryBalancesAsync(InventoryBalances.ToList(), SelectedStore.Id);
                }
                catch (Exception)
                {
                    ShowError?.Invoke("Couldn't save changes to inventory balances!", "Error!");
                }
                IsSaving = false;
                ShouldSaveInventoryBalancesCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanSaveInventoryBalances(object? arg) => !IsSaving && SelectedStore is not null;

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

        private bool CanRemoveInventoryBalance(object? args) => SelectedInventoryBalances.Count > 0;


        public void AddInventoryBalance(int unitsInStock)
        {
            InventoryBalances.Add(new InventoryBalance() { Book = SelectedBook, Isbn13 = SelectedBook.Isbn13, Store = SelectedStore, StoreId = SelectedStore.Id, UnitsInStock = unitsInStock });
            isPossibleToAddBooks = InventoryBalances.Count < numberOfBooksInCatalog ? true : false;
            ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
        }
        private void DoOpenAddInventoryBalance(object obj) => OpenAddInventoryBalance?.Invoke();

        private bool CanOpenAddInventoryBalance(object? arg) => IsInventoryMode && SelectedStore is not null && InventoryBalances.Count < numberOfBooksInCatalog ? true : false;

        public async Task GetAndSetBooksForInventoryView()
        {
            try
            {
                var books = await DataManager.GetBooksAsync();

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
                ShouldOpenAddInventoryBalanceCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                ShowError?.Invoke("Couldn't load book catalog", "Error!");
            }
        }
        //TODO: Invoke Event to show messagebox with eventargs with message to show (taken from exception message?)

        //TODO: Make async / Command for main menu
        public async Task GetAndSetStoresAsync()
        {
            //TODO: FIX ERROR HANDLING
            try
            {
                Stores = new ObservableCollection<Store>(await DataManager.GetStoresAsync());
            }
            catch (Exception ex)
            {
                ShowError?.Invoke("Couldn't load stores", "Error!");
            }

            if (Stores.Count == 0)
            {
                Stores.Add(new Store() { StoreName = "Couldn't load stores" });
                ShowError?.Invoke("Couldn't load stores", "Error!");
                StoresAreLoadedSuccessfully = false;
            }
            else
            {
                StoresAreLoadedSuccessfully = true;
            }
        }
        //TODO: Invoke Event to show messagebox with eventargs with message to show (taken from exception message?)

        //TODO: Make async! 
        public async Task GetAndSetInventoryBalancesAsync(Store store)
        {
            try
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
            catch (Exception ex)
            {

                ShowError?.Invoke("Couldn't load Inventory balances for store", "Error!");
            }

        }
    }
}
