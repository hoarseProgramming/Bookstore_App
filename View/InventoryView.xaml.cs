using Bookstore_App.ViewModel;
using System.Windows.Controls;

namespace Bookstore_App.View
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
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
            await (DataContext as InventoryViewModel)?.GetAndSetBooks();
        }
    }
}
