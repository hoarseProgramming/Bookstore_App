
namespace Bookstore_App.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public InventoryViewModel? InventoryViewModel { get; }
        public MainWindowViewModel()
        {
            InventoryViewModel = new InventoryViewModel(this);
        }
    }
}
