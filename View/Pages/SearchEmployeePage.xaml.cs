using System.Windows;
using System.Windows.Controls;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchEmployeePage.xaml
    /// </summary>
    public partial class SearchEmployeePage : Page
    {
        private readonly IServiceProvider serviceProvider;
        public SearchEmployeePage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var addEmployeePage = new EmployeePage(serviceProvider);
            NavigationService.Navigate(addEmployeePage);
        }
    }
}
