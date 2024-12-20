using Bookstore_App.Presentation.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Bookstore_App.Presentation.View
{
    /// <summary>
    /// Interaction logic for CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        public CatalogView()
        {
            InitializeComponent();
        }

        private async void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsEnabled == true)
            {
                await (DataContext as CatalogViewModel)?.GetAndSetBooksForCatalogView();
            }
        }

        private void bookListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
