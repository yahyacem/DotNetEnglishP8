using CalifornianHealthMonolithic.APIBooking.Repositories;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.APIBooking.Models;
using AutoMapper;

namespace CalifornianHealthMonolithic.APIBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper, ILogger<BookingService> logger) 
        {
            this._bookingRepository = bookingRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<BookAppointmentResponseModel> BookAppointmentAsync(int patientId, int consultantCalendarId)
        {
            BookAppointmentResponseModel response = new();

            // Get ConsultantCalendar record to check if it exists and if it's available
            ConsultantCalendar? consultantCalendarToBook = await _bookingRepository.GetConsultantCalendarByIdAsync(consultantCalendarId);
            if (consultantCalendarToBook == null)
            {
                response.Status = BookAppointmentResponseModel.StatusType.NotFound;
                return response;
            }
            if (!consultantCalendarToBook.Available)
            {
                response.Status = BookAppointmentResponseModel.StatusType.NotAvailable;
                return response;
            }
            // Update the ConsultantCalendar record so it's not available anymore
            consultantCalendarToBook.Available = false;
            consultantCalendarToBook = await _bookingRepository.UpdateConsultantCalendarAsync(consultantCalendarToBook);
            if (consultantCalendarToBook == null)
            {
                response.Status = BookAppointmentResponseModel.StatusType.InternalError;
                return response;
            }

            // Create a new appointment
            Appointment? newAppointment = new()
            {
                Id = null,
                PatientId = patientId,
                ConsultantId = consultantCalendarToBook.ConsultantId,
                StartDateTime = consultantCalendarToBook.Date,
                EndDateTime = consultantCalendarToBook.Date.AddHours(1)
            };
            newAppointment = await _bookingRepository.CreateAppointmentAsync(newAppointment);
            // Check if appointment was created properly
            if (newAppointment == null)
            {
                response.Status = BookAppointmentResponseModel.StatusType.InternalError;
                return response;
            }
            // Get the consultant and patient informations
            Consultant consultantEntity = await _bookingRepository.GetConsultantByIdAsync(newAppointment.ConsultantId);
            Patient patientEntity = await _bookingRepository.GetPatientByIdAsync(newAppointment.PatientId);

            AppointmentViewModel appointmentViewModel = _mapper.Map<AppointmentViewModel>(newAppointment);
            if (appointmentViewModel == null)
            {
                return null;
            }
            appointmentViewModel.ConsultantName = $"{consultantEntity.FName} {consultantEntity.LName}";
            appointmentViewModel.PatientName = $"{patientEntity.FName} {patientEntity.LName}";
            
            // Return success response with the new created appointment
            response.Status = BookAppointmentResponseModel.StatusType.Success;
            response.Appointment = appointmentViewModel;
            
            return response;
        }
    }
}