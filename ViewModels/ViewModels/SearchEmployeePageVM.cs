using Data.Abstractions;
using System.Windows.Input;
using ViewModels.Abstractions;
using ViewModels.Base;

namespace ViewModels.ViewModels
{
    public class SearchEmployeePageVM : ViewModelBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly INavigationService navigationService;

        public SearchEmployeePageVM(IEmployeeRepository employeeRepository, INavigationService navigationService)
        {
            this.employeeRepository = employeeRepository;
            this.navigationService = navigationService;

            AddEmployeeCommand = new RelayCommand(AddEmployeeAsync);
        }
        public ICommand SearchCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        private string searchText;
        private string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }
        private void AddEmployeeAsync()
        {
            navigationService.NavigateTo<EmployeePageVM>();
        }
    }
}
