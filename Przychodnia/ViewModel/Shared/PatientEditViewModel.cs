using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public partial class PatientEditViewModel : PatientFormBaseViewModel<PatientEditFormData>
{
    private readonly IPatientService _patientService;

    [ObservableProperty] private PatientWrapper _editPatientWrapper;

    public PatientEditViewModel(IPatientService patientService, IDialogService dialogService, IPostalCodeService postalCodeService, IMapper mapper)
    : base(postalCodeService, dialogService, mapper)
    {
        _patientService = patientService;
        ActionButtonCommand = new AsyncRelayCommand(EditPatient);
    }

    public static string HeaderText => "Edytuj wybranego pacjenta";
    public static string ActionButtonText => "Edytuj";

    public ICommand ActionButtonCommand { get; }

    public async Task InitializeAsync(PatientWrapper wrapper)
    {
        EditPatientWrapper = wrapper;
        await base.InitializeFormDataAsync();
        _mapper.Map(EditPatientWrapper, FormData);
        EnteredCode = FormData.PostalCode?.Code ?? string.Empty;
    }

    private async Task EditPatient()
    {
        try
        {
            _mapper.Map(FormData, EditPatientWrapper);
            var dto = _mapper.Map<PatientDTO>(EditPatientWrapper);
            await _patientService.UpdateAsync(EditPatientWrapper.Id, dto);
            _dialogService.Show("Sukces", "Pomyślnie edytowano pacjenta");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.Message}\n{ex.InnerException?.Message ?? string.Empty}");
        }
    }
}
