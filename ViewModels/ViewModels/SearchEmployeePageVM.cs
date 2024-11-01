using Data.Abstractions;
using Model;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Employee> employees = [];
        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }
        private async Task LoadEmployeesAsync()
        {
            var employees = await employeeRepository.GetAllAsync();
            Employees = new ObservableCollection<Employee>(employees);
        }
        private void AddEmployeeAsync()
        {
            navigationService.NavigateTo<EmployeePageVM>();
        }
    }
}
