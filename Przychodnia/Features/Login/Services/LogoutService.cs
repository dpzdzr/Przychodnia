using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Login.Views;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.CurrentUserService;
using Przychodnia.Shared.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Przychodnia.Features.Login.Services;

public class LogoutService(IServiceProvider serviceProvider, ICurrentUserService currentUserService,
    NavigationServiceProxy navigationServiceProxy)
    : ILogoutService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly NavigationServiceProxy _navigationServiceProxy = navigationServiceProxy;

    public void Logout()
    {
        _currentUserService.Clear();
        _navigationServiceProxy.Clear();
        
        var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
        loginWindow.Show();
    }
}
