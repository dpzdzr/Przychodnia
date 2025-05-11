using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public partial class PatientListViewModel : BaseViewModel
{
    private readonly IPatientService _patientService;
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;

    [ObservableProperty] private PatientWrapper? selectedPatient;
    [ObservableProperty] private ObservableCollection<PatientWrapper> patients = [];

    public PatientListViewModel(IPatientService patientService, INavigationService navigationService,
        IServiceProvider serviceProvider, IDialogService dialogService)
    {
        _patientService = patientService;
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;

        AddPatientCommand = new AsyncRelayCommand(AddPatient);
        EditPatientCommand = new AsyncRelayCommand(EditPatient, () => SelectedPatient != null);
        CancelSelectionCommand = new RelayCommand(CancelSelection, () => SelectedPatient != null);
        RemovePatientCommand = new AsyncRelayCommand(RemovePatient, () => SelectedPatient != null);

        WeakReferenceMessenger.Default.Register<PatientAddedMessage>(this, (r, m) =>
        {
            Patients.Add(new PatientWrapper(m.Value));
        });
    }

    public IAsyncRelayCommand AddPatientCommand { get; }
    public IAsyncRelayCommand EditPatientCommand { get; }
    public IAsyncRelayCommand RemovePatientCommand { get; }
    public IRelayCommand CancelSelectionCommand { get; }

    public async Task InitializeAsync()
    {
        var items = await _patientService.GetAllWithDetailsAsync();
        Patients = [.. items.Select(p => new PatientWrapper(p))];
    }

    private async Task AddPatient()
    {
        var addVm = _serviceProvider.GetRequiredService<PatientAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }
    private async Task EditPatient()
    {
        var editVm = _serviceProvider.GetRequiredService<PatientEditViewModel>();
        if (SelectedPatient is not null)
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
    private void CancelSelection()
    {
        SelectedPatient = null;
    }
    partial void OnSelectedPatientChanged(PatientWrapper? value)
    {
        (EditPatientCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
        (RemovePatientCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
        (CancelSelectionCommand as RelayCommand)?.NotifyCanExecuteChanged();
    }
}
