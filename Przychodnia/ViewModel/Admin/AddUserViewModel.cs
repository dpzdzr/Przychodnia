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
using Przychodnia.ViewModel.Forms;

namespace Przychodnia.ViewModel.Admin;

public class AddUserViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly ILaboratoryService _labService;
    private readonly IUserTypeService _userTypeService;
    private readonly IUserService _userService;

    private ObservableCollection<UserType> _userTypes;
    private ObservableCollection<Laboratory> _laboratories;

    public UserFormData FormData { get; } = new();

    public ObservableCollection<UserType> UserTypes
    {
        get => _userTypes;
        set => SetProperty(ref _userTypes, value);
    }

    public ObservableCollection<Laboratory> Laboratories
    {
        get => _laboratories;
        set => SetProperty(ref _laboratories, value);
    }

    public bool IsDoctor 
        => FormData.SelectedUserType?.IsDoctor == true;
    public bool IsLabTechnician 
        => FormData.SelectedUserType?.IsLabTechnician == true; 
    public bool IsDoctorOrLabTechnician 
        => FormData.SelectedUserType?.IsDoctorOrLabTechnician == true;

    public ICommand SaveUserCommand { get; }
    public AddUserViewModel(IUserService userService, IUserTypeService userTypeService, ILaboratoryService labService, IDialogService dialogService)
    {
        _userService = userService;
        _userTypeService = userTypeService;
        _labService = labService;
        _dialogService = dialogService;

        FormData.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(FormData.SelectedUserType))
            {
                OnPropertyChanged(nameof(IsDoctor));
                OnPropertyChanged(nameof(IsLabTechnician));
                OnPropertyChanged(nameof(IsDoctorOrLabTechnician));
            }
        };

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
        var userTypes = await _userTypeService.GetAllAsync();
        UserTypes = [.. userTypes];

        var labs = await _labService.GetAllAsync();
        Laboratories = [.. labs];
    }

    private void ClearForm()
    {   
        FormData.FirstName = string.Empty; FormData.LastName = string.Empty; FormData.Login = string.Empty;
        FormData.Password = string.Empty; FormData.LicenseNumber = string.Empty; FormData.IsActive = false;
        FormData.SelectedUserType = null; FormData.SelectedLaboratory = null;
    }

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
