using Data.Abstractions;
using Model;
using System.Windows.Input;
using ViewModels.Abstractions;
using ViewModels.Base;

namespace ViewModels.ViewModels
{
    public class CompanyPageVM : ViewModelBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IDialogService dialogService;
        private readonly INavigationService navigationService;
        private Company currentCompany;
        public CompanyPageVM
            (ICompanyRepository companyRepository, IDialogService dialogService, INavigationService navigationService, Company? company)
        {
            this.companyRepository = companyRepository;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            currentCompany = company ?? new Company(Guid.Empty, string.Empty);
            Title = currentCompany.Title;

            SaveCommand = new RelayCommand(async () => await SaveAsync(), CanSave);
            DeleteCommand = new RelayCommand(async () => await DeleteAsync(), CanDelete);
            CancelCommand = new RelayCommand(Cancel);
        }
        private string title = string.Empty;
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        private bool CanSave() => !string.IsNullOrWhiteSpace(Title);
        private bool CanDelete() => currentCompany != null && currentCompany.Id != Guid.Empty;
        private void Cancel()
        {
            navigationService.NavigateTo<MenuPageVM>();
        }
        private async Task SaveAsync()
        {
            currentCompany.Title = Title;
            var isValid = Company.IsValid(currentCompany);
            if (isValid != string.Empty)
            {
                dialogService.ShowMessage(isValid);
                return;
            }
            try
            {
                if (currentCompany.Id == Guid.Empty)
                {
                    currentCompany.Id = Guid.NewGuid();
                    await companyRepository.CreateAsync(currentCompany);
                    dialogService.ShowMessage("Компания успешно создана!");
                    navigationService.NavigateTo<MenuPageVM>();
                }
                else
                {
                    await companyRepository.UpdateAsync(currentCompany);
                    dialogService.ShowMessage("Компания успешно изменена!");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage($"Ошибка при сохранении компании: {ex.Message}");
            }
        }
        private async Task DeleteAsync()
        {
            if (currentCompany != null)
            {
                try
                {
                    await companyRepository.DeleteAsync(currentCompany.Id);
                    dialogService.ShowMessage("Компания успешно удалена!");
                }
                catch (Exception ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }
        }
    }
}
