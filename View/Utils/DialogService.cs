using System.Windows;
using ViewModels.Abstractions;

namespace View.Utils
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Сообщение");
            }
        }
    }
}
