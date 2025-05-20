using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Features.Login.Services;
using Przychodnia.Features.Panels.Admin.ViewModels;
using Przychodnia.Features.Panels.Admin.Views;
using Przychodnia.Shared.Services.CurrentUserService;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System.Security;
using System.Windows;
using System.Windows.Navigation;

namespace Przychodnia.Features.Login.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly NavigationServiceProxy _navigationServiceProxy;
    private readonly ILoginService _loginService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewModelFactory _viewModelFactory;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string? inputLogin;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private SecureString? inputPassword;


    public LoginViewModel(IDialogService dialogService, NavigationServiceProxy navigationServiceProxy, 
        ILoginService loginService, IServiceProvider serviceProvider, IViewModelFactory viewModelFactory, 
        IMessenger messenger)
        : base(dialogService, messenger)
    {
        _navigationServiceProxy = navigationServiceProxy;
        _loginService = loginService;
        _serviceProvider = serviceProvider;
        _viewModelFactory = viewModelFactory;

        LoginCommand = new AsyncRelayCommand(TryLogin, () => CanLogin);
        _viewModelFactory = viewModelFactory;
    }

    public event Action<int>? LoginSucceeded;
    public IAsyncRelayCommand LoginCommand { get; }
    public SecureString? Password { private get; set; }

    public bool CanLogin => IsLoginNotEmpty && IsPasswordNotEmpty;

    private async Task TryLogin()
    {
        if (await _loginService.Authenticate(InputLogin!, InputPassword!))
        {
            var currentUser = _serviceProvider.GetRequiredService<ICurrentUserService>().GetUser();
            LoginSucceeded?.Invoke(currentUser.UserTypeId);
            DisposePassword();  
        }
        else
            ShowError("Niepoprawne dane logowania lub użytkownik nieaktywny.");
       
    }
    private bool IsPasswordNotEmpty => InputPassword is not null && InputPassword.Length > 0;
    private bool IsLoginNotEmpty => !string.IsNullOrEmpty(InputLogin);
    private void DisposePassword()
    {
        InputPassword?.Dispose();
        InputPassword = null;
    }
}
