using Bookstore_App.Presentation.Commands;

namespace Bookstore_App.Presentation.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public InventoryViewModel InventoryViewModel { get; } = new();

        public Action? ToggleFullScreen { get; set; }
        public DelegateCommand ToggleFullScreenCommand { get; }
        public MainWindowViewModel()
        {
            ToggleFullScreenCommand = new DelegateCommand(DoToggleFullScreen);
        }

        private void DoToggleFullScreen(object obj) => ToggleFullScreen?.Invoke();
    }
}
