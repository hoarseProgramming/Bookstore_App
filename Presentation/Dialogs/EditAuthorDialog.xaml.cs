using Bookstore_App.Presentation.ViewModel;
using System.Windows;

namespace Bookstore_App.Presentation.Dialogs
{
    /// <summary>
    /// Interaction logic for EditAuthorDialog.xaml
    /// </summary>
    public partial class EditAuthorDialog : Window
    {
        public EditAuthorDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)App.Current.MainWindow).DataContext;
        }

        private void buttonUpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Other check when edit author is called
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
