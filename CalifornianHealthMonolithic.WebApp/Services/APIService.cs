using System.Net.Http.Headers;
using AutoMapper;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;
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
        private readonly string _APIConsultantURL = "http://localhost:5264";
        private readonly string _APIBookingURL = "http://localhost:5102";
        public APIService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }
        public async Task<ConsultantNamesViewModel> GetConsultantNames()
        {
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");
            ConsultantNamesViewModel consultantNames = new();
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Consultants/Names");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                consultantNames.ConsultantsNames = JsonConvert.DeserializeObject<List<ConsultantNameModel>>(apiResponse);
            }
            return consultantNames;
        }
        public async Task<ConsultantListViewModel> GetConsultantListViewModel()
        {
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");
            List<Consultant> listConsultantEntities = new();
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Consultants");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                listConsultantEntities = JsonConvert.DeserializeObject<List<Consultant>>(apiResponse);
            }

            ConsultantListViewModel listConsultantsViewModel = new();
            foreach(Consultant consultant in listConsultantEntities)
            {
                var c = _mapper.Map<ConsultantViewModel>(consultant);
                listConsultantsViewModel.ConsultantViewModels.Add(c);
            }
            return listConsultantsViewModel;
        }
        public async Task<List<ConsultantCalendarViewModel>> GetListConsultantCalendarViewModel(int id)
        {
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");
            List<ConsultantCalendar> listConsultantCalendarEntities = new();
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Consultants/{id}/Calendar");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                listConsultantCalendarEntities = JsonConvert.DeserializeObject<List<ConsultantCalendar>>(apiResponse);
            }
            List<ConsultantCalendarViewModel> consultantCalendarViewModel = new();
            foreach(ConsultantCalendar consultantCalendar in listConsultantCalendarEntities)
            {
                var c = _mapper.Map<ConsultantCalendarViewModel>(consultantCalendar);
                consultantCalendarViewModel.Add(c);
            }
            return consultantCalendarViewModel;
        }
        public async Task<ConsultantCalendarViewModel> GetConsultantCalendarViewModel(int id, string token)
        {
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");
            ConsultantCalendar? cosultantCalendarEntity = null;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Calendar/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                cosultantCalendarEntity = JsonConvert.DeserializeObject<ConsultantCalendar>(apiResponse);
            }

            ConsultantCalendarViewModel? consultantCalendarViewModel = null;
            if (cosultantCalendarEntity != null)
            {
                consultantCalendarViewModel = _mapper.Map<ConsultantCalendarViewModel>(cosultantCalendarEntity);
            }
            return consultantCalendarViewModel;
        }
        public async Task<AppointmentViewModel> BookAppointment(int id, string token)
        {
            AppointmentViewModel? appointmentViewModel = null;
            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync($"{_APIBookingURL}/Booking/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                appointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(apiResponse);
            }
            
            return appointmentViewModel;
        }
        public async Task<AppointmentViewModel> GetAppointmentViewModelAsync(int id, string token)
        {
            var _httpClient = _httpClientFactory.CreateClient("APIConsultant");
            AppointmentViewModel? appointmentViewModel = null;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_APIConsultantURL}/Appointment/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                appointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(apiResponse);
            }
            
            return appointmentViewModel;
        }
    }
}