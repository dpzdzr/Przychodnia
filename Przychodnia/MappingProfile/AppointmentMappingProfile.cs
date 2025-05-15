using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.MappingProfile;

class AppointmentMappingProfile : Profile
{
    public AppointmentMappingProfile()
    {
        CreateMap<AppointmentDTO, Appointment>();
        CreateMap<AppointmentAddFormData, AppointmentDTO>()
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.SelectedPatient.Id))
            .ForMember(dest => dest.AttendingDoctorId, opt => opt.MapFrom(src => src.SelectedDoctor.Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.FullDate));
    }
}
