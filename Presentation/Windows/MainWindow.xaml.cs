using Bookstore_App.Presentation.Dialogs;
using Bookstore_App.Presentation.ViewModel;
using System.Windows;

namespace Bookstore_App
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel = new();

        private WindowState stateBeforeFullScreenToggled;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
            mainWindowViewModel.InventoryViewModel.ShowError = (message, caption) => MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            mainWindowViewModel.InventoryViewModel.OpenAddInventoryBalance = OpenAddInventoryBalanceDialog;
            mainWindowViewModel.ToggleFullScreen = ToggleFullScreen;
            Loaded += MainWindow_Loaded;

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await mainWindowViewModel.InventoryViewModel.GetAndSetStoresAsync();
        }

        public void ToggleFullScreen()
        {
            if (!(this.WindowState == WindowState.Maximized))
            {
                stateBeforeFullScreenToggled = WindowState;
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = stateBeforeFullScreenToggled;
                ResizeMode = ResizeMode.CanResize;
            }
        }

        private async void OpenAddInventoryBalanceDialog()
        {
            AddInventoryBalanceDialog addInventoryBalanceDialog = new();

            var result = addInventoryBalanceDialog.ShowDialog();

            if (result == true)
            {
                await mainWindowViewModel.InventoryViewModel.AddInventoryBalanceAsync(addInventoryBalanceDialog.selectedNumberOfUnits);
            }
        }
    }
}