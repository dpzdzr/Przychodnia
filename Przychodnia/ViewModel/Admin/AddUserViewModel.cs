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

namespace Przychodnia.ViewModel.Admin;

public class AddUserViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly ILaboratoryService _labService;
    private readonly IUserTypeService _userTypeService;
    private readonly IUserService _userService;

    private ObservableCollection<UserType> _userTypes;
    private ObservableCollection<Laboratory> _laboratories;
    private UserType _selectedUserType;
    private Laboratory _selectedLaboratory;

    private string _firstName;
    private string _lastName;
    private string _login;
    private string _password;
    private string _licenseNumber;
    private bool _isActive;

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }
    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set => SetProperty(ref _licenseNumber, value);
    }

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }

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
    public UserType SelectedUserType
    {
        get => _selectedUserType;
        set
        {
            if (SetProperty(ref _selectedUserType, value))
            {
                OnPropertyChanged(nameof(IsDoctor));
                OnPropertyChanged(nameof(IsDoctorOrLabTechnician));
                OnPropertyChanged(nameof(IsLabTechnician));
            }
        }
    }

    public Laboratory SelectedLaboratory
    {
        get => _selectedLaboratory;
        set => SetProperty(ref _selectedLaboratory, value);
    }

    public bool IsDoctor => SelectedUserType?.Type == UserTypeEnum.Lekarz;
    public bool IsLabTechnician => SelectedUserType?.Type == UserTypeEnum.Laborant || SelectedUserType?.Type == UserTypeEnum.KierownikLaboratorium;

    public bool IsDoctorOrLabTechnician => IsDoctor || IsLabTechnician;

    public ICommand SaveUserCommand { get; }
    public AddUserViewModel(IUserService userService, IUserTypeService userTypeService, ILaboratoryService labService, IDialogService dialogService)
    {
        _userService = userService;
        _userTypeService = userTypeService;
        _labService = labService;
        _dialogService = dialogService;

        SaveUserCommand = new AsyncRelayCommand(AddUserAsync);
    }

    private async Task AddUserAsync()
    {
        try
        {
            var newUser = new UserInputDTO
            {
                FirstName = FirstName,
                LastName = LastName,
                Login = Login,
                PasswordHash = Password,
                UserType = SelectedUserType,
                LicenseNumber = LicenseNumber,
                Laboratory = SelectedLaboratory,
                IsActive = IsActive
            };
            await _userService.CreateUserAsync(newUser);
            ClearForm();
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego użytkownika");

        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}");
        }
    }

    public async Task InitializeAsync()
    {
        var userTypes = await _userTypeService.GetAllAsync();
        UserTypes = [.. userTypes];

        var labs = await _labService.GetAllAsync();
        Laboratories = [.. labs];
        Laboratories.Insert(0, new Laboratory { Name = "" });
    }

    private void ClearForm()
    {
        FirstName = string.Empty; LastName = string.Empty; Login = string.Empty;
        Password = string.Empty; LicenseNumber = string.Empty; IsActive = false;
    }
}
