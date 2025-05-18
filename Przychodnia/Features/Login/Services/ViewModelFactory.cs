using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Features.Panels.Admin.ViewModels;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Login.Services;

public class ViewModelFactory(IServiceProvider serviceProvider) : IViewModelFactory
{
    private readonly IServiceProvider _services = serviceProvider;
    public INavigationService CreateFor(int userTypeId)
    {
        return userTypeId switch
        {
            (int)UserTypeEnum.Admin => _services.GetRequiredService<AdminPanelViewModel>(),
            _ => throw new InvalidOperationException("Brak użytkownika takiego typu")
        };
    }

}
