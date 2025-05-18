namespace Przychodnia.Shared.Services.DialogService;

public interface IDialogService
{
    bool Confirm(string title, string message);
    void Show(string title, string message);

    void Error(string title, string message);
}
