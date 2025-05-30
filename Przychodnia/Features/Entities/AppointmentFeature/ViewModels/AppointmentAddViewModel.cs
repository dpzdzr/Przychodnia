﻿using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.AppointmentFeature.Messages;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.CurrentUserService;
using Przychodnia.Shared.Services.DialogService;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public partial class AppointmentAddViewModel(
    IDialogService dialogService, 
    IUserService userService,
    IPatientService patientService, 
    IMapper mapper, 
    IAppointmentService appointmentService, 
    IMessenger messenger,
    ICurrentUserService currentUserService)
    : AppointmentFormBaseViewModel<AppointmentAddFormData>
    (dialogService, userService, patientService, mapper, appointmentService, messenger)
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    public static string HeaderText => "Dodaj nową wizytę";
    public static string SubmitButtonText => "Dodaj";

    public override async Task InitializeAsync() => await InitializeFormDataAsync();

    protected override async Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            ValidateFormData();

            var dto = _mapper.Map<AppointmentDTO>(FormData);
            var currentUser = _currentUserService.GetUser();
            if(currentUser?.UserTypeId == (int)UserTypeEnum.Rejestrator)
                dto.ScheduledById = currentUser.Id;
            dto.PatientId = (await _patientService.GetByPeselAsync(FormData.EnteredPatientPesel))!.Id;
            var entity = await _appointmentService.CreateAsync(dto);
            ShowSucces("Pomyślnie dodano nową wizytę");
            _messenger.Send<AppointmentChangedMessage>(new(new(entity, EntityChangedAction.Added)));
            ClearForm();
        });
    }

    private void ClearForm()
    {
        FormData.ClearForm();
    }
}
