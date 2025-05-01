using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Przychodnia.ViewModel;

public class AdminPanelViewModel : NavigableViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    public ICommand NavigateToAddUserCommand { get; }
    public ICommand NavigateBackCommand { get; }

    public AdminPanelViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        NavigateToAddUserCommand = new RelayCommand(() =>
        {
            var vm = _serviceProvider.GetRequiredService<AddUserViewModel>();
            NavigateTo(vm);
        });

        NavigateBackCommand = new RelayCommand(NavigateBack);
    }
}
