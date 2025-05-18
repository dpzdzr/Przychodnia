using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels;
using Przychodnia.Features.Entities.LaboratoryFeature.ViewModels;
using Przychodnia.Features.Entities.PatientFeature.ViewModels;
using Przychodnia.Features.Entities.PostalCodeFeature.ViewModels;
using Przychodnia.Features.Entities.UserFeature.ViewModels;
using Przychodnia.Features.HomePage.ViewModels;
using Przychodnia.Shared.Services;
using Przychodnia.Shared.ViewModels;
using System.Windows.Input;

namespace Przychodnia.Features.Panels.Admin.ViewModels;
public class AdminPanelViewModel : BaseNavigableViewModel, INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string _homePageCaption = "Panel administratora";

    public AdminPanelViewModel(IServiceProvider serviceProvider, IDialogService dialogService)
        : base(dialogService)
    {
        _serviceProvider = serviceProvider;
        InitializeHomePage();

        NavigateToUsersListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<UserListViewModel>(vm => vm.InitializeAsync()));

        NavigateToPostalCodesListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<PostalCodeListViewModel>(vm => vm.InitializeAsync()));

        NavigateToPatientsListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<PatientListViewModel>(vm => vm.InitializeAsync()));

        NavigateToLaboratoriesListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<LaboratoryListViewModel>(vm => vm.InitializeAsync()));

        NavigateToAppointmentsListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<AppointmentListViewModel>(vm => vm.InitializeAsync()));

        NavigateBackCommand = new RelayCommand(NavigateBack);
    }

    public new void NavigateTo(BaseViewModel viewModel)
        => base.NavigateTo(viewModel);
    public new void NavigateBack()
        => base.NavigateBack();

    public IAsyncRelayCommand NavigateToUsersListCommand { get; }
    public IAsyncRelayCommand NavigateToPostalCodesListCommand { get; }
    public IAsyncRelayCommand NavigateToPatientsListCommand { get; }
    public IAsyncRelayCommand NavigateToLaboratoriesListCommand { get; }
    public IAsyncRelayCommand NavigateToAppointmentsListCommand { get; }
    public ICommand NavigateBackCommand { get; }

    private async Task NavigateToAsync<TViewModel>(Func<TViewModel, Task>? initializer = null)
        where TViewModel : BaseViewModel
    {
        if (CurrentViewModel is TViewModel)
            return;

        IsBusy = true;
        try
        {
            var vm = _serviceProvider.GetRequiredService<TViewModel>();
            if (initializer != null)
                await initializer(vm);
            NavigateTo(vm);
        }
        finally
        {
            IsBusy = false;
        }
    }
    private void InitializeHomePage()
    {
        var homePage = _serviceProvider.GetRequiredService<HomePageViewModel>();
        homePage.Caption = _homePageCaption;
        CurrentViewModel = homePage;
    }
}
