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

public partial class PatientEditViewModel(IPatientService patientService, IDialogService dialogService,
    IPostalCodeService postalCodeService, IMapper mapper, IMessenger messenger) 
    : PatientFormBaseViewModel<PatientEditFormData>(postalCodeService, dialogService, mapper, messenger, patientService)
{
    [ObservableProperty] private PatientWrapper? _editPatientWrapper;

    public static string HeaderText => "Edytuj wybranego pacjenta";
    public static string SubmitButtonText => "Edytuj";

    public async Task InitializeAsync(PatientWrapper wrapper)
    {
        EditPatientWrapper = wrapper;
        EnteredCode = FormData.PostalCode?.Code ?? string.Empty;
        await base.InitializeFormDataAsync();
        _mapper.Map(EditPatientWrapper, FormData);
        FormData.PostalCode = Cities.FirstOrDefault(c => c.Id == EditPatientWrapper.PostalCode?.Id);
    }

    protected async override Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            _mapper.Map(FormData, EditPatientWrapper);
            if (FormData.PostalCode is not null && FormData.PostalCode.Id is null)
                EditPatientWrapper!.PostalCode = null;
            var dto = _mapper.Map<PatientDTO>(EditPatientWrapper);
            if (EditPatientWrapper.Id is not int id)
                throw new InvalidOperationException("Nie można aktualizować pacjenta bez ID");
            await _patientService.UpdateAsync(id, dto);
            _dialogService.Show("Sukces", "Pomyślnie edytowano pacjenta");
        });
    }
}
