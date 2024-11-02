﻿using Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
