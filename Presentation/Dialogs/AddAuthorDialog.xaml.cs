using Bookstore_App.Presentation.ViewModel;
using System.Windows;

namespace Bookstore_App.Presentation.Dialogs
{
    public partial class AddAuthorDialog : Window
    {
        public AddAuthorDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)App.Current.MainWindow).DataContext;
        }

        private void buttonAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as MainWindowViewModel).CatalogViewModel.GetAuthorInputIsCorrect())
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
