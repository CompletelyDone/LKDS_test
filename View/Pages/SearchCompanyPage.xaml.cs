using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchCompanyPage.xaml
    /// </summary>
    public partial class SearchCompanyPage : Page
    {
        private readonly IServiceProvider serviceProvider;
        public SearchCompanyPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }
        private void AddCompany(object sender, RoutedEventArgs e)
        {
            var addCompanyPage = new CompanyPage(serviceProvider);
            NavigationService.Navigate(addCompanyPage);
        }
    }
}
