using Bookstore_App.Presentation.ViewModel;
using System.Windows;

namespace Bookstore_App.Presentation.Dialogs
{
    public partial class AddBookDialog : Window
    {
        public AddBookDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)App.Current.MainWindow).DataContext;
        }

        private void buttonAddBook_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as MainWindowViewModel).CatalogViewModel.GetBookInputIsCorrect())
            {
                DialogResult = true;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
