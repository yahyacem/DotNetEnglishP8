using AutoMapper;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;

namespace CalifornianHealthMonolithic.Shared.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Appointment, AppointmentViewModel>();
        }
    }
}