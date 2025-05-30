﻿using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.AppointmentFeature.Models;

namespace Przychodnia.Features.Entities.AppointmentFeature.Services;

public interface IAppointmentService : IBaseEntityService<Appointment, AppointmentDTO>
{
    new Task<Appointment> CreateAsync(AppointmentDTO dto);
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetAllForDoctorOnDateAsync(int doctorId, DateTime date);
    Task<IEnumerable<Appointment>> GetAllForPatientAsync(int patientId);
    Task<bool> HasAppointmentsForPatient(int patientId);
    Task RemoveAllForPatient(int patientId);
}
