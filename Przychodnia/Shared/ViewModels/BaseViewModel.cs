using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Shared.Services.DialogService;

namespace Przychodnia.Shared.ViewModels;

public abstract class BaseViewModel(IDialogService dialogService, IMessenger messenger) 
    : ObservableObject, IDisposable
{
    protected readonly IDialogService _dialogService = dialogService;
    protected readonly IMessenger _messenger = messenger;
    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    protected bool Confirm(string title, string message)
        => _dialogService.Confirm(title, message);
    protected void ShowSucces(string message)
        => _dialogService.Show("Sukces", message);
    protected void ShowError(Exception ex, string errorTitle = "Błąd")
    {
        string message = ex.Message;
        if (ex.InnerException is not null)
            message += $"\n{ex.InnerException.Message}";
        _dialogService.Error(errorTitle, $"{message}");
    }
    protected void ShowError(string message, string errorTitle = "Błąd")
    {
        _dialogService.Error(errorTitle, message);
    }
    protected async Task TryExecuteAsync(Func<Task> action, string errorTitle = "Błąd")
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            ShowError(ex, errorTitle);
        }
    }

    #region Dispose pattern
    private bool _disposed;
    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if(disposing)
        {
            _messenger.UnregisterAll(this);
        }
        _disposed = true;
    }

    ~BaseViewModel() => Dispose(false);
    #endregion
}
