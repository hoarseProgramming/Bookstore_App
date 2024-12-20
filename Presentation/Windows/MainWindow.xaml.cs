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
            mainWindowViewModel.CatalogViewModel.ShowError = (message, caption) => MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            mainWindowViewModel.InventoryViewModel.OpenAddInventoryBalance = OpenAddInventoryBalanceDialog;
            mainWindowViewModel.ToggleFullScreen = ToggleFullScreen;
            mainWindowViewModel.CatalogViewModel.OpenAddBook = OpenAddBookDialog;
            mainWindowViewModel.CatalogViewModel.OpenAddAuthor = OpenAddAuthorDialog;
            mainWindowViewModel.CatalogViewModel.OpenEditAuthors = OpenEditAuthorsDialog;
            mainWindowViewModel.CatalogViewModel.OpenEditAuthor = OpenEditAuthorDialog;
            mainWindowViewModel.CatalogViewModel.OpenEditBook = OpenEditBookDialog;


            mainWindowViewModel.InventoryViewModel.ShouldSaveInventoryBalancesMessage += OnShouldSaveMessageRecieved;

            mainWindowViewModel.ShouldGoToInventoryMessage += OnShouldGoToInventoryMessageRecieved;
        }

        private void OpenEditBookDialog()
        {
            var editBookDialog = new EditBookDialog();

            var result = editBookDialog.ShowDialog();

            if (result == true)
            {
                mainWindowViewModel.CatalogViewModel.UpdateBook();
            }
        }

        //TODO: FIX async
        private async void OpenEditAuthorDialog()
        {
            var editAuthorsDialog = new EditAuthorDialog();

            var result = editAuthorsDialog.ShowDialog();

            if (result == true)
            {
                await mainWindowViewModel.CatalogViewModel.UpdateAuthor();
            }
        }

        private void OpenEditAuthorsDialog()
        {
            var editAuthorsDialog = new EditAuthorsDialog();

            var result = editAuthorsDialog.ShowDialog();

        }

        private void OpenAddAuthorDialog()
        {
            var addAuthorDialog = new AddAuthorDialog();

            var result = addAuthorDialog.ShowDialog();

            if (result == true)
            {
                mainWindowViewModel.CatalogViewModel.AddNewAuthorToDatabase();
            }
        }

        private void OpenAddBookDialog()
        {
            var addBookDialog = new AddBookDialog();

            var result = addBookDialog.ShowDialog();

            if (result == true)
            {
                mainWindowViewModel.CatalogViewModel.AddBook();
            }
        }

        private void OnShouldGoToInventoryMessageRecieved(object? sender, EventArgs e)
        {
            mainWindowViewModel.GoToInventory();
        }

        private async void OnShouldSaveMessageRecieved(object? sender, EventArgs e)
        {
            if (sender is InventoryViewModel)
            {
                await mainWindowViewModel.InventoryViewModel.SaveInventoryBalancesAsync();
            }
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

        private void OpenAddInventoryBalanceDialog()
        {
            AddInventoryBalanceDialog addInventoryBalanceDialog = new();

            var result = addInventoryBalanceDialog.ShowDialog();

            if (result == true)
            {
                mainWindowViewModel.InventoryViewModel.AddInventoryBalance(addInventoryBalanceDialog.selectedNumberOfUnits);
            }
        }
    }
}