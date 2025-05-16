using AutoMapper;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Shared;

public partial class AppointmentEditViewModel(IDialogService dialogService, IUserService userService,
    IPatientService patientService, IAppointmentService appointmentService, IMapper mapper)
    : AppointmentFormBaseViewModel<AppointmentEditFormData>
    (dialogService, userService, patientService, mapper, appointmentService)
{
    public AppointmentWrapper appointmentWrapper = new AppointmentWrapper();

    public static string HeaderText => "Edytuj wybraną wizytę";
    public static string SubmitButtonText => "Edytuj";



    public async Task InitializeAsync(AppointmentWrapper wrapper)
    {
        appointmentWrapper = wrapper;
        await base.InitializeFormDataAsync();
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
        _mapper.Map(FormData, appointmentWrapper);
        var dto = _mapper.Map<AppointmentDTO>(FormData);
        await _appointmentService.UpdateAsync(FormData.Id, dto);
        ShowSucces("Pomyślnie edytowano wizytę");
    }
}
