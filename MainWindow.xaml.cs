using Bookstore_App.ViewModel;
using System.Windows;

namespace Bookstore_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel = new();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }
    }
}