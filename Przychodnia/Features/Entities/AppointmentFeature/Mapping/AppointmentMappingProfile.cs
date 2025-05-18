using AutoMapper;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.AppointmentFeature.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.AppointmentFeature.Mapping;

class AppointmentMappingProfile : Profile
{
    public AppointmentMappingProfile()
    {
        CreateMap<AppointmentDTO, Appointment>();
        CreateMap<AppointmentAddFormData, AppointmentDTO>()
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.SelectedPatient.Id))
            .ForMember(dest => dest.AttendingDoctorId, opt => opt.MapFrom(src => src.SelectedDoctor.Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.FullDate));
        CreateMap<AppointmentEditFormData, AppointmentDTO>()
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.SelectedPatient.Id))
            .ForMember(dest => dest.AttendingDoctorId, opt => opt.MapFrom(src => src.SelectedDoctor.Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.FullDate));
        CreateMap<AppointmentWrapper, AppointmentEditFormData>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SelectedDate,
                opt => opt.MapFrom(src => src.Date.HasValue ? src.Date.Value.Date : (DateTime?)null))

            //.ForMember(dest => dest.SelectedHour,
            //    opt => opt.MapFrom(src => src.Date.HasValue ? src.Date.Value.TimeOfDay : (TimeSpan?)null))
            .ForMember(dest => dest.SelectedHour, opt => opt.Ignore())

            .ForMember(dest => dest.Completed,
                opt => opt.MapFrom(src => src.Completed))
            .ForMember(dest => dest.ScheduledBy,
                opt => opt.MapFrom(src => src.ScheduledBy))

            //.ForMember(dest => dest.SelectedDoctor,
            //    opt => opt.MapFrom(src => src.AttendingDoctor))
            .ForMember(dest => dest.SelectedDoctor, opt => opt.Ignore())

            .ForMember(dest => dest.SelectedPatient,
                opt => opt.MapFrom(src => src.Patient))
            .ForMember(dest => dest.EnteredPatientPesel,
                opt => opt.MapFrom(src => src.Patient.Pesel ?? string.Empty));
        ;

        CreateMap<AppointmentEditFormData, AppointmentWrapper>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))

            .ForMember(dest => dest.Date,
                opt => opt.MapFrom(src =>
                    src.SelectedDate.HasValue && src.SelectedHour.HasValue
                        ? src.SelectedDate.Value.Date + src.SelectedHour.Value
                        : (DateTime?)null))

            .ForMember(dest => dest.Completed,
                opt => opt.MapFrom(src => src.Completed))

            .ForMember(dest => dest.ScheduledBy,
                opt => opt.MapFrom(src => src.ScheduledBy))

            .ForMember(dest => dest.AttendingDoctor,
                opt => opt.MapFrom(src => src.SelectedDoctor))

            .ForMember(dest => dest.Patient,
                opt => opt.MapFrom(src => src.SelectedPatient));

    }
}
