﻿using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Core.Interfaces;
using Przychodnia.Features.Entities.AppointmentFeature.Messages;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.AppointmentFeature.Wrappers;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public partial class AppointmentEditViewModel(
    IDialogService dialogService, 
    IUserService userService,
    IPatientService patientService, 
    IAppointmentService appointmentService, 
    IMapper mapper, 
    IMessenger messenger)
    : AppointmentFormBaseViewModel<AppointmentEditFormData>
    (dialogService, userService, patientService, mapper, appointmentService, messenger)
{
    public AppointmentWrapper? appointmentWrapper;

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
        await TryExecuteAsync(async () =>
        {
            ValidateFormData();

            _mapper.Map(FormData, appointmentWrapper);
            var dto = _mapper.Map<AppointmentDTO>(FormData);
            var id = FormData.Id;
            await _appointmentService.UpdateAsync(id, dto);
            var entity = await _appointmentService.GetByIdAsync(id);
            _messenger.Send<AppointmentChangedMessage>(new(new(entity!, EntityChangedAction.Edited)));
            ShowSucces("Pomyślnie edytowano wizytę");
        });
    }
}
