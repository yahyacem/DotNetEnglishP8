using AutoMapper;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;
using static CalifornianHealthMonolithic.WebApp.Areas.Identity.Pages.Account.RegisterModel;

namespace CalifornianHealthMonolithic.WebApp.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Consultant, ConsultantViewModel>();
            CreateMap<ConsultantViewModel, Consultant>();
            CreateMap<ConsultantCalendarViewModel, ConsultantCalendar>()
                .ForMember(x=> x.Available, opt => opt.Ignore());
            CreateMap<ConsultantCalendar, ConsultantCalendarViewModel>();
            CreateMap<Patient, InputModel>();
            CreateMap<InputModel, Patient>();
        }
    }
}