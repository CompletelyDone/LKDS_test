using Model;
using System.Windows;
using System.Windows.Controls;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CompanyPage.xaml
    /// </summary>
    public partial class CompanyPage : Page
    {
        private Company? currentCompany;
        public CompanyPage()
        {
            InitializeComponent();
        }
        public CompanyPage(Company company) : this()
        {
            currentCompany = company;
            TitleTextBox.Text = company.Title;
            DeleteButton.IsEnabled = true;
        }
        private void SaveButtonPressed(object sender, RoutedEventArgs e)
        {
            if (currentCompany == null)
            {
                var newCompany = new Company(Guid.NewGuid(), TitleTextBox.Text);
            }
            else
            {
                currentCompany.Title = TitleTextBox.Text;
            }

            NavigationService.GoBack();
        }
        private void DeleteButtonPressed(object sender, RoutedEventArgs e)
        {
            if(currentCompany != null)
            {

            }
        }
        private void CancelButtonPressed(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack) NavigationService.GoBack();
        }
    }
}
