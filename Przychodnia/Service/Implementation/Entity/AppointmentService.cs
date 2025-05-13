using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class AppointmentService(IAppointmentRepository appointmentRepo) 
    : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepo = appointmentRepo;

    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        => await _appointmentRepo.GetAllWithDetailsAsync();
}
