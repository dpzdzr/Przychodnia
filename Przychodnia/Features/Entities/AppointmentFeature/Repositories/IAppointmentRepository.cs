using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.AppointmentFeature.Models;

namespace Przychodnia.Features.Entities.AppointmentFeature.Repositories;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetAllForDoctorOnDateAsync(int doctorId, DateTime date);
}
