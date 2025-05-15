using Przychodnia.Model;

namespace Przychodnia.Service.Interface.Entity;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();

}
