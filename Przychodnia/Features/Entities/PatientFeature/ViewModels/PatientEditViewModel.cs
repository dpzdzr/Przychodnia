using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.PatientFeature.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PatientFeature.Wrappers;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;


namespace Przychodnia.Features.Entities.PatientFeature.ViewModels;

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
        await InitializeFormDataAsync();
        _mapper.Map(EditPatientWrapper, FormData);
        FormData.PostalCode = Cities.FirstOrDefault(c => c.Id == EditPatientWrapper.PostalCode?.Id);
    }

    protected async override Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            ValidateFormData();

            if (EditPatientWrapper?.Id is not int id)
                throw new InvalidOperationException("Nie można aktualizować pacjenta bez ID");

            var dto = MapToDto();
            await _patientService.UpdateAsync(id, dto);
            await NotifyEntityUpdated(id);
            _dialogService.Show("Sukces", "Pomyślnie edytowano pacjenta");
        });
    }

    private async Task NotifyEntityUpdated(int id)
    {
        var entity = await _patientService.GetByIdAsync(id);
        _messenger.Send<PatientChangedMessage>(new(new(entity, EntityChangedAction.Edited)));
    }

    private PatientDTO MapToDto()
    {
        _mapper.Map(FormData, EditPatientWrapper);

        // dummy check
        if (FormData.PostalCode is not null && FormData.PostalCode.Id is null)
            EditPatientWrapper!.PostalCode = null;

        return _mapper.Map<PatientDTO>(EditPatientWrapper);
    }
}
