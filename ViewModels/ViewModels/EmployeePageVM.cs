using System.Windows.Input;
using ViewModels.Base;

namespace ViewModels.ViewModels
{
    public class EmployeePageVM : ViewModelBase
    {
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
    }
}
