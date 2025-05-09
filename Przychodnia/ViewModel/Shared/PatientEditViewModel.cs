using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public class PatientEditViewModel : PatientFormBaseViewModel<PatientEditFormData>
{
    private readonly IPatientService _patientService;
    
    private PatientWrapper _editPatientWrapper;

    public PatientEditViewModel(IPatientService patientService, IDialogService dialogService, IPostalCodeService postalCodeService, IMapper mapper)
    : base(postalCodeService, dialogService, mapper)
    {
        _patientService = patientService;
        ActionButtonCommand = new AsyncRelayCommand(EditPatient);
    }

    public static string HeaderText => "Edytuj wybranego pacjenta";
    public static string ActionButtonText => "Edytuj";
    public PatientWrapper EditPatientWrapper
    {
        get => _editPatientWrapper;
        set => SetProperty(ref _editPatientWrapper, value);
    }

    public ICommand ActionButtonCommand { get; }

    public async Task InitializeAsync(PatientWrapper wrapper)
    {
        EditPatientWrapper = wrapper;
        await base.InitializeFormDataAsync();
        _mapper.Map(EditPatientWrapper, FormData);
        EnteredCode = FormData.PostalCode.Code ?? string.Empty;
    }

    private async Task EditPatient()
    {
        _mapper.Map(FormData, EditPatientWrapper);
        await _patientService.UpdateAsync(EditPatientWrapper.Id, _mapper.Map<PatientDTO>(EditPatientWrapper));
        _dialogService.Show("Sukces", "Pomyślnie edytowano pacjenta");
    }
}
