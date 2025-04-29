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
using Przychodnia.ViewModel.Model;

namespace Przychodnia.ViewModel;

internal class AdminPanelViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserTypeRepository _userTypeRepository;
    private readonly ILaboratoryRepository _laboratoryRepository;
    private readonly IUserCreationService _userCreationService;

    private ObservableCollection<UserType> _userTypes;
    private ObservableCollection<Laboratory> _laboratories;
    private UserType _selectedUserType;
    private Laboratory _selectedLaboratory;

    private string _firstName;
    private string _lastName;
    private string _login;
    private string _password;

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

    public bool IsDoctor => SelectedUserType?.Name == "Lekarz";
    public bool IsLabTechnician => SelectedUserType?.Name == "Laborant" || SelectedUserType?.Name == "Kierownik laboratorium";

    public bool IsDoctorOrLabTechnician => IsDoctor || IsLabTechnician;

    public ICommand SaveUserCommand;

    public AdminPanelViewModel(IUserRepository userRepository, IUserTypeRepository userTypeRepository, ILaboratoryRepository laboratoryRepository, IUserCreationService userCreationService)
    {
        _userRepository = userRepository;
        _userTypeRepository = userTypeRepository;
        _laboratoryRepository = laboratoryRepository;
        _userCreationService = userCreationService;

        UserTypes = [.. _userTypeRepository.GetAll()];
        Laboratories = [.. _laboratoryRepository.GetAll()];
        Laboratories.Insert(0, new Laboratory { Name = "" });

        SaveUserCommand = new RelayCommand(AddUser);
    }

    private void AddUser()
    {
        var userInputModel = new UserInputModel
        {

        };
        _userCreationService.CreateUser(userInputModel);
    }
}
