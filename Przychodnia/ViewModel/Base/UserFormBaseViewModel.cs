using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Base;

public abstract class UserFormBaseViewModel<TForm> : BaseViewModel
    where TForm : UserFormDataBase, new()
{
    private readonly ILaboratoryService _labService;
    private readonly IUserTypeService _userTypeService;
    protected readonly IDialogService _dialogService;
    public TForm FormData { get; } = new();

    private ObservableCollection<UserType> _userTypes;
    private ObservableCollection<Laboratory> _laboratories;

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

    protected UserFormBaseViewModel(IUserTypeService userTypeService, ILaboratoryService laboratoryService, IDialogService dialogService)
    {
        _userTypeService = userTypeService;
        _labService = laboratoryService;    
        _dialogService = dialogService;
        FormData.PropertyChanged += OnFormDataPropertyChanged;
    }

    private void OnFormDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(FormData.SelectedUserType))
        {
            OnPropertyChanged(nameof(IsDoctor));
            OnPropertyChanged(nameof(IsLabTechnician));
            OnPropertyChanged(nameof(IsDoctorOrLabTechnician));
        }
    }

    public async Task InitializeFormDataAsync()
    {
        UserTypes = [.. await _userTypeService.GetAllAsync()];
        Laboratories = [.. await _labService.GetAllAsync()];

        if(UserTypes.Any())
            FormData.SelectedUserType = UserTypes.First();
    }
}
