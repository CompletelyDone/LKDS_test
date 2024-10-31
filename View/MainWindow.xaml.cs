using System.Windows;
using System.Windows.Navigation;
using View.Pages;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationService navigationService;
        public MainWindow()
        {
            InitializeComponent();
            navigationService = MainWindowFrame.NavigationService;

            var menuPage = new MenuPage();
            navigationService.Navigate(menuPage);
        }
    }
}