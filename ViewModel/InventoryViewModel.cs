using Microsoft.EntityFrameworkCore;
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
                RaisePropertyChanged();
            }
        }

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

        public InventoryViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            //TODO: Should be async and sent by mainwindow loaded method?
            GetStores();
        }

        //TODO: Make async
        public void GetStores()
        {
            using var db = new BookstoreCompanyContext();

            var stores = db.Stores.ToList();

            foreach (var store in stores)
            {
                Stores.Add(store);
            }
        }

        //TODO: Make async! Make as Command!?
        public void GetAndSetInventoryBalances(Store store)
        {
            ObservableCollection<InventoryBalance> newInventoryBalances = new();

            using var db = new BookstoreCompanyContext();

            var inventoryBalances = db.InventoryBalances
                .Include(i => i.Store)
                .Include(i => i.Book)
                .Where(i => i.Store.Equals(store))
                .ToList();

            foreach (var inventoryBalance in inventoryBalances)
            {
                newInventoryBalances.Add(inventoryBalance);
            }

            InventoryBalances = newInventoryBalances;
        }
    }
}
