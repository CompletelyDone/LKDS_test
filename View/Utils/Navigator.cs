using System.Windows.Controls;
using ViewModels.Abstractions;

namespace View.Utils
{
    public class Navigator : INavigationService
    {
        private readonly Frame navigationFrame;
        public Navigator(Frame frame)
        {
            navigationFrame = frame;
        }
        public void GoBack()
        {
            if (navigationFrame.CanGoBack)
            {
                navigationFrame.GoBack();
            }
        }
    }
}
