using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public class PatientAddViewModel : PatientFormBaseViewModel<PatientAddFormData>
{
    private readonly IPatientService _patientService;

    private PatientWrapper _addPatientWrapper;

    public PatientAddViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
        IPatientService patientService, IMapper mapper)
        : base(postalCodeService, dialogService, mapper)
    {
        _patientService = patientService;
        ActionButtonCommand = new AsyncRelayCommand(AddPatient);
    }

    public static string HeaderText => "Dodaj pacjenta";
    public static string ActionButtonText => "Dodaj";
    public PatientWrapper AddPatientWrapper
    {
        get => _addPatientWrapper;
        set => SetProperty(ref _addPatientWrapper, value);
    }

    public ICommand ActionButtonCommand { get; }

    public async Task InitializeAsync(PatientWrapper wrapper)
    {
        AddPatientWrapper = wrapper;
        await base.InitializeFormDataAsync();
    }

    private async Task AddPatient()
    {
        try
        {
            var dto = _mapper.Map<PatientDTO>(FormData);
            await _patientService.CreateAsync(dto);
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego pacjenta");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.InnerException.Message}");
        }
        ClearForm();
    }
    private void ClearForm()
    {
        EnteredCode = string.Empty;
        FormData.ClearForm();
    }
}
