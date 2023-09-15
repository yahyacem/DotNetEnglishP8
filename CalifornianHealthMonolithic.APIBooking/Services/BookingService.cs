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
        public BookingService(IBookingRepository bookingRepository, IMapper mapper) 
        {
            this._bookingRepository = bookingRepository;
            this._mapper = mapper;
        }
        public async Task<BookAppointmentResponseModel> BookAppointmentAsync(int patientId, int consultantCalendarId)
        {
            BookAppointmentResponseModel response = new();

            // Book appointment
            Appointment? newAppointment = await _bookingRepository.BookAppointmentAsync(consultantCalendarId, patientId);
            if (newAppointment == null)
            {
                response.Status = BookAppointmentResponseModel.StatusType.NotAvailable;
                return response;
            }
            // Get the consultant and patient informations
            Consultant consultantEntity = await _bookingRepository.GetConsultantByIdAsync(newAppointment.ConsultantId);
            Patient patientEntity = await _bookingRepository.GetPatientByIdAsync(newAppointment.PatientId);

            AppointmentModel appointmentViewModel = _mapper.Map<AppointmentModel>(newAppointment);
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