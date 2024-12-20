using Bookstore_App.Presentation.ViewModel;
using System.Windows;

namespace Bookstore_App.Presentation.Dialogs
{
    public partial class EditBookDialog : Window
    {
        public EditBookDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)App.Current.MainWindow).DataContext;
            Closing += EditBookDialog_Closing;
        }

        private void EditBookDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainWindowViewModel).CatalogViewModel.StopEditing();
        }

        private void buttonUpdateBook_Click(object sender, RoutedEventArgs e)
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
