using System.Windows;
using System.Windows.Controls;
using ViewModels.Abstractions;

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
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var addEmployeePage = new EmployeePage(serviceProvider, navigationService);
            NavigationService.Navigate(addEmployeePage);
        }
    }
}
