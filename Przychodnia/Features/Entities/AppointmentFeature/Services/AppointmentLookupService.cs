using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.Repositories;

namespace Przychodnia.Features.Entities.AppointmentFeature.Services;

public class AppointmentLookupService(IAppointmentRepository repo)
    : IAppointmentLookupService
{
    private readonly IAppointmentRepository _repo = repo;

    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();
    public async Task<IEnumerable<Appointment>> GetAllForDoctorOnDateAsync(int doctorId, DateTime date)
        => await _repo.GetAllForDoctorOnDateAsync(doctorId, date);
    public async Task<IEnumerable<Appointment>> GetAllForPatientAsync(int patientId)
        => await _repo.GetAllForPatientAsync(patientId);
    public async Task<bool> HasAppointmentsForPatient(int patientId)
        => await _repo.HasAppointmentsForPatient(patientId);
}
