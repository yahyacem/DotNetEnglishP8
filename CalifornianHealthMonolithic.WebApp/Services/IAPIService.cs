using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.WebApp.Models;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;

namespace CalifornianHealthMonolithic.WebApp.Services
{
    public interface IAPIService
    {
        ///<summary>Makes an API call to the APIConsultant to get the list of consultants</summary>
        ///<returns>Returns the list of consultants</returns>
        public Task<ConsultantNamesViewModel> GetConsultantNames();
        public Task<ConsultantListViewModel> GetConsultantListViewModel();
        public Task<List<ConsultantCalendarViewModel>> GetListConsultantCalendarViewModel(int id);
        public Task<ConsultantCalendarViewModel> GetConsultantCalendarViewModel(int id, string token);
        public Task<APIServiceResponseModel> BookAppointment(int id, string token);
        public Task<AppointmentViewModel> GetAppointmentViewModelAsync(int id, string token);
    }
}