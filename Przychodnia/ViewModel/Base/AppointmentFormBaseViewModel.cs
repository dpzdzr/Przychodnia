using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Base;

public partial class AppointmentFormBaseViewModel<TForm> : BaseViewModel
    where TForm : AppointmentBaseFormData, new()
{
    private readonly IUserService _userService;
    private readonly IPatientService _patientService;

    [ObservableProperty] private ObservableCollection<UserWrapper> users = [];
    [ObservableProperty] private ObservableCollection<PatientWrapper> patients = [];

    public AppointmentFormBaseViewModel(IDialogService dialogService, IUserService userService, 
        IPatientService patientService) : base(dialogService)
    {
        _userService = userService;
        _patientService = patientService;
    }

    public TForm FormData { get; } = new();

    public async Task InitializeFormDataAsync()
    {
        var users = await _userService.GetAllWithDetailsAsync();
        Users = [.. users.Select(u => new UserWrapper(u)).ToList()];

        var patients = await _patientService.GetAllWithDetailsAsync();
        Patients = [.. patients.Select(u => new PatientWrapper(u)).ToList()];
    }
}
