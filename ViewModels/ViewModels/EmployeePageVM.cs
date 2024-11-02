using Data.Abstractions;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.Abstractions;
using ViewModels.Base;

namespace ViewModels.ViewModels
{
    public class EmployeePageVM : ViewModelBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IDialogService dialogService;
        private Employee currentEmployee;
        public EmployeePageVM
            (IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IDialogService dialogService, Employee? employee)
        {
            this.employeeRepository = employeeRepository;
            this.companyRepository = companyRepository;
            this.dialogService = dialogService;

            currentEmployee = employee ?? new Employee(Guid.Empty, string.Empty, string.Empty, string.Empty, null, null);

            FirstName = currentEmployee.FirstName;
            LastName = currentEmployee.LastName;
            Patronymic = currentEmployee.Patronymic;
            SelectedCompanyId = currentEmployee.CompanyId;

            LoadCompaniesAsync().ConfigureAwait(false);

            SaveCommand = new RelayCommand(async () => await SaveAsync(), CanSave);
            DeleteCommand = new RelayCommand(async () => await DeleteAsync(), CanDelete);
        }
        public ObservableCollection<Company> Companies { get; } = [];
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        private string firstName = string.Empty;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string lastName = string.Empty;
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string patronymic = string.Empty;
        public string Patronymic
        {
            get => patronymic;
            set
            {
                if (patronymic != value)
                {
                    patronymic = value;
                    OnPropertyChanged();
                }
            }
        }
        private Guid? selectedCompanyId;
        public Guid? SelectedCompanyId
        {
            get => selectedCompanyId;
            set
            {
                if (selectedCompanyId != value)
                {
                    selectedCompanyId = value;
                    OnPropertyChanged();
                }
            }
        }
        private async Task LoadCompaniesAsync()
        {
            var companies = await companyRepository.GetAllAsync();
            Companies.Clear();
            foreach (var company in companies)
            {
                Companies.Add(company);
            }
        }
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName);
        }
        private bool CanDelete() => currentEmployee != null && currentEmployee.Id != Guid.Empty;        
        private async Task SaveAsync()
        {
            currentEmployee.FirstName = FirstName;
            currentEmployee.LastName = LastName;
            currentEmployee.Patronymic = Patronymic;
            currentEmployee.CompanyId = SelectedCompanyId;

            if (currentEmployee.Id == Guid.Empty)
            {
                await employeeRepository.CreateAsync(currentEmployee);
            }
            else
            {
                await employeeRepository.UpdateAsync(currentEmployee);
            }

            dialogService.ShowMessage("Сотрудник успешно сохранен!");
        }
        private async Task DeleteAsync()
        {
            if (currentEmployee.Id != Guid.Empty)
            {
                await employeeRepository.DeleteAsync(currentEmployee.Id);
                dialogService.ShowMessage("Сотрудник успешно удален!");
            }
        }
    }
}
