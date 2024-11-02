using Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using View.Utils;
using ViewModels.Abstractions;
using ViewModels.ViewModels;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private readonly INavigationService navigationService;
        public EmployeePage(IServiceProvider serviceProvider, INavigationService navigationService, Employee? employee = null)
        {
            InitializeComponent();
            this.navigationService = navigationService;

            var employeeRepository = serviceProvider.GetRequiredService<IEmployeeRepository>();
            var companyRepository = serviceProvider.GetRequiredService<ICompanyRepository>();
            var dialogService = serviceProvider.GetRequiredService<IDialogService>();

            DataContext = new EmployeePageVM(employeeRepository, companyRepository, dialogService, navigationService, employee);
        }

        private void EmployeePhotoMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dialogService = new DialogService();
            var photoPath = dialogService.GetPhotoPath();

            if (!string.IsNullOrEmpty(photoPath))
            {
                EmployeePhoto.Source = new BitmapImage(new Uri(photoPath));

                var viewModel = (EmployeePageVM)DataContext;
                viewModel.PhotoPath = photoPath;
            }
        }
    }
}
