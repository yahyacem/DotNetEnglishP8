using CalifornianHealthMonolithic.APIConsultant.Repositories;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;
using AutoMapper;

namespace CalifornianHealthMonolithic.APIConsultant.Services
{
    public class ConsultantService : IConsultantService
    {
        private IConsultantRepository _consultantRepository;
        private IConsultantCalendarRepository _consultantCalendarRepository;
        private IAppointmentRepository _appointmentRepository;
        private IPatientRepository _patientRepository;
        private IMapper _mapper;
        public ConsultantService(
            IConsultantRepository consultantRepository, 
            IConsultantCalendarRepository consultantCalendarRepository, 
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IMapper mapper) 
        {
            this._consultantRepository = consultantRepository;
            this._consultantCalendarRepository = consultantCalendarRepository;
            this._appointmentRepository = appointmentRepository;
            this._patientRepository = patientRepository;
            this._mapper = mapper;
        }
        public async Task<List<Consultant>> GetConsultantsAsync()
        {
            return await _consultantRepository.GetConsultantsAsync();
        }
        public async Task<List<ConsultantNameModel>> GetConsultantsNamesAsync()
        {
            return await _consultantRepository.GetConsultantNamesAsync();
        }
        public async Task<List<ConsultantCalendar>> GetConsultantCalendarByConsultantIdAsync(int id)
        {
            return await _consultantCalendarRepository.GetConsultantCalendarByConsultantIdAsync(id);
        }
        public async Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id)
        {
            return await _consultantCalendarRepository.GetConsultantCalendarByIdAsync(id);
        }
        public async Task<List<ConsultantCalendar>> GetConsultantCalendarOfMonthAsync(int id, DateTime date)
        {
            return await _consultantCalendarRepository.GetConsultantCalendarOfMonthAsync(id, date);
        }
        public async Task<AppointmentViewModel?> GetAppointmentViewModelByIdAsync(int id)
        {
            // Get appointment
            Appointment? appointmentEntity = await _appointmentRepository.GetAppointmentByIdAsync(id);

            // Check if appointment exists
            if (appointmentEntity == null)
            {
                return null;
            }

            // Get consultant
            Consultant? consultant = await _consultantRepository.GetConsultantByIdAsync(appointmentEntity.ConsultantId);
            
            // Get patient
            Patient? patient = await _patientRepository.GetPatientByIdAsync(appointmentEntity.PatientId);
            
            // Instantiate Appointment View Model and map values from Appointment entity object
            AppointmentViewModel appointmentViewModel = _mapper.Map<AppointmentViewModel>(appointmentEntity);
            
            // Pass values returned from Consultant and Patient previously retrieved
            appointmentViewModel.ConsultantName = consultant != null ? $"{consultant.FName} {consultant.LName}" : "";
            appointmentViewModel.PatientName = patient != null ? $"{patient.FName} {patient.LName}" : "";
            return appointmentViewModel;
        }
    }
}