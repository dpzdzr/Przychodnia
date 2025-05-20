using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Features.Panels.Admin.ViewModels;
using Przychodnia.Features.Panels.Admin.Views;
using Przychodnia.Features.Panels.Doctor.ViewModels;
using Przychodnia.Features.Panels.Doctor.Views;
using Przychodnia.Features.Panels.Receptionist.ViewModels;
using Przychodnia.Features.Panels.Receptionist.Views;
using Przychodnia.Shared.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Przychodnia.Features.Login.Services;

public class PanelWindowFactory(IServiceProvider serviceProvider, IViewModelFactory viewModelFactory, NavigationServiceProxy proxy) 
    : IPanelWindowFactory
{
    private readonly IServiceProvider _services = serviceProvider;
    private readonly NavigationServiceProxy _proxy = proxy;

    public Window CreateFor(int userTypeId)
    {
        var vm = viewModelFactory.CreateFor(userTypeId);
        _proxy.Current = vm;

        return userTypeId switch
        {
            (int)UserTypeEnum.Admin => new AdminPanelWindow((AdminPanelViewModel)vm),
            (int)UserTypeEnum.Rejestrator => new ReceptionistPanelWindow((ReceptionistPanelViewModel)vm),
            (int)UserTypeEnum.Lekarz => new DoctorPanelWindow((DoctorPanelViewModel)vm),
            _ => throw new InvalidOperationException("Nie zaimplementowano panelu obsługi dla użytkownika tego typu.")
        };
    }
}
