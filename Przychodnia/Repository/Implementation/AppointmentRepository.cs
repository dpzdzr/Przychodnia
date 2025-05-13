using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class AppointmentRepository(AppDbContext context)
    : BaseRepository<Appointment, AppDbContext>(context), IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        => await _dbSet.Include(a => a.AttendingDoctor)
                       .Include(a => a.Patient)
                       .Include(a => a.ScheduledBy).ToListAsync();
}
