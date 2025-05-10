using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Navigation;
using Przychodnia.ViewModel.Shared;

namespace Przychodnia.ViewModel.Admin;
public class AdminPanelViewModel : NavigableBaseViewModel, INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public AdminPanelViewModel(IServiceProvider serviceProvider)
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
            (() => NavigateToAsync<LaboratoryListViewModel>());

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
    public ICommand NavigateBackCommand { get; }

    private async Task NavigateToAsync<TViewModel>(Func<TViewModel, Task> initializer = null)
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
        homePage.Caption = "Panel administratora";
        CurrentViewModel = homePage;
    }
}
