using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.WebApp.Models.ViewModels
{
    public class ConsultantCalendarViewModel
    {
        public int Id { get; set; }
        public int ConsultantId { get; set; }
        public DateTime Date { get; set; }
    }
    public class ConsultantNamesViewModel 
    {
        public List<ConsultantNameModel> ConsultantsNames { get; set;}
    }
    public class ConsultantCalendarListViewModel
    {
        public List<ConsultantCalendarViewModel> ListConsultantsCalendar { get; set;}
    }
}