using Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModels.Abstractions;
using ViewModels.ViewModels;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CompanyPage.xaml
    /// </summary>
    public partial class CompanyPage : Page 
    {
        private readonly IServiceProvider serviceProvider;
        public CompanyPage(IServiceProvider serviceProvider, INavigationService navigationService, Company? company = null)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            var companyRepository = serviceProvider.GetRequiredService<ICompanyRepository>();
            var dialogService = serviceProvider.GetRequiredService<IDialogService>();

            DataContext = new CompanyPageVM(companyRepository, dialogService, navigationService, company);
        }
    }
}
