using Microsoft.Win32;
using System.Windows;
using ViewModels.Abstractions;

namespace View.Utils
{
    public class DialogService : IDialogService
    {
        public string? GetPhotoPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                Title = "Выберите фотографию сотрудника"
            };

            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }
        public void ShowMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Сообщение");
            }
        }
    }
}
