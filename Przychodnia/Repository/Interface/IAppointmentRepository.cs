using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
}
