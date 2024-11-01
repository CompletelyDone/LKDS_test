using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using ViewModels.Abstractions;
using ViewModels.Services;
using ViewModels.ViewModels;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private readonly IServiceProvider serviceProvider;
        public MenuPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            var dataService = serviceProvider.GetRequiredService<DataService>();
            var dialogService = serviceProvider.GetRequiredService<IDialogService>();
            DataContext = new MenuPageVM(dataService, dialogService);
        }

        private void OnNavigateButtonPressed(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                string buttonTag = button.Tag.ToString();
                switch (buttonTag)
                {
                    case "AddEmployee":
                        var employeePage = new EmployeePage(serviceProvider);
                        NavigationService.Navigate(employeePage);
                        break;
                    case "SearchEmployee":
                        var searchEmployeePage = new SearchEmployeePage(serviceProvider);
                        NavigationService.Navigate(searchEmployeePage);
                        break;
                    case "AddCompany":
                        var companyPage = new CompanyPage(serviceProvider);
                        NavigationService.Navigate(companyPage);
                        break;
                    case "SearchCompany":
                        var searchCompanyPage = new SearchCompanyPage(serviceProvider);
                        NavigationService.Navigate(searchCompanyPage);
                        break;
                }
            }
        }
    }
}
