using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Bookstore_App.Presentation.CustomElements
{
    internal class MultiSelectDataGrid : DataGrid
    {
        public MultiSelectDataGrid()
        {
            SelectionChanged += MultiSelectDataGrid_SelectionChanged;
        }

        void MultiSelectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = SelectedItems;
        }

        #region SelectedItemsList

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(MultiSelectDataGrid), new PropertyMetadata(null));

        #endregion
    }
}
