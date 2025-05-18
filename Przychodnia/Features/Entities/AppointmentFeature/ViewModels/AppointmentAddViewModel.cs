using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Shared.Services;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public partial class AppointmentAddViewModel(IDialogService dialogService, IUserService userService,
    IPatientService patientService, IMapper mapper, IAppointmentService appointmentService)
    : AppointmentFormBaseViewModel<AppointmentAddFormData>(dialogService, userService, patientService,
        mapper, appointmentService)
{

    public static string HeaderText => "Dodaj nową wizytę";
    public static string SubmitButtonText => "Dodaj";

    public async Task InitializeAsync() => await InitializeFormDataAsync();

    protected override async Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            if(!FormData.IsValid)
                throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");

            var dto = _mapper.Map<AppointmentDTO>(FormData);
            dto.PatientId = (await _patientService.GetByPeselAsync(FormData.EnteredPatientPesel))!.Id;
            await _appointmentService.CreateAsync(dto);
            ShowSucces("Pomyślnie dodano nową wizytę");
            ClearForm();
        });
    }

    private void ClearForm()
    {
        FormData.ClearForm();
    }
}
