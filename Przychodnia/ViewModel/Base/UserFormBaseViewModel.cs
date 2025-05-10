using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Base;

public abstract partial class UserFormBaseViewModel<TForm> : BaseViewModel
    where TForm : UserFormDataBase, new()
{
    private readonly ILaboratoryService _labService;
    private readonly IUserTypeService _userTypeService;
    protected readonly IDialogService _dialogService;
    protected readonly IMapper _mapper;

    [ObservableProperty] private ObservableCollection<UserType> userTypes;
    [ObservableProperty] private ObservableCollection<Laboratory> laboratories;

    public UserFormBaseViewModel(IUserTypeService userTypeService, ILaboratoryService laboratoryService, IDialogService dialogService, IMapper mapper)
    {
        _mapper = mapper;
        _userTypeService = userTypeService;
        _labService = laboratoryService;    
        _dialogService = dialogService;
        FormData.PropertyChanged += OnFormDataPropertyChanged;
    }

    public TForm FormData { get; } = new();
    public bool IsDoctor
         => FormData.SelectedUserType?.IsDoctor == true;
    public bool IsLabTechnician
        => FormData.SelectedUserType?.IsLabTechnician == true;
    public bool HasLicenseNumber
        => FormData.SelectedUserType?.HasLicenseNumber == true;

    public async Task InitializeFormDataAsync()
    {
        UserTypes = [.. await _userTypeService.GetAllAsync()];
        Laboratories = [.. await _labService.GetAllAsync()];

        FormData.SelectedUserType = UserTypes.First();
    }

    private void OnFormDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(FormData.SelectedUserType))
        {
            OnPropertyChanged(nameof(IsDoctor));
            OnPropertyChanged(nameof(IsLabTechnician));
            OnPropertyChanged(nameof(HasLicenseNumber));
        }
    }
}
