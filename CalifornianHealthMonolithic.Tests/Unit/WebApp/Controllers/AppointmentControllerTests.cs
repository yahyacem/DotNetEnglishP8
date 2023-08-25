using System.Security.Claims;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Tests.Integration;
using CalifornianHealthMonolithic.WebApp.Controllers;
using CalifornianHealthMonolithic.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CalifornianHealthMonolithic.Tests.Unit.WebApp.Controllers
{
    public class AppointmentControllerTests : UnitBase
    {
        public AppointmentControllerTests() : base() {}
        [Fact]
        public async Task Index_Should_Return_AppointmentViewModel()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

    
            AppointmentViewModel appointmentViewModelToReturn = new()
            {
                Id = Convert.ToInt32(appointments.First().Id),
                StartDateTime = appointments.First().StartDateTime,
                EndDateTime = appointments.First().EndDateTime,
                PatientName = $"{patients.First().FName} {patients.First().LName}",
                ConsultantName = $"{consultants.First().FName} {consultants.First().LName}"
            };

            // Set up parameters and instantiate service
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.GetValidTokenAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync("");

            var apiService = new Mock<IAPIService>();
            apiService.Setup(x => x.GetAppointmentViewModelAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(appointmentViewModelToReturn);

            // Instantiate controller
            AppointmentController appointmentControllerController = new(apiService.Object, authenticationService.Object);

            // Act
            var response = await appointmentControllerController.Index(Convert.ToInt32(appointments.First().Id));
                
            // Assert
            
            // Response should be NotFound
            Assert.IsType<ViewResult>(response);
            Assert.Equal(((ViewResult)response).Model, appointmentViewModelToReturn);
        }
    }
}