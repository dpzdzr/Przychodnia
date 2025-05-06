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

public class EditUserViewModel : BaseUserFormViewModel<UserEditFormData>
{
    private readonly IUserService _userService;

    private User _editableUser;

    public string HeaderText => "Edytuj użytkownika";
    public string ActionButtonText => "Edytuj";

    public ICommand SaveUserCommand { get; }
    public User EditableUser
    {
        get => _editableUser;
        set => SetProperty(ref _editableUser, value);
    }

    public EditUserViewModel(IDialogService dialogService, ILaboratoryService labService, IUserTypeService userTypeService, IUserService userService) : base(userTypeService, labService, dialogService)
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(EditUserAsync);
    }

    public async Task InitializeAsync(int id)
    {
        var user = await _userService.GetByIdWithDetailsAsync(id);
        _editableUser = user;

        await base.InitializeFormDataAsync();

        LoadFromUser();
    }

    public void LoadFromUser()
    {
        FormData.Id = EditableUser.Id;
        FormData.FirstName = EditableUser.FirstName;
        FormData.LastName = EditableUser.LastName;
        FormData.Login = EditableUser.Login;
        FormData.Password = EditableUser.PasswordHash;
        FormData.LicenseNumber = EditableUser.LicenseNumber;
        FormData.SelectedLaboratory = EditableUser.Laboratory;
        FormData.SelectedUserType = EditableUser.UserType;
        FormData.IsActive = EditableUser.IsActive;
    }

    public void LoadToUser()
    {
        EditableUser.FirstName = FormData.FirstName;
        EditableUser.LastName = FormData.LastName;
        EditableUser.Login = FormData.Login;
        EditableUser.PasswordHash = FormData.Password;
        EditableUser.LicenseNumber = FormData.LicenseNumber;
        EditableUser.Laboratory = FormData.SelectedLaboratory;
        EditableUser.UserType = FormData.SelectedUserType;
        EditableUser.IsActive = FormData.IsActive;
    }

    private async Task EditUserAsync()
    {
        LoadToUser();
        await _userService.SaveChanges();
        _dialogService.Show("Sukces", "Pomyślnie edytowano użytkownika");
    }
}
