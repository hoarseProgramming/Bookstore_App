using Bookstore_App.Domain;
using Bookstore_App.Presentation.ViewModel;
using System.Windows.Controls;

namespace Bookstore_App.Presentation.View
{
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
        }

        private async void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var store = (sender as ListBox)?.SelectedItem as Store;
            await (DataContext as InventoryViewModel)?.GetAndSetInventoryBalancesAsync(store);
            await (DataContext as InventoryViewModel)?.GetAndSetBooksForInventoryView();
        }

        private async void UserControl_IsEnabledChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.IsEnabled == true)
            {
                await (DataContext as InventoryViewModel)?.GetAndSetStoresAsync();
            }
        }
    }
}
