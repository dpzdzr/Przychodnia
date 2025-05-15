using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class AppointmentService(IAppointmentRepository appointmentRepo, IMapper mapper,
    IPatientService patientService, IUserService userService)
    : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepo = appointmentRepo;
    private readonly IUserService _userService = userService;
    private readonly IPatientService _patientService = patientService;
    private readonly IMapper _mapper = mapper;

    public async Task<Appointment> CreateAsync(AppointmentDTO dto)
    {
        var appointment = new Appointment();
        await MapDtoAndResolveRelationsAsync(dto, appointment);
        await _appointmentRepo.AddAsync(appointment);
        appointment = await _appointmentRepo.AddAsync(appointment);
        await _appointmentRepo.SaveChangesAsync();
        return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        => await _appointmentRepo.GetAllWithDetailsAsync();

    public async Task<IEnumerable<Appointment>> GetAppointmentsForDoctorOnDateAsync(int doctorId, DateTime date)
        => await _appointmentRepo.GetAllForDoctorOnDateAsync(doctorId, date);

    public async Task RemoveAsync(int appointmentId)
    {
        var appointment = await _appointmentRepo.GetByIdAsync(appointmentId) ??
            throw new KeyNotFoundException("Nie znaleziono wizyty");
        _appointmentRepo.Remove(appointment);
        await _appointmentRepo.SaveChangesAsync();
    }

    private async Task MapDtoAndResolveRelationsAsync(AppointmentDTO dto, Appointment target)
    {
        _mapper.Map(dto, target);

        var doctor = await _userService.GetByIdWithDetailsAsync(dto.AttendingDoctorId) ??
            throw new KeyNotFoundException("Nieprawidłowy lekarz");

        var patient = await _patientService.GetByIdAsync(dto.PatientId) ??
            throw new KeyNotFoundException("Nieprawidłowy pacjent");

        target.AttendingDoctor = doctor;
        target.Patient = patient;
    }
}
