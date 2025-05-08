using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Shared;

public class PatientsListViewModel : BaseViewModel
{
    private readonly IPatientService _patientService;
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;

    private ObservableCollection<Patient> _patients;
    private Patient _selectedPatient;
    public ObservableCollection<Patient> Patients
    {
        get => _patients;
        set => SetProperty(ref _patients, value);
    }

    public Patient SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            if (SetProperty(ref _selectedPatient, value))
            {
                (EditPatientCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
                (RemovePatientCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
            }
        }
    }

    public ICommand AddPatientCommand { get; }
    public ICommand EditPatientCommand { get; }
    public ICommand RemovePatientCommand { get; }

    public PatientsListViewModel(IPatientService patientService, INavigationService navigationService, IServiceProvider serviceProvider, IDialogService dialogService)
    {
        _patientService = patientService;
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;

        AddPatientCommand = new AsyncRelayCommand(AddPatient);
        EditPatientCommand = new AsyncRelayCommand(EditPatient, () => SelectedPatient != null);
        RemovePatientCommand = new AsyncRelayCommand(RemovePatient, () => SelectedPatient != null);
    }

    public async Task InitializeAsync()
    {
        Patients = [.. await _patientService.GetAllWithDetailsAsync()];
    }

    private async Task AddPatient()
    {
        var addVm = _serviceProvider.GetRequiredService<PatientAddViewModel>();
        await addVm.InitializeFormDataAsync();
        _navigationService.NavigateTo(addVm);
    }

    private async Task EditPatient()
    {
        var editVm = _serviceProvider.GetRequiredService<PatientEditViewModel>();
        await editVm.InitializeAsync(SelectedPatient.Id);
        _navigationService.NavigateTo(editVm);
    }

    private async Task RemovePatient()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego pacjenta?"))
        {
            await _patientService.RemoveAsync(SelectedPatient);
            Patients.Remove(SelectedPatient);
        }
    }

    public override async Task OnNavigatedBack()
    {
        await InitializeAsync();
    }
}
