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

public partial class UserListViewModel : BaseListViewModel<UserWrapper>
{
    private readonly IUserService _userService;

    public UserListViewModel(IDialogService dialogService, IUserService userService,
    INavigationService navigationService, IServiceProvider serviceProvider)
        : base(dialogService, navigationService, serviceProvider)
    {
        _userService = userService;

        WeakReferenceMessenger.Default.Register<UserAddedMessage>(this, (r, m) =>
        {
            Items.Add(new UserWrapper(m.Value));
        });
    }

    public override async Task InitializeAsync()
    {
        var items = await _userService.GetAllWithDetailsAsync();
        Items = [.. items.Select(u => new UserWrapper(u))];
    }

    protected override async Task Add()
    {
        var addVm = _serviceProvider.GetRequiredService<UserAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }
    protected override async Task Edit()
    {
        var editVm = _serviceProvider.GetRequiredService<UserEditViewModel>();
        await editVm.InitializeAsync(SelectedItem);
        _navigationService.NavigateTo(editVm);
    }
    protected override async Task Remove()
    {
        await TryExecuteAsync(async () =>
        {
            if (SelectedItem?.Id is int userId &&
                Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
            {
                await _userService.RemoveAsync(userId);
                Items.Remove(SelectedItem);
            }
        });
    }
}
