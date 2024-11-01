using Model;
using System.Windows.Controls;
using View.Pages;
using ViewModels.Abstractions;
using ViewModels.Base;
using ViewModels.ViewModels;

namespace View.Utils
{
    public class Navigator : INavigationService
    {
        private readonly Frame navigationFrame;
        private readonly IServiceProvider serviceProvider;
        public Navigator(IServiceProvider serviceProvider, Frame frame)
        {
            this.serviceProvider = serviceProvider;
            navigationFrame = frame;
        }
        public void GoBack()
        {
            if (navigationFrame.CanGoBack)
            {
                navigationFrame.GoBack();
            }
        }

        public void NavigateTo<T>() where T : ViewModelBase
        {
            Page page = GetPageFromViewModel(typeof(T));
            if (page != null)
            {
                navigationFrame.Navigate(page);
            }
        }

        public void NavigateTo<T>(object parameter) where T : ViewModelBase
        {
            Page page = GetPageFromViewModel(typeof(T), parameter);
            if (page != null)
            {
                navigationFrame.Navigate(page);
            }
        }
        private Page GetPageFromViewModel(Type viewModelType, object parameter = null)
        {
            switch (viewModelType.Name)
            {
                case nameof(CompanyPageVM):
                    return new CompanyPage(serviceProvider, this, (Company)parameter);
                case nameof(EmployeePageVM):
                    return new EmployeePage(serviceProvider, this, (Employee)parameter);
                case nameof(SearchCompanyPageVM):
                    return new SearchCompanyPage(serviceProvider, this);
                case nameof(SearchEmployeePageVM):
                    return new SearchEmployeePage(serviceProvider, this);
                default:
                    throw new ArgumentException("Неизвестный ViewModel", nameof(viewModelType));
            }
        }
    }
}
