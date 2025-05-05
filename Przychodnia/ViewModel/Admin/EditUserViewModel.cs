using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Forms;

namespace Przychodnia.ViewModel.Admin;

public class EditUserViewModel : ViewModelBase
{
    private readonly IUserService _userService;

    private User _editebleUser;

    public UserFormData UserFormData = new();

    public User EditebleUser
    {
        get => _editebleUser;
        set => SetProperty(ref _editebleUser, value);
    }

    public EditUserViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Initialize(int id)
    {   
        var user = await _userService.GetByIdWithDetailsAsync(id);
        _editebleUser = user;
        LoadFromUser();
    }

    public void LoadFromUser()
    {
        UserFormData.FirstName = EditebleUser.FirstName;
        UserFormData.LastName = EditebleUser.LastName;
        UserFormData.Login = EditebleUser.Login;
        UserFormData.Password = EditebleUser.PasswordHash;
        UserFormData.LicenseNumber = EditebleUser.LicenseNumber;
        UserFormData.SelectedLaboratory = EditebleUser.Laboratory;
        UserFormData.SelectedUserType = EditebleUser.UserType;
        UserFormData.IsActive = EditebleUser.IsActive;
    }
}
