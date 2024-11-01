using System.Windows;
using System.Windows.Navigation;
using View.Pages;
using View.Utils;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationService navigationService;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            navigationService = MainWindowFrame.NavigationService;

            var navigator = new Navigator(serviceProvider, MainWindowFrame);

            var menuPage = new MenuPage(serviceProvider, navigator);
            navigationService.Navigate(menuPage);
        }
    }
}