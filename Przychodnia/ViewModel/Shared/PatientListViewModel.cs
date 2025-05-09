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
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public class PatientListViewModel : BaseViewModel
{
    private readonly IPatientService _patientService;
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;

    private ObservableCollection<PatientWrapper> _patients;
    private PatientWrapper _selectedPatient;

    public PatientListViewModel(IPatientService patientService, INavigationService navigationService, IServiceProvider serviceProvider, IDialogService dialogService)
    {
        _patientService = patientService;
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;

        AddPatientCommand = new AsyncRelayCommand(AddPatient);
        EditPatientCommand = new AsyncRelayCommand(EditPatient, () => SelectedPatient != null);
        RemovePatientCommand = new AsyncRelayCommand(RemovePatient, () => SelectedPatient != null);
    }

    public PatientWrapper SelectedPatient
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
    public ObservableCollection<PatientWrapper> Patients
    {
        get => _patients;
        set => SetProperty(ref _patients, value);
    }

    public ICommand AddPatientCommand { get; }
    public ICommand EditPatientCommand { get; }
    public ICommand RemovePatientCommand { get; }

    public async Task InitializeAsync()
    {
        var items = await _patientService.GetAllWithDetailsAsync();
        Patients = [.. items.Select(p => new PatientWrapper(p))];
    }
    public override async Task OnNavigatedBack()
    {
        await InitializeAsync();
    }

    private async Task AddPatient()
    {
        PatientWrapper newPatientWrapper = new(new Patient());
        Patients.Add(newPatientWrapper);

        var addVm = _serviceProvider.GetRequiredService<PatientAddViewModel>();
        await addVm.InitializeAsync(newPatientWrapper);
        _navigationService.NavigateTo(addVm);
    }

    private async Task EditPatient()
    {
        var editVm = _serviceProvider.GetRequiredService<PatientEditViewModel>();
        await editVm.InitializeAsync(SelectedPatient);
        _navigationService.NavigateTo(editVm);
    }

    private async Task RemovePatient()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego pacjenta?"))
        {
            await _patientService.RemoveAsync(SelectedPatient.Id);
            Patients.Remove(SelectedPatient);
        }
    }
}
