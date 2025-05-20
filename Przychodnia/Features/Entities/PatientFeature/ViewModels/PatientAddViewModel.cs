using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.PatientFeature.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels;

public class PatientAddViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
    IPatientService patientService, IMapper mapper, IMessenger messenger)
    : PatientFormBaseViewModel<PatientAddFormData>(postalCodeService, dialogService, mapper, messenger, patientService)
{
    public static string HeaderText => "Dodaj pacjenta";
    public static string SubmitButtonText => "Dodaj";

    public override async Task InitializeAsync() => await InitializeFormDataAsync();

    protected override async Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            ValidateFormData();

            var dto = _mapper.Map<PatientDTO>(FormData);
            var entity = await _patientService.CreateAsync(dto);
            _messenger.Send<PatientChangedMessage>(new(new(entity, EntityChangedAction.Added)));
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego pacjenta");
            ClearForm();
        });
    }

    //private void NotifyPatientAdded(Patient entity)
    //    => _messenger.Send(new PatientAddedMessage(entity));
    private void ClearForm()
    {
        EnteredCode = string.Empty;
        FormData.ClearForm();
    }
}
