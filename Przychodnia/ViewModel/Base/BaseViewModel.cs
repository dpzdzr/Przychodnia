using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Base;

public abstract class BaseViewModel(IDialogService dialogService) : ObservableObject
{
    protected readonly IDialogService _dialogService = dialogService;


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
}
