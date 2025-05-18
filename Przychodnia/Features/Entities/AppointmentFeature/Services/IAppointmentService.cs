using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.AppointmentFeature.Models;

namespace Przychodnia.Features.Entities.AppointmentFeature.Services;

public interface IAppointmentService : IBaseEntityService<Appointment, AppointmentDTO>
{
    new Task<Appointment> CreateAsync(AppointmentDTO dto);
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetAppointmentsForDoctorOnDateAsync(int doctorId, DateTime date);
}
