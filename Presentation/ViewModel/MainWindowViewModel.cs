using Bookstore_App.Presentation.Commands;

namespace Bookstore_App.Presentation.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public InventoryViewModel InventoryViewModel { get; } = new();
        public CatalogViewModel CatalogViewModel { get; } = new();

        private bool _IsMainWindowMode = true;

        public bool IsMainWIndowMode
        {
            get => _IsMainWindowMode;
            set
            {
                _IsMainWindowMode = value;
                RaisePropertyChanged();
                GoToMainMenuCommand.RaiseCanExecuteChanged();
            }
        }

        public Action? ToggleFullScreen { get; set; }
        public DelegateCommand ToggleFullScreenCommand { get; }

        public DelegateCommand GoToInventoryCommand { get; }
        public EventHandler ShouldGoToInventoryMessage { get; set; }

        public DelegateCommand GoToCatalogCommand { get; set; }
        public DelegateCommand GoToMainMenuCommand { get; set; }
        public MainWindowViewModel()
        {
            ToggleFullScreenCommand = new DelegateCommand(DoToggleFullScreen);
            GoToInventoryCommand = new DelegateCommand(ShouldGoToInventory, CanGoToInventory);
            GoToCatalogCommand = new DelegateCommand(GoToCatalog, CanGoToCatalog);
            GoToMainMenuCommand = new DelegateCommand(GoToMainWindow, CanGoToMainWIndow);
        }

        private void GoToMainWindow(object obj)
        {
            if (InventoryViewModel.IsInventoryMode)
            {
                InventoryViewModel.IsInventoryMode = false;
                GoToInventoryCommand.RaiseCanExecuteChanged();
            }
            else
            {
                CatalogViewModel.IsCatalogMode = false;
                GoToCatalogCommand.RaiseCanExecuteChanged();
            }

            IsMainWIndowMode = !IsMainWIndowMode;
        }

        private bool CanGoToMainWIndow(object? arg) => !IsMainWIndowMode;
        private void GoToCatalog(object obj)
        {
            if (IsMainWIndowMode)
            {
                IsMainWIndowMode = !IsMainWIndowMode;
            }
            else
            {
                InventoryViewModel.IsInventoryMode = false;
                GoToInventoryCommand.RaiseCanExecuteChanged();
            }

            CatalogViewModel.IsCatalogMode = true;
            GoToCatalogCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoToCatalog(object? arg) => !CatalogViewModel.IsCatalogMode;

        private void ShouldGoToInventory(object obj) => ShouldGoToInventoryMessage.Invoke(this, EventArgs.Empty);

        public void GoToInventory()
        {
            if (IsMainWIndowMode)
            {
                IsMainWIndowMode = !IsMainWIndowMode;
            }
            else
            {
                CatalogViewModel.IsCatalogMode = false;
                GoToCatalogCommand.RaiseCanExecuteChanged();
            }

            InventoryViewModel.IsInventoryMode = true;
            GoToInventoryCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoToInventory(object? arg) => !InventoryViewModel.IsInventoryMode;

        private void DoToggleFullScreen(object obj) => ToggleFullScreen?.Invoke();
    }
}
