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
        public SearchCompanyPage()
        {
            InitializeComponent();
        }
        private void AddCompany(object sender, RoutedEventArgs e)
        {
            var addCompanyPage = new CompanyPage();
            NavigationService.Navigate(addCompanyPage);
        }
    }
}
