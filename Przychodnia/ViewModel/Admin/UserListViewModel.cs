using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Admin;

public class UserListViewModel : BaseViewModel
{
    private readonly IDialogService _dialogService;
    private readonly IUserService _userService;
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;
    
    private UserWrapper _selectedUser;
    private ObservableCollection<UserWrapper> _users;

    public UserListViewModel(IDialogService dialogService, IUserService userService,
    INavigationService navigationService, IServiceProvider serviceProvider)
    {
        _dialogService = dialogService;
        _userService = userService;
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;

        DeleteUserCommand = new AsyncRelayCommand(RemoveUser, () => SelectedUser != null);
        EditUserCommand = new AsyncRelayCommand(EditUser, () => SelectedUser != null);
        AddUserCommand = new AsyncRelayCommand(AddUser);

        WeakReferenceMessenger.Default.Register<UserAddedMessage>(this, (r, m) =>
        {
            Users.Add(new UserWrapper(m.Value));
        });
    }

    public ObservableCollection<UserWrapper> Users
    {
        get => _users;
        set => SetProperty(ref _users, value);
    }
    public UserWrapper SelectedUser
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

    public IAsyncRelayCommand DeleteUserCommand { get; }
    public IAsyncRelayCommand EditUserCommand { get; }
    public IAsyncRelayCommand AddUserCommand { get; }

    public async Task InitializeAsync()
    {
        var items = await _userService.GetAllWithUserTypeAsync();
        Users = [.. items.Select(u => new UserWrapper(u))];
    }

    private async Task AddUser()
    {   
        UserWrapper newUserWrapper = new(new User());
        Users.Add(newUserWrapper);

        var addVm = _serviceProvider.GetRequiredService<UserAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }
    private async Task EditUser()
    {
        var editVm = _serviceProvider.GetRequiredService<UserEditViewModel>();
        await editVm.InitializeAsync(SelectedUser);
        _navigationService.NavigateTo(editVm);
    }
    private async Task RemoveUser()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
        {
            await _userService.RemoveAsync(SelectedUser.Id);
            Users.Remove(SelectedUser);
        }
    }
}
