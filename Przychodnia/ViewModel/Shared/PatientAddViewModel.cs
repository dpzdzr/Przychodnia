using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public class PatientAddViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
    IPatientService patientService, IMapper mapper, IMessenger messenger) 
    : PatientFormBaseViewModel<PatientAddFormData>(postalCodeService, dialogService, mapper, messenger, patientService)
{
    public static string HeaderText => "Dodaj pacjenta";
    public static string SubmitButtonText => "Dodaj";

    public async Task InitializeAsync() => await base.InitializeFormDataAsync();

    protected override async Task Submit()
    {
        await TryExecuteAsync(async () => 
        {
            var dto = _mapper.Map<PatientDTO>(FormData);
            var entity = await _patientService.CreateAsync(dto);
            NotifyPatientAdded(entity);
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego pacjenta");
            ClearForm();
        });
    }

    private void NotifyPatientAdded(Patient entity)
        => _messenger.Send(new PatientAddedMessage(entity));
    private void ClearForm()
    {
        EnteredCode = string.Empty;
        FormData.ClearForm();
    }
}
