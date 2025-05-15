using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IAppointmentService
{
    Task<Appointment> CreateAsync(AppointmentDTO dto);
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetAppointmentsForDoctorOnDateAsync(int doctorId, DateTime date);
    Task RemoveAsync(int appointmentId);
}
