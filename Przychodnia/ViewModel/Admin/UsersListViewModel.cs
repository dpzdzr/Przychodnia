using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Admin;

public class UsersListViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IUserService _userService;
    private User _selectedUser;
    private ObservableCollection<User> _users;

    public IAsyncRelayCommand DeleteUserCommand { get; }
    public IAsyncRelayCommand EditUserCommand { get; }
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

    public UsersListViewModel(IDialogService dialogService, IUserService userService)
    {
        _dialogService = dialogService;
        _userService = userService;

        DeleteUserCommand = new AsyncRelayCommand(RemoveUser, () => SelectedUser != null);
        EditUserCommand = new AsyncRelayCommand(EditUser, () => SelectedUser != null);
    }

    private async Task RemoveUser()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
        {
            await _userService.RemoveUserAsync(SelectedUser);
            Users.Remove(SelectedUser);
        }
    }

    private async Task EditUser()
    {

    }

    public async Task InitializeAsync()
    {
        var users = await _userService.GetAllWithUserTypeAsync();
        Users = [.. users];
    }
}
