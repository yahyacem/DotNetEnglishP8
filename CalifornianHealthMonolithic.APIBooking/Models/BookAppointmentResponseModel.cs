using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;

namespace CalifornianHealthMonolithic.APIBooking.Models
{
    public class BookAppointmentResponseModel
    {
        public StatusType Status { get; set; }
        public AppointmentViewModel? Appointment { get; set; }
        public enum StatusType
        {
            Success,
            NotFound,
            NotAvailable,
            InternalError
        }
    }
}