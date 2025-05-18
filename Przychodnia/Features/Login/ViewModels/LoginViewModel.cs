using CommunityToolkit.Mvvm.Input;
using Przychodnia.Shared.Services;
using Przychodnia.Shared.ViewModels;
using System.Security;

namespace Przychodnia.Features.Login.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public LoginViewModel(IDialogService dialogService, INavigationService navigationService)
        : base(dialogService)
    {
        _navigationService = navigationService;

        CloseCommand = new RelayCommand(OnCloseRequested);
    }

    public event Action? RequestClose;
    public IRelayCommand CloseCommand { get; }
    public SecureString? Password { private get; set; }

    private void OnCloseRequested()
    {
        RequestClose?.Invoke();
    }
}
