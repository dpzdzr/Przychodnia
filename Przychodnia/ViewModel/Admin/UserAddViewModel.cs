using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Admin;

public class UserAddViewModel : UserFormBaseViewModel<UserAddFormData>
{
    private readonly IUserService _userService;

    public static string HeaderText => "Dodaj użytkownika";
    public static string ActionButtonText => "Dodaj";
    public ICommand SaveUserCommand { get; }
    public UserAddViewModel(IUserService userService, IUserTypeService userTypeService, ILaboratoryService labService, IDialogService dialogService)
        : base(userTypeService, labService, dialogService) 
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(AddUserAsync);
    }

    private async Task AddUserAsync()
    {
        try
        {
            await _userService.CreateUserAsync(CreateUserInputDTOFromForm());
            ClearForm();
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego użytkownika");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.Message}");
        }
    }

    public async Task InitializeAsync()
    {
        await base.InitializeFormDataAsync();
    }

    private void ClearForm() => FormData.ClearForm();

    private UserInputDTO CreateUserInputDTOFromForm()
    {
        return new UserInputDTO
        {
            FirstName = FormData.FirstName,
            LastName = FormData.LastName,
            Login = FormData.Login,
            PasswordHash = FormData.Password,
            UserType = FormData.SelectedUserType,
            LicenseNumber = FormData.LicenseNumber,
            Laboratory = FormData.SelectedLaboratory,
            IsActive = FormData.IsActive
        };
    }
}
