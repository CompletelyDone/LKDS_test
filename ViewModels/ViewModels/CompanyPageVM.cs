using Data.Abstractions;
using Model;
using System.ComponentModel;
using System.Windows.Input;
using ViewModels.Base;

namespace ViewModels.ViewModels
{
    public class CompanyPageVM : INotifyPropertyChanged
    {
        private readonly ICompanyRepository companyRepository;
        private Company? currentCompany;
        public event PropertyChangedEventHandler? PropertyChanged;
        public CompanyPageVM(ICompanyRepository companyRepository, Company? company)
        {
            this.companyRepository = companyRepository;
            currentCompany = company ?? new Company(Guid.NewGuid(), string.Empty);
            Title = currentCompany.Title;

            SaveCommand = new RelayCommand(async () => await SaveAsync(), () => !string.IsNullOrWhiteSpace(Title));
            DeleteCommand = new RelayCommand(async () => await DeleteAsync(), CanDelete);
        }
        private string title = string.Empty;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private bool CanDelete()
        {
            return currentCompany != null && currentCompany.Id != Guid.Empty;
        }

        private async Task SaveAsync()
        {
            currentCompany.Title = Title;
            if (currentCompany.Id == Guid.Empty)
            {
                await companyRepository.CreateAsync(currentCompany);
            }
            else
            {
                await companyRepository.UpdateAsync(currentCompany);
            }
        }

        private async Task DeleteAsync()
        {
            if (currentCompany != null)
            {
                await companyRepository.DeleteAsync(currentCompany.Id);
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
