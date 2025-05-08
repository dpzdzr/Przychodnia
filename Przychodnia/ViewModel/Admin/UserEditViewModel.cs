using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Admin;

public class UserEditViewModel : UserFormBaseViewModel<UserEditFormData>
{
    private readonly IUserService _userService;

    private User _editableUser;

    public static string HeaderText => "Edytuj użytkownika";
    public static string ActionButtonText => "Edytuj";
        
    public ICommand SaveUserCommand { get; }
    public User EditableUser
    {
        get => _editableUser;
        set => SetProperty(ref _editableUser, value);
    }

    public UserEditViewModel(IDialogService dialogService, ILaboratoryService labService, IUserTypeService userTypeService, IUserService userService) : base(userTypeService, labService, dialogService)
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(EditUserAsync);
    }

    public async Task InitializeAsync(int id)
    {
        var user = await _userService.GetByIdWithDetailsAsync(id);
        EditableUser = user;

        await base.InitializeFormDataAsync();

        FormData.LoadFromUser(user);
    }

    private async Task EditUserAsync()
    {
        FormData.LoadToUser(EditableUser);
        await _userService.SaveChangesAsync();
        _dialogService.Show("Sukces", "Pomyślnie edytowano użytkownika");
    }
}
