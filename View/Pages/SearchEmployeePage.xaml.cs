using Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using ViewModels.Abstractions;
using ViewModels.ViewModels;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchEmployeePage.xaml
    /// </summary>
    public partial class SearchEmployeePage : Page
    {
        private readonly IServiceProvider serviceProvider;
        private readonly INavigationService navigationService;
        public SearchEmployeePage(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.navigationService = navigationService;

            var employeeRepository = serviceProvider.GetRequiredService<IEmployeeRepository>();

            DataContext = new SearchEmployeePageVM(employeeRepository, navigationService);
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var addEmployeePage = new EmployeePage(serviceProvider, navigationService);
            NavigationService.Navigate(addEmployeePage);
        }
    }
}
