using AutoMapper;
using CalifornianHealthMonolithic.APIBooking.Models;
using CalifornianHealthMonolithic.APIBooking.Repositories;
using CalifornianHealthMonolithic.APIBooking.Services;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Mappers;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using Microsoft.Extensions.Logging;

namespace CalifornianHealthMonolithic.Tests.Integration.API
{
    [Collection("Sequential")]
    public class BookingIntegrationTests : IntegrationBase
    {
        public BookingIntegrationTests() : base() {}
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(3000)]
        public async Task Booking_Should_Return_AppointmentsAllSuccess(int quantity)
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(10);
            List<Consultant> consultants = GetSeedConsultants(10);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(quantity, consultants);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.ConsultantCalendars.AddRangeAsync(consultantCalendars);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IBookingRepository bookingRepository = new BookingRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IBookingService bookingService = new BookingService(bookingRepository, mapper);
            
            // Act

            // Stack tasks to run them in parallel
            Random r = new();
            var tasks = consultantCalendars.Select(c => bookingService.BookAppointmentAsync(r.Next(1, patients.Count), c.Id ?? default));
            List<BookAppointmentResponseModel> bookingResponses = new(await Task.WhenAll(tasks));

            // Assert

            // Check if all the responses are success
            Assert.DoesNotContain(bookingResponses, r => r.Status != BookAppointmentResponseModel.StatusType.Success);
            Assert.DoesNotContain(bookingResponses, r => r.Appointment == null);
            Assert.Equal(bookingResponses.Count, quantity);
        }
        [Fact]
        public async Task BookingTwiceSame_Should_Return_AppointmentNotSuccess()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(2);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.ConsultantCalendars.AddRangeAsync(consultantCalendars);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IBookingRepository bookingRepository = new BookingRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IBookingService bookingService = new BookingService(bookingRepository, mapper);

            // Act

            // Book first appointment
            BookAppointmentResponseModel firstAppointment = await bookingService.BookAppointmentAsync(patients[0].Id, consultants[0].Id ?? default);
            BookAppointmentResponseModel secondAppointment = await bookingService.BookAppointmentAsync(patients[1].Id, consultants[0].Id ?? default);

            // Assert

            // Check if second appointment is not success
            Assert.Equal(BookAppointmentResponseModel.StatusType.Success, firstAppointment.Status);
            Assert.NotEqual(BookAppointmentResponseModel.StatusType.Success, secondAppointment.Status);
        }
    }
}