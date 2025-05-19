using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels;
using Przychodnia.Features.Entities.LaboratoryFeature.ViewModels;
using Przychodnia.Features.Entities.PatientFeature.ViewModels;
using Przychodnia.Features.Entities.PostalCodeFeature.ViewModels;
using Przychodnia.Features.Entities.UserFeature.ViewModels;
using Przychodnia.Features.HomePage.ViewModels;
using Przychodnia.Features.Login.Services;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Panels.Receptionist.ViewModels;

public class ReceptionistPanelViewModel : BaseNavigableViewModel, INavigationService
{
    public ReceptionistPanelViewModel(IServiceProvider serviceProvider, IDialogService dialogService,
        ILogoutService logoutService, IMessenger messenger)
        : base(dialogService, serviceProvider, "Panel rejestratora", logoutService, messenger)
    {
        InitializeHomePage();

        NavigateToPostalCodesListCommand = CreateNavigationCommand<PostalCodeListViewModel>();
        NavigateToPatientsListCommand = CreateNavigationCommand<PatientListViewModel>();
        NavigateToAppointmentsListCommand = CreateNavigationCommand<AppointmentListViewModel>();
    }

    public IAsyncRelayCommand NavigateToPostalCodesListCommand { get; }
    public IAsyncRelayCommand NavigateToPatientsListCommand { get; }
    public IAsyncRelayCommand NavigateToAppointmentsListCommand { get; }
}
