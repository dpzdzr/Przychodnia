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
    public IAsyncRelayCommand NavigateToUsersListCommand { get; }
    public IAsyncRelayCommand NavigateToPostalCodesListCommand { get; }
    public IAsyncRelayCommand NavigateToPatientsListCommand { get; }
    public ICommand NavigateBackCommand { get; }

    public AdminPanelViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        CurrentViewModel = _serviceProvider.GetRequiredService<AdminPanelHomePageViewModel>();

        NavigateToUsersListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<UsersListViewModel>(vm => vm.InitializeAsync()));

        NavigateToPostalCodesListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<PostalCodesListViewModel>(vm => vm.InitializeAsync()));

        NavigateToPatientsListCommand = new AsyncRelayCommand
            (() => NavigateToAsync<PatientsListViewModel>(vm => vm.InitializeAsync()));

        NavigateBackCommand = new RelayCommand(NavigateBack);
    }

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

    public void NavigateTo(BaseViewModel viewModel) => base.NavigateTo(viewModel);
    public void NavigateBack()
    {
        base.NavigateBack();
        _ = CurrentViewModel?.OnNavigatedBack();
    }
}
