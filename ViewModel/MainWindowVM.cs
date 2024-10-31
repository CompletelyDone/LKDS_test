using ViewModel.Services;

namespace ViewModel
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
