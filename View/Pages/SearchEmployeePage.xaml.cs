using System.Windows;
using System.Windows.Controls;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchEmployeePage.xaml
    /// </summary>
    public partial class SearchEmployeePage : Page
    {
        public SearchEmployeePage()
        {
            InitializeComponent();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var addEmployeePage = new EmployeePage();
            NavigationService.Navigate(addEmployeePage);
        }
    }
}
