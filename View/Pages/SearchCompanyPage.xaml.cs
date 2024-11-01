using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ViewModels.Abstractions;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchCompanyPage.xaml
    /// </summary>
    public partial class SearchCompanyPage : Page
    {
        private readonly IServiceProvider serviceProvider;
        private readonly INavigationService navigationService;
        public SearchCompanyPage(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.navigationService = navigationService;
        }
        private void AddCompany(object sender, RoutedEventArgs e)
        {
            var addCompanyPage = new CompanyPage(serviceProvider, navigationService);
            NavigationService.Navigate(addCompanyPage);
        }
    }
}
