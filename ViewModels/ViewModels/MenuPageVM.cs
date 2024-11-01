using System.Windows.Input;
using ViewModels.Abstractions;
using ViewModels.Base;
using ViewModels.Services;

namespace ViewModels.ViewModels
{
    public class MenuPageVM : ViewModelBase
    {
        private readonly DataService dataService;
        private readonly IDialogService dialogService;
        public MenuPageVM(DataService dataService, IDialogService dialogService)
        {
            this.dataService = dataService;
            this.dialogService = dialogService;
            this.dialogService = dialogService;
            CreateRandomCommand = new RelayCommand(async () => await CreateRandomAsync());
            this.dataService = dataService;
        }
        public ICommand CreateRandomCommand { get; }
        private async Task CreateRandomAsync()
        {
            await Task.Run(async () =>
            {
                await dataService.CreateRandomAsync();
                dialogService.ShowMessage("Случайные компании и сотрудники успешно созданы!");
            });
        }
    }
}
