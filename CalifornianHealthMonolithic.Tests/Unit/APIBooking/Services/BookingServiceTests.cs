using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using CalifornianHealthMonolithic.APIBooking.Controllers;
using CalifornianHealthMonolithic.APIBooking.Models;
using CalifornianHealthMonolithic.APIBooking.Repositories;
using CalifornianHealthMonolithic.APIBooking.Services;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Mappers;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Tests.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalifornianHealthMonolithic.Tests.Unit.APIBooking.Services
{
    public class BookingServiceTests : UnitBase
    {
        public BookingServiceTests() : base() {}
        [Fact]
        public async Task Booking_Should_Return_NotFound()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);

            // Set up parameters and instantiate service
            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository.Setup(x => x.GetConsultantCalendarByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: null);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IBookingService bookingService = new BookingService(bookingRepository.Object, mapper);

            // Act
            BookAppointmentResponseModel response = await bookingService.BookAppointmentAsync(1, 1);
                
            // Assert

            // Response should be NotFound
            Assert.Equal(BookAppointmentResponseModel.StatusType.NotFound, response.Status);
        }
        [Fact]
        public async Task Booking_Should_Return_InternalError()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);

            // Set up parameters and instantiate service
            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository.Setup(x => x.GetConsultantCalendarByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: consultantCalendars.First());
            bookingRepository.Setup(x => x.UpdateConsultantCalendarAsync(It.IsAny<ConsultantCalendar>()))
                             .ReturnsAsync(value: null);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IBookingService bookingService = new BookingService(bookingRepository.Object, mapper);

            // Act
            BookAppointmentResponseModel response = await bookingService.BookAppointmentAsync(1, 1);
                
            // Assert

            // Response should be NotFound
            Assert.Equal(BookAppointmentResponseModel.StatusType.InternalError, response.Status);
        
            // Arrange

            patients = GetSeedPatients(1);
            consultants = GetSeedConsultants(1);
            consultantCalendars = GetSeedConsultantCalendars(1, consultants);

            // Set up parameters and instantiate service
            bookingRepository = new Mock<IBookingRepository>();
            bookingRepository.Setup(x => x.GetConsultantCalendarByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: consultantCalendars.First());
            bookingRepository.Setup(x => x.UpdateConsultantCalendarAsync(It.IsAny<ConsultantCalendar>()))
                             .ReturnsAsync(value: consultantCalendars.First());
            bookingRepository.Setup(x => x.CreateAppointmentAsync(It.IsAny<Appointment>()))
                             .ReturnsAsync(value: null);
            bookingService = new BookingService(bookingRepository.Object, mapper);

            // Act
            response = await bookingService.BookAppointmentAsync(1, 1);
                
            // Assert

            // Response should be InternalError
            Assert.Equal(BookAppointmentResponseModel.StatusType.InternalError, response.Status);
        }
        [Fact]
        public async Task Booking_Should_Return_NotAvailable()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            consultantCalendars.First().Available = false;

            // Set up parameters and instantiate service
            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository.Setup(x => x.GetConsultantCalendarByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: consultantCalendars.First());
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IBookingService bookingService = new BookingService(bookingRepository.Object, mapper);

            // Act
            BookAppointmentResponseModel response = await bookingService.BookAppointmentAsync(1, 1);
                
            // Assert

            // Response should be NotAvailable
            Assert.Equal(BookAppointmentResponseModel.StatusType.NotAvailable, response.Status);
        }
        [Fact]
        public async Task Booking_Should_Return_Null()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

            // Set up parameters and instantiate service
            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository.Setup(x => x.GetConsultantCalendarByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: consultantCalendars.First());
            bookingRepository.Setup(x => x.UpdateConsultantCalendarAsync(It.IsAny<ConsultantCalendar>()))
                             .ReturnsAsync(value: consultantCalendars.First());
            bookingRepository.Setup(x => x.CreateAppointmentAsync(It.IsAny<Appointment>()))
                             .ReturnsAsync(value: appointments.First());
            bookingRepository.Setup(x => x.GetConsultantByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: consultants.First());
            bookingRepository.Setup(x => x.GetPatientByIdAsync(It.IsAny<int>()))
                             .ReturnsAsync(value: patients.First());
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<AppointmentViewModel?>(It.IsAny<Appointment>()))
                  .Returns(value: null);
            IBookingService bookingService = new BookingService(bookingRepository.Object, mapper.Object);

            // Act
            BookAppointmentResponseModel response = await bookingService.BookAppointmentAsync(1, 1);
                
            // Assert

            // Response should be null
            Assert.Null(response);
        }
    }
}