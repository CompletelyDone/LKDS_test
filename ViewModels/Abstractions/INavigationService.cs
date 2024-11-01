using ViewModels.Base;

namespace ViewModels.Abstractions
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo<T>() where T : ViewModelBase;
        void NavigateTo<T>(object parameter) where T : ViewModelBase;
    }
}
