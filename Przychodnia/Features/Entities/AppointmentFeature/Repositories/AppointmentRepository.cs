using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.AppointmentFeature.Models;

namespace Przychodnia.Features.Entities.AppointmentFeature.Repositories;

public class AppointmentRepository(AppDbContext context)
    : BaseRepository<Appointment, AppDbContext>(context), IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetAllForDoctorOnDateAsync(int doctorId, DateTime date)
    {
        return await _dbSet
            .Include(a => a.AttendingDoctor)
            .Include(a => a.Patient)
            .Where(a => a.AttendingDoctorId == doctorId && a.Date.Value.Date == date.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(a => a.AttendingDoctor)
            .Include(a => a.Patient)
            .Include(a => a.ScheduledBy)
            .ToListAsync();
    }
}
