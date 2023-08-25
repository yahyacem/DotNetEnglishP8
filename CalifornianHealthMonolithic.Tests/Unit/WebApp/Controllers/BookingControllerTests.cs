using System.Net;
using System.Security.Claims;
using AutoMapper;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Tests.Integration;
using CalifornianHealthMonolithic.WebApp.Controllers;
using CalifornianHealthMonolithic.WebApp.Mappers;
using CalifornianHealthMonolithic.WebApp.Models;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;
using CalifornianHealthMonolithic.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace CalifornianHealthMonolithic.Tests.Unit.WebApp.Controllers
{
    public class BookingControllerTests : UnitBase
    {
        public BookingControllerTests() : base() { }
        [Fact]
        public async Task ConsultantCalendar_Should_Return_ConsultantNamesViewModel()
        {
            // Arrange

            // Get seed data for database
            List<Consultant> consultants = GetSeedConsultants(10);

            ConsultantNamesViewModel consultantNamesViewModelToReturn = new()
            {
                ConsultantsNames = new List<ConsultantNameModel>()
            };
            foreach (var c in consultants)
            {
                consultantNamesViewModelToReturn.ConsultantsNames
                .Add(new ConsultantNameModel()
                {
                    Id = c.Id,
                    Name = $"{c.FName} {c.LName}"
                });
            }

            // Set up parameters and instantiate service
            var authenticationService = new Mock<IAuthenticationService>();

            var apiService = new Mock<IAPIService>();
            apiService.Setup(x => x.GetConsultantNames()).ReturnsAsync(consultantNamesViewModelToReturn);

            // Instantiate controller
            BookingController bookingController = new(apiService.Object, authenticationService.Object);

            // Act
            var response = await bookingController.ConsultantCalendar();

            // Assert

            // Response should be NotFound
            Assert.IsType<ViewResult>(response);
            Assert.Equal(((ViewResult)response).Model, consultantNamesViewModelToReturn);
        }
        [Fact]
        public async Task CreateAppointment_Should_Return_ConsultantCalendarViewModel()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);

            ConsultantCalendarViewModel consultantCalendarViewModelToReturn = new()
            {
                Id = Convert.ToInt32(consultantCalendars.First().Id),
                ConsultantId = Convert.ToInt32(consultantCalendars.First().ConsultantId),
                Date = consultantCalendars.First().Date
            };

            // Set up parameters and instantiate service
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.GetValidTokenAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync("");

            var apiService = new Mock<IAPIService>();
            apiService.Setup(x => x.GetConsultantCalendarViewModel(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(consultantCalendarViewModelToReturn);

            // Instantiate controller
            BookingController bookingController = new(apiService.Object, authenticationService.Object);

            // Act
            var response = await bookingController.CreateAppointment(1);

            // Assert

            // Response should be NotFound
            Assert.IsType<ViewResult>(response);
            Assert.Equal(((ViewResult)response).Model, consultantCalendarViewModelToReturn);
        }
        [Fact]
        public async Task ConfirmAppointment_Should_Redirect_AppointmentViewModel()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

            APIServiceResponseModel responseModel = new()
            {
                Response = new HttpResponseMessage(HttpStatusCode.OK),
                Result = new AppointmentViewModel()
                {
                    Id = Convert.ToInt32(appointments.First().Id),
                    StartDateTime = appointments.First().StartDateTime,
                    EndDateTime = appointments.First().EndDateTime,
                    ConsultantName = $"{consultants.First().FName} {consultants.First().LName}",
                    PatientName = $"{patients.First().FName} {patients.First().LName}"
                }
            };

            // Set up parameters and instantiate service
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.GetValidTokenAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync("");

            var apiService = new Mock<IAPIService>();
            apiService.Setup(x => x.BookAppointment(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(responseModel);

            // Instantiate controller
            BookingController bookingController = new(apiService.Object, authenticationService.Object);

            // Act
            var response = await bookingController.ConfirmAppointment(1);

            // Assert

            // Response should be NotFound
            Assert.IsType<RedirectResult>(response);
            Assert.Equal(((RedirectResult)response).Url, $"/Appointment/{appointments.First().Id}");
        }
        [Fact]
        public async Task Calendar_Should_Redirect_ConsultantCalendarListViewModel()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);
            
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));

            ConsultantCalendarListViewModel consultantCalendarListViewModel = new()
            {
                ListConsultantsCalendar = new List<ConsultantCalendarViewModel>()
            };
            foreach(ConsultantCalendar consultantCalendar in consultantCalendars)
            {
                var c = mapper.Map<ConsultantCalendarViewModel>(consultantCalendar);
                consultantCalendarListViewModel.ListConsultantsCalendar.Add(c);
            }
            
            // Set up parameters and instantiate service
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.GetValidTokenAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync("");

            var apiService = new Mock<IAPIService>();
            apiService.Setup(x => x.GetListConsultantCalendarViewModel(It.IsAny<int>())).ReturnsAsync(consultantCalendarListViewModel.ListConsultantsCalendar);

            // Instantiate controller
            BookingController bookingController = new(apiService.Object, authenticationService.Object);

            // Act
            var response = await bookingController.Calendar(1);

            // Assert

            // Response should be NotFound
            Assert.IsType<PartialViewResult>(response);
            var responseModel = ((PartialViewResult)response).Model as ConsultantCalendarListViewModel;
            Assert.NotNull(responseModel);
            Assert.NotEmpty(responseModel.ListConsultantsCalendar);
            Assert.Equal(responseModel.ListConsultantsCalendar.Count, consultantCalendarListViewModel.ListConsultantsCalendar.Count);
            Assert.Equal(responseModel.ListConsultantsCalendar.First().Id, consultantCalendarListViewModel.ListConsultantsCalendar.First().Id);
            Assert.Equal(responseModel.ListConsultantsCalendar.First().Date, consultantCalendarListViewModel.ListConsultantsCalendar.First().Date);
            Assert.Equal(responseModel.ListConsultantsCalendar.First().ConsultantId, consultantCalendarListViewModel.ListConsultantsCalendar.First().ConsultantId);
        }
    }
}