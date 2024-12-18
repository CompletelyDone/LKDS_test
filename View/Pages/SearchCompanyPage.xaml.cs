﻿using Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using ViewModels.Abstractions;
using ViewModels.ViewModels;

namespace View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchCompanyPage.xaml
    /// </summary>
    public partial class SearchCompanyPage : Page
    {
        private readonly IServiceProvider serviceProvider;
        private readonly INavigationService navigationService;
        public SearchCompanyPage(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.navigationService = navigationService;

            var companyRepository = serviceProvider.GetRequiredService<ICompanyRepository>();

            DataContext = new SearchCompanyPageVM(companyRepository, navigationService);
        }
    }
}
