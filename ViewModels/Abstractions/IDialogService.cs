namespace ViewModels.Abstractions
{
    public interface IDialogService
    {
        void ShowMessage(string message);
        string? GetPhotoPath();
    }
}
