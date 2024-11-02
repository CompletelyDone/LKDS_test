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
        private readonly INavigationService navigationService;
        private Employee currentEmployee;
        public EmployeePageVM(
            IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository,
            IDialogService dialogService,
            INavigationService navigationService,
            Employee? employee)
        {
            this.employeeRepository = employeeRepository;
            this.companyRepository = companyRepository;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            currentEmployee = employee ?? new Employee(Guid.Empty, string.Empty, string.Empty, string.Empty, null, null);

            FirstName = currentEmployee.FirstName;
            LastName = currentEmployee.LastName;
            Patronymic = currentEmployee.Patronymic;
            PhotoPath = currentEmployee.PhotoPath;
            SelectedCompany = currentEmployee.Company;

            LoadCompaniesAsync().ConfigureAwait(false);

            SaveCommand = new RelayCommand(async () => await SaveAsync(), CanSave);
            DeleteCommand = new RelayCommand(async () => await DeleteAsync(), CanDelete);
            CancelCommand = new RelayCommand(Cancel);
        }
        public ObservableCollection<Company> Companies { get; } = [];
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
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
        private string photoPath = "../Assets/defaultPerson.jpg";
        public string PhotoPath
        {
            get => photoPath;
            set
            {
                photoPath = value;
                OnPropertyChanged();
            }
        }
        private Company? selectedCompany;
        public Company? SelectedCompany
        {
            get => selectedCompany;
            set
            {
                if (selectedCompany != value)
                {
                    selectedCompany = value;
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
        private void Cancel()
        {
            navigationService.NavigateTo<MenuPageVM>();
        }
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName);
        }
        private bool CanDelete() => currentEmployee != null && currentEmployee.Id != Guid.Empty;
        private async Task SaveAsync()
        {
            try
            {
                currentEmployee.FirstName = FirstName;
                currentEmployee.LastName = LastName;
                currentEmployee.Patronymic = Patronymic;
                currentEmployee.Company = SelectedCompany;
                currentEmployee.PhotoPath = PhotoPath;

                if (currentEmployee.Id == Guid.Empty)
                {
                    currentEmployee.Id = Guid.NewGuid();
                    await employeeRepository.CreateAsync(currentEmployee);
                    dialogService.ShowMessage("Сотрудник успешно создан!");
                    navigationService.NavigateTo<MenuPageVM>();
                }
                else
                {
                    await employeeRepository.UpdateAsync(currentEmployee);
                    dialogService.ShowMessage("Сотрудник успешно сохранен!");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }
        private async Task DeleteAsync()
        {
            if (currentEmployee.Id != Guid.Empty)
            {
                try
                {
                    await employeeRepository.DeleteAsync(currentEmployee.Id);
                    dialogService.ShowMessage("Сотрудник успешно удален!");
                }
                catch (Exception ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }
        }
    }
}
