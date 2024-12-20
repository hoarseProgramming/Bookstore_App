using Bookstore_App.Presentation.Commands;

namespace Bookstore_App.Presentation.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public InventoryViewModel InventoryViewModel { get; } = new();
        public CatalogViewModel CatalogViewModel { get; } = new();

        public Action? ToggleFullScreen { get; set; }
        public DelegateCommand ToggleFullScreenCommand { get; }

        public DelegateCommand ShouldGoToInventoryCommand { get; }
        public EventHandler ShouldGoToInventoryMessage { get; set; }

        public DelegateCommand GoToCatalogCommand { get; set; }
        public MainWindowViewModel()
        {
            ToggleFullScreenCommand = new DelegateCommand(DoToggleFullScreen);
            ShouldGoToInventoryCommand = new DelegateCommand(ShouldGoToInventory, CanGoToInventory);
            GoToCatalogCommand = new DelegateCommand(GoToCatalog, CanGoToCatalog);
        }

        private void GoToCatalog(object obj)
        {
            InventoryViewModel.IsInventoryMode = false;
            ShouldGoToInventoryCommand.RaiseCanExecuteChanged();
            CatalogViewModel.IsCatalogMode = true;
            GoToCatalogCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoToCatalog(object? arg) => !CatalogViewModel.IsCatalogMode;

        private void ShouldGoToInventory(object obj) => ShouldGoToInventoryMessage.Invoke(this, EventArgs.Empty);

        public void GoToInventory()
        {
            CatalogViewModel.IsCatalogMode = false;
            GoToCatalogCommand.RaiseCanExecuteChanged();
            InventoryViewModel.IsInventoryMode = true;
            ShouldGoToInventoryCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoToInventory(object? arg) => !InventoryViewModel.IsInventoryMode;

        private void DoToggleFullScreen(object obj) => ToggleFullScreen?.Invoke();
    }
}
