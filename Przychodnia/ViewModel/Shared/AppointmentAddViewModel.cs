using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Shared;

public partial class AppointmentAddViewModel(IDialogService dialogService, IUserService userService,
    IPatientService patientService, IMapper mapper, IAppointmentService appointmentService)
    : AppointmentFormBaseViewModel<AppointmentAddFormData>(dialogService, userService, patientService,
        mapper, appointmentService)
{

    public static string HeaderText => "Dodaj nową wizytę";
    public static string SubmitButtonText => "Dodaj";

    public async Task InitializeAsync() => await base.InitializeFormDataAsync();

    protected override async Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            var dto = new AppointmentDTO();

            if (SelectedDoctor?.Id is int id)
                dto.AttendingDoctorId = id;

            var patient = await _patientService.GetByPeselAsync(EnteredPatientPesel) ??
                throw new KeyNotFoundException("Nie znaleziono pacjenta");

            dto.PatientId = patient.Id;

            if (FullDate is DateTime date)
                dto.Date = date;

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
