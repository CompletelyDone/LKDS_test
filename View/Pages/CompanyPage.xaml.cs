using Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModels.ViewModels;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CompanyPage.xaml
    /// </summary>
    public partial class CompanyPage : Page 
    {
        public CompanyPage(IServiceProvider serviceProvider, Company? company = null)
        {
            InitializeComponent();

            var companyRepository = serviceProvider.GetRequiredService<ICompanyRepository>();

            DataContext = new CompanyPageVM(companyRepository, company);
        }
        private void CancelButtonPressed(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack) NavigationService.GoBack();
        }
    }
}
