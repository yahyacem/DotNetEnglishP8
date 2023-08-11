using System.Net.Http.Headers;
using AutoMapper;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.WebApp.Mappers;
using CalifornianHealthMonolithic.WebApp.Models;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;
using Newtonsoft.Json;

namespace CalifornianHealthMonolithic.WebApp.Services
{
    public class APIService : IAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string _APIConsultantURL;
        private readonly string _APIBookingURL;
        public APIService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _configuration = configuration;
            _APIConsultantURL = _configuration["API:APIConsultant"];
            _APIBookingURL = _configuration["API:APIBooking"];
        }
        public async Task<ConsultantNamesViewModel> GetConsultantNames()
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");

            // Call api to get names of consultants
            ConsultantNamesViewModel consultantNames = new();
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Consultants/Names");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                consultantNames.ConsultantsNames = JsonConvert.DeserializeObject<List<ConsultantNameModel>>(apiResponse);
            }

            // Returns result
            return consultantNames;
        }
        public async Task<ConsultantListViewModel> GetConsultantListViewModel()
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");

            // Call api to get list of consultants
            List<Consultant> listConsultantEntities = new();
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Consultants");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                listConsultantEntities = JsonConvert.DeserializeObject<List<Consultant>>(apiResponse);
            }

            // Map entities to view model
            ConsultantListViewModel listConsultantsViewModel = new();
            foreach(Consultant consultant in listConsultantEntities)
            {
                var c = _mapper.Map<ConsultantViewModel>(consultant);
                listConsultantsViewModel.ConsultantViewModels.Add(c);
            }

            // Returns result
            return listConsultantsViewModel;
        }
        public async Task<List<ConsultantCalendarViewModel>> GetListConsultantCalendarViewModel(int id)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");

            // Call api to get list of ConsultantCalendar
            List<ConsultantCalendar> listConsultantCalendarEntities = new();
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Consultants/{id}/Calendar");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                listConsultantCalendarEntities = JsonConvert.DeserializeObject<List<ConsultantCalendar>>(apiResponse);
            }

            // Map entities to view model
            List<ConsultantCalendarViewModel> consultantCalendarViewModel = new();
            foreach(ConsultantCalendar consultantCalendar in listConsultantCalendarEntities)
            {
                var c = _mapper.Map<ConsultantCalendarViewModel>(consultantCalendar);
                consultantCalendarViewModel.Add(c);
            }

            // Returns result
            return consultantCalendarViewModel;
        }
        public async Task<ConsultantCalendarViewModel> GetConsultantCalendarViewModel(int id, string token)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");

            // Add bearer token to authorization header
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Call api to get list of ConsultantCalendar
            ConsultantCalendar? cosultantCalendarEntity = null;
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Calendar/{id}");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                cosultantCalendarEntity = JsonConvert.DeserializeObject<ConsultantCalendar>(apiResponse);
            }

            // Map entities to view model
            ConsultantCalendarViewModel? consultantCalendarViewModel = null;
            if (cosultantCalendarEntity != null)
            {
                consultantCalendarViewModel = _mapper.Map<ConsultantCalendarViewModel>(cosultantCalendarEntity);
            }

            // Returns result
            return consultantCalendarViewModel;
        }
        public async Task<APIServiceResponseModel> BookAppointment(int id, string token)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            // Add bearer token to authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Call api to get list of ConsultantCalendar
            AppointmentViewModel? appointmentViewModel = null;
            var response = await _httpClient.PostAsync($"{_APIBookingURL}/Booking/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                appointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(apiResponse);
            }

            // Prepare response model
            APIServiceResponseModel requestResponse = new()
            {
                Result = appointmentViewModel,
                Response = response
            };

            // Returns result
            return requestResponse;
        }
        public async Task<AppointmentViewModel> GetAppointmentViewModelAsync(int id, string token)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");

            // Add bearer token to authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Call api to get list of ConsultantCalendar
            AppointmentViewModel? appointmentViewModel = null;
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Appointment/{id}");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                appointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(apiResponse);
            }
            
            // Returns result
            return appointmentViewModel;
        }
    }
}