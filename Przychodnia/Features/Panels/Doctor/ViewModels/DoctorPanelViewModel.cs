using CommunityToolkit.Mvvm.Input;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels;
using Przychodnia.Features.Entities.PatientFeature.ViewModels;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Panels.Doctor.ViewModels;

public class DoctorPanelViewModel : BaseNavigableViewModel, INavigationService
{
    public DoctorPanelViewModel(IDialogService dialogService, IServiceProvider services)
        : base(dialogService, services, "Panel lekarza")
    {
        InitializeHomePage();

        NavigateToPatientsListCommand = CreateNavigationCommand<PatientListViewModel>();
        NavigateToAppointmentsListCommand = CreateNavigationCommand<AppointmentListViewModel>();
    }

    public IAsyncRelayCommand NavigateToAppointmentsListCommand { get; }
    public IAsyncRelayCommand NavigateToPatientsListCommand { get; }
}
