using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Implementation;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class AppointmentService(IAppointmentRepository appointmentRepo, IMapper mapper,
    IPatientService patientService, IUserService userService)
    : BaseService<Appointment, AppointmentDTO, IAppointmentRepository>(appointmentRepo, mapper), IAppointmentService
{
    private readonly IUserService _userService = userService;
    private readonly IPatientService _patientService = patientService;

    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();
    public async Task<IEnumerable<Appointment>> GetAppointmentsForDoctorOnDateAsync(int doctorId, DateTime date)
        => await _repo.GetAllForDoctorOnDateAsync(doctorId, date);

    public async override Task<Appointment> CreateAsync(AppointmentDTO dto)
    {
        var appointment = new Appointment();
        await MapDtoAndResolveRelationsAsync(dto, appointment);
        await _repo.AddAsync(appointment);
        await _repo.SaveChangesAsync();
        return appointment;
    }
    public override async Task RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _repo.Remove(entity!);
        await _repo.SaveChangesAsync();
    }
    public override async Task UpdateAsync(int id, AppointmentDTO dto)
    {
        var entity = await GetByIdAsync(id);
        _mapper.Map(dto, entity);
        await _repo.SaveChangesAsync();
    }

    private async Task MapDtoAndResolveRelationsAsync(AppointmentDTO dto, Appointment target)
    {
        _mapper.Map(dto, target);
        target.AttendingDoctor = await _userService.GetByIdWithDetailsAsync(dto.AttendingDoctorId);
        target.Patient = await _patientService.GetByIdAsync(dto.PatientId);
    }
}
