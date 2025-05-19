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
using System.Windows.Input;

namespace Przychodnia.Features.Panels.Admin.ViewModels;
public class AdminPanelViewModel : BaseNavigableViewModel, INavigationService
{
    public AdminPanelViewModel(IServiceProvider serviceProvider, IDialogService dialogService, 
        ILogoutService logoutService, IMessenger messenger)
        : base(dialogService, serviceProvider, "Panel administratora", logoutService, messenger)
    {
        InitializeHomePage();

        NavigateToUsersListCommand = CreateNavigationCommand<UserListViewModel>();
        NavigateToPostalCodesListCommand = CreateNavigationCommand<PostalCodeListViewModel>();
        NavigateToPatientsListCommand = CreateNavigationCommand<PatientListViewModel>();
        NavigateToLaboratoriesListCommand = CreateNavigationCommand<LaboratoryListViewModel>();
        NavigateToAppointmentsListCommand = CreateNavigationCommand<AppointmentListViewModel>();
    }

    public IAsyncRelayCommand NavigateToUsersListCommand { get; }
    public IAsyncRelayCommand NavigateToPostalCodesListCommand { get; }
    public IAsyncRelayCommand NavigateToPatientsListCommand { get; }
    public IAsyncRelayCommand NavigateToLaboratoriesListCommand { get; }
    public IAsyncRelayCommand NavigateToAppointmentsListCommand { get; }
}
