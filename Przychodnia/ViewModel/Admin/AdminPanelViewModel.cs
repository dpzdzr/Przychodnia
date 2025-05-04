using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Admin;

public class AdminPanelViewModel : NavigableViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    public IAsyncRelayCommand NavigateToAddUserCommand { get; }
    public IAsyncRelayCommand NavigateToUsersListCommand { get; }
    public ICommand NavigateBackCommand { get; }

    public AdminPanelViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        CurrentViewModel = _serviceProvider.GetRequiredService<AdminPanelHomePageViewModel>();

        NavigateToAddUserCommand = new AsyncRelayCommand(async () =>
        {
            IsBusy = true;
            try
            {
                var vm = _serviceProvider.GetRequiredService<AddUserViewModel>();
                await vm.InitializeAsync();
                NavigateTo(vm);
            }
            finally
            {
                IsBusy = false;
            }
        });

        NavigateToUsersListCommand = new AsyncRelayCommand(async () =>
        {
            IsBusy = true;
            try
            {
                var vm = _serviceProvider.GetRequiredService<UsersListViewModel>();
                await vm.InitializeAsync();
                NavigateTo(vm);
            }
            finally
            {
                IsBusy = false;
            }
        });

        NavigateBackCommand = new RelayCommand(NavigateBack);
    }
}
