using Data.Abstractions;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.Abstractions;
using ViewModels.Base;

namespace ViewModels.ViewModels
{
    public class SearchCompanyPageVM : ViewModelBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly INavigationService navigationService;
        public SearchCompanyPageVM(ICompanyRepository companyRepository, INavigationService navigationService)
        {
            this.companyRepository = companyRepository;
            this.navigationService = navigationService;
            LoadCompaniesAsync();
            SearchCommand = new RelayCommand<object>(async _ => await SearchAsync());
            AddCompanyCommand = new RelayCommand<object>(async _ => await AddCompanyAsync());
            EditCompanyCommand = new RelayCommand<Company>(async company => await EditCompanyAsync(company));
            DeleteCompanyCommand = new RelayCommand<Company>(async company => await DeleteCompanyAsync(company));
        }
        public ICommand SearchCommand { get; }
        public ICommand AddCompanyCommand { get; }
        public ICommand EditCompanyCommand { get; }
        public ICommand DeleteCompanyCommand { get; }
        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Company> companies;
        public ObservableCollection<Company> Companies
        {
            get => companies;
            set
            {
                companies = value;
                OnPropertyChanged();
            }
        }
        private async Task LoadCompaniesAsync()
        {
            var allCompanies = await companyRepository.GetAllAsync();
            Companies = new ObservableCollection<Company>(allCompanies);
        }
        private async Task AddCompanyAsync()
        {
            navigationService.NavigateTo<CompanyPageVM>();
        }
        private async Task EditCompanyAsync(Company company)
        {
            navigationService.NavigateTo<CompanyPageVM>(company);
        }
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadCompaniesAsync();
            }
            else
            {
                var allCompanies = await companyRepository.GetAllAsync();
                var filteredCompanies = allCompanies
                    .Where(c => c.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Companies = new ObservableCollection<Company>(filteredCompanies);
            }
        }
        private async Task DeleteCompanyAsync(Company company)
        {
            if (company != null)
            {
                await companyRepository.DeleteAsync(company.Id);
                await LoadCompaniesAsync();
            }
        }
    }
}
