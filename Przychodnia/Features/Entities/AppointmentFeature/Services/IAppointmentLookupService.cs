using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Features.Entities.AppointmentFeature.Models;

namespace Przychodnia.Features.Entities.AppointmentFeature.Services;

public interface IAppointmentLookupService
{
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetAllForDoctorOnDateAsync(int doctorId, DateTime date);
    Task<IEnumerable<Appointment>> GetAllForPatientAsync(int patientId);
    Task<bool> HasAppointmentsForPatient(int patientId);
}
