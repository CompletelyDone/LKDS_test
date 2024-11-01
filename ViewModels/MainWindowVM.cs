using ViewModels.Services;

namespace ViewModels
{
    public class MainWindowVM
    {
        private readonly DataService dataService;
        public MainWindowVM(DataService dataService)
        {
            this.dataService = dataService;
        }
    }
}
