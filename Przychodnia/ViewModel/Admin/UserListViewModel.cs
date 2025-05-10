using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
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

public partial class UserListViewModel : BaseViewModel
{
    private readonly IDialogService _dialogService;
    private readonly IUserService _userService;
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty] private UserWrapper selectedUser;
    [ObservableProperty] private ObservableCollection<UserWrapper> users;

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
        if(SelectedUser?.Id is int userId && 
            _dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
        {
            await _userService.RemoveAsync(userId);
            Users.Remove(SelectedUser);
        }
    }
    partial void OnSelectedUserChanged(UserWrapper value)
    {
        (DeleteUserCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
        (EditUserCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
    }
}
