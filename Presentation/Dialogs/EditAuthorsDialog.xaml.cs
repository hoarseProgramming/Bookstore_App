using Bookstore_App.Presentation.ViewModel;
using System.Windows;

namespace Bookstore_App.Presentation.Dialogs
{
    public partial class EditAuthorsDialog : Window
    {
        public EditAuthorsDialog()
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
    }
}
