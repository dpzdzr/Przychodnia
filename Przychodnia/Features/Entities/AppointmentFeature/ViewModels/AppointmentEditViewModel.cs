using AutoMapper;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.AppointmentFeature.Wrappers;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Shared.Services.DialogService;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public partial class AppointmentEditViewModel(IDialogService dialogService, IUserService userService,
    IPatientService patientService, IAppointmentService appointmentService, IMapper mapper)
    : AppointmentFormBaseViewModel<AppointmentEditFormData>
    (dialogService, userService, patientService, mapper, appointmentService)
{
    public AppointmentWrapper appointmentWrapper = new();

    public static string HeaderText => "Edytuj wybraną wizytę";
    public static string SubmitButtonText => "Edytuj";



    public async Task InitializeAsync(AppointmentWrapper wrapper)
    {
        appointmentWrapper = wrapper;
        await InitializeFormDataAsync();
        FormData = _mapper.Map<AppointmentEditFormData>(wrapper);
        FormData.SelectedDoctor = Doctors.First(d => d.Id == wrapper.AttendingDoctor!.Id);
        await UpdateAvailableHours();
        if (wrapper.Date is DateTime dt)
        {
            var hour = dt.TimeOfDay;
            AvailableHours.Add(hour);
            SortAvailableHours();
            FormData.SelectedHour = hour;
        }

    }

    protected override async Task Submit()
    {
        if (!FormData.IsValid)
            throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");

        _mapper.Map(FormData, appointmentWrapper);
        var dto = _mapper.Map<AppointmentDTO>(FormData);
        await _appointmentService.UpdateAsync(FormData.Id, dto);
        ShowSucces("Pomyślnie edytowano wizytę");
    }
}
