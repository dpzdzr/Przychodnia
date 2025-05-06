using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Shared;

public class PatientsListViewModel : ViewModelBase
{   
    private readonly IPatientService _patientService;
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
        set => SetProperty(ref _selectedPatient, value);
    }


    public PatientsListViewModel(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public async Task InitializeAsync()
    {
        Patients = [.. await _patientService.GetAllAsync()];
    }

}
