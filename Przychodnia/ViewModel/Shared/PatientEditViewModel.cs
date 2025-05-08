using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Shared;

public class PatientEditViewModel : PatientFormBaseViewModel<PatientEditFormData>
{
    private readonly IPatientService _patientService;
    private Patient _editablePatient;

    public Patient EditablePatient
    {
        get => _editablePatient;
        set => SetProperty(ref _editablePatient, value);
    }

    public static string HeaderText => "Edytuj wybranego pacjenta";
    public static string ActionButtonText => "Edytuj";

    public ICommand ActionButtonCommand { get; }
    public PatientEditViewModel(IPatientService patientService, IDialogService dialogService, IPostalCodeService postalCodeService)
        : base(postalCodeService, dialogService)
    {
        _patientService = patientService;
        ActionButtonCommand = new AsyncRelayCommand(EditPatient);
    }

    private async Task EditPatient()
    {
        FormData.LoadToPatient(EditablePatient);
        await _patientService.SaveChangesAsync();
        _dialogService.Show("Sukces", "Pomyślnie edytowano pacjenta");
    }

    public async Task InitializeAsync(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        EditablePatient = patient;

        await base.InitializeFormDataAsync();
        FormData.LoadFromPatient(patient);
        EnteredCode = FormData.PostalCode.Code ?? string.Empty;
    }
}
