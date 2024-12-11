using Bookstore_App.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Bookstore_App.Dialogs
{
    /// <summary>
    /// Interaction logic for AddInventoryBalanceDialog.xaml
    /// </summary>
    public partial class AddInventoryBalanceDialog : Window
    {
        public int selectedNumberOfUnits;

        public AddInventoryBalanceDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)App.Current.MainWindow).DataContext;

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void buttonAddBook_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as MainWindowViewModel).InventoryViewModel.GetAndSetBooks();
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var result = Int32.TryParse((sender as TextBox).Text, out selectedNumberOfUnits);
        }
    }
}
