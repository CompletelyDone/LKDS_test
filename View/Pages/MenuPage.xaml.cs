using System.Windows.Controls;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
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
                        var employeePage = new EmployeePage();
                        NavigationService.Navigate(employeePage);
                        break;
                    case "SearchEmployee":
                        var searchEmployeePage = new SearchEmployeePage();
                        NavigationService.Navigate(searchEmployeePage);
                        break;
                    case "AddCompany":
                        var companyPage = new CompanyPage();
                        NavigationService.Navigate(companyPage);
                        break;
                    case "SearchCompany":
                        var searchCompanyPage = new SearchCompanyPage();
                        NavigationService.Navigate(searchCompanyPage);
                        break;
                }
            }
        }
    }
}
