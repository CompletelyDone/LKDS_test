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

            LoadEmployeesAsync().ConfigureAwait(false);

            SearchCommand = new RelayCommand<object>(async _ => await SearchEmployeeAsync());
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand<Employee>(EditEmployeeAsync);
            DeleteEmployeeCommand = new RelayCommand<Employee>(DeleteEmployeeAsync);
        }
        public ICommand SearchCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        private string searchText = string.Empty;
        public string SearchText
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
            await Task.Run(async () =>
            {
                var employees = await employeeRepository.GetAllAsync();
                Employees = new ObservableCollection<Employee>(employees);
            });
        }
        private async Task SearchEmployeeAsync()
        {
            if(!string.IsNullOrEmpty(SearchText))
            {
                var employees = await employeeRepository.SearchEmployeeByFieldAsync(searchText);
                Employees = new ObservableCollection<Employee>(employees);
            }
        }
        private void AddEmployee()
        {
            navigationService.NavigateTo<EmployeePageVM>();
        }
        private async Task EditEmployeeAsync(Employee employee)
        {
            if (await employeeRepository.GetByIdAsync(employee.Id) != null)
            {
                navigationService.NavigateTo<EmployeePageVM>(employee);
            }
        }
        private async Task DeleteEmployeeAsync(Employee employee)
        {
            if(employee != null)
            {
                await employeeRepository.DeleteAsync(employee.Id);
                await LoadEmployeesAsync();
            }
        }
    }
}
