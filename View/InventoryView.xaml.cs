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

        private void TreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            (DataContext as InventoryViewModel)?.GetAndSetInventoryBalances(e.NewValue as Store);
        }

        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var store = (sender as ListBox)?.SelectedItem as Store;
            (DataContext as InventoryViewModel)?.GetAndSetInventoryBalances(store);
        }
    }
}
