using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.WebApp.Models;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;

namespace CalifornianHealthMonolithic.WebApp.Services
{
    public interface IAPIService
    {
        ///<summary>Makes an API call to the APIConsultant to get the list of consultants names</summary>
        ///<returns>Returns the list of consultants names</returns>
        public Task<ConsultantNamesViewModel> GetConsultantNames();
        ///<summary>Makes an API call to the APIConsultant to get the list of consultants</summary>
        ///<returns>Returns the list of consultants</returns>
        public Task<ConsultantListViewModel> GetConsultantListViewModel();
        ///<summary>Makes an API call to the APIConsultant to get the list of consultant calendar by consultant calendar</summary>
        ///<returns>Returns the list of consultant calendar</returns>
        /// <param name="id">Consultant ID</param>
        public Task<List<ConsultantCalendarViewModel>> GetListConsultantCalendarViewModel(int id);
        ///<summary>Makes an API call to the APIConsultant to get a consultant calendar by id</summary>
        ///<returns>Returns the request consultant calendar</returns>
        /// <param name="id">Consultant calendar ID</param>
        /// <param name="token">Access token</param>
        public Task<ConsultantCalendarViewModel> GetConsultantCalendarViewModel(int id, string token);
        ///<summary>Makes an API call to the APIBooking to book an new appointment</summary>
        ///<returns>Returns APIServiceResponseModel with data and status of HTTP</returns>
        /// <param name="id">Consultant calendar ID</param>
        /// <param name="token">Access token</param>
        public Task<APIServiceResponseModel> BookAppointment(int id, string token);
        ///<summary>Makes an API call to the APIBooking to get details of an appointment</summary>
        ///<returns>Returns details of requested appointment</returns>
        /// <param name="id">Appointment ID</param>
        /// <param name="token">Access token</param>
        public Task<AppointmentModel> GetAppointmentViewModelAsync(int id, string token);
    }
}