using Bookstore_App.Presentation.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Bookstore_App.Presentation.Dialogs
{
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
            await ((MainWindowViewModel)DataContext).InventoryViewModel.GetAndSetBooksForInventoryView();
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            bool success = false;

            if (textBox is not null)
            {
                success = int.TryParse(textBox.Text, out selectedNumberOfUnits);
                if (success)
                {
                    success = selectedNumberOfUnits >= 0;
                }
            }

            if (!success)
            {
                MessageBox.Show("Invalid input! Must be a positive number!", "Wrong input!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
