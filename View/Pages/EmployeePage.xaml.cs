using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ViewModels.Abstractions;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private readonly INavigationService navigationService;
        public EmployeePage(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            InitializeComponent();
            this.navigationService = navigationService;
        }

        private void EmployeePhotoMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                Title = "Выберите фотографию сотрудника"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                EmployeePhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
        }
        private void CancelButtonPressed(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();
        }
    }
}
