using Bookstore_App.Dialogs;
using Bookstore_App.ViewModel;
using System.Windows;

namespace Bookstore_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel = new();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
            Loaded += MainWindow_Loaded;

            SubscribeToEvents();

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await mainWindowViewModel.InventoryViewModel.GetAndSetStoresAsync();
        }

        private void SubscribeToEvents()
        {
            mainWindowViewModel.InventoryViewModel.ShouldOpenAddBookMessage += OnsShouldOpenAddBookMessageRecieved;
        }

        private async void OnsShouldOpenAddBookMessageRecieved(object? sender, EventArgs e)
        {
            AddInventoryBalanceDialog addInventoryBalanceDialog = new();

            var result = addInventoryBalanceDialog.ShowDialog();

            if (result == true)
            {
                await mainWindowViewModel.InventoryViewModel.AddBookAsync(addInventoryBalanceDialog.selectedNumberOfUnits);
            }
        }
    }
}