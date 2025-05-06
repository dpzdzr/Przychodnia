using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Interface;

namespace Przychodnia.ViewModel.Admin;

public class UsersListViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IUserService _userService;
    private readonly IAdminNavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    private User _selectedUser;
    private ObservableCollection<User> _users;

    public IAsyncRelayCommand DeleteUserCommand { get; }
    public IAsyncRelayCommand EditUserCommand { get; }
    public IAsyncRelayCommand AddUserCommand { get; }
    public ObservableCollection<User> Users
    {
        get => _users;
        set => SetProperty(ref _users, value);
    }

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            if (SetProperty(ref _selectedUser, value))
            {
                (DeleteUserCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
                (EditUserCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
            }
        }
    }

    public UsersListViewModel(IDialogService dialogService, IUserService userService,
        IAdminNavigationService navigationService, IServiceProvider serviceProvider)
    {
        _dialogService = dialogService;
        _userService = userService;
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        DeleteUserCommand = new AsyncRelayCommand(RemoveUser, () => SelectedUser != null);
        EditUserCommand = new AsyncRelayCommand(EditUser, () => SelectedUser != null);
        AddUserCommand = new AsyncRelayCommand(AddUser);
    }

    private async Task RemoveUser()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
        {
            await _userService.RemoveAsync(SelectedUser);
            Users.Remove(SelectedUser);
        }
    }

    private async Task EditUser()
    {
        var editVm = _serviceProvider.GetRequiredService<UserEditViewModel>();
        await editVm.InitializeAsync(SelectedUser.Id);
        _navigationService.NavigateTo(editVm);
    }

    private async Task AddUser()
    {
        var addVm = _serviceProvider.GetRequiredService<UserAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }

    public async Task InitializeAsync()
    {
        var users = await _userService.GetAllWithUserTypeAsync();
        Users = [.. users];
    }

    public override async Task OnNavigatedBack()
    {
       await InitializeAsync();
    }
}
