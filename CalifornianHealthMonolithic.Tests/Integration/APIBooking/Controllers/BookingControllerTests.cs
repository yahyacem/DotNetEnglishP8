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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalifornianHealthMonolithic.Tests.Integration.APIBooking.Controllers
{
    [Collection("Sequential")]
    public class BookingControllerTests : IntegrationBase
    {
        public BookingControllerTests() : base() {}
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
            List<Patient> patients = GetSeedPatients(1);
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

            // Mock HttpContext and HttpContextAccessor to provide claim with patient Id
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1") }));
            httpContext.Setup(ctx => ctx.User).Returns(user);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(acc => acc.HttpContext).Returns(httpContext.Object);

            // Instantiate booking controller
            BookingController bookingController = new(bookingService, httpContextAccessor.Object);
            
            // Act

            // Stack tasks to run them in parallel
            var listResponses = new List<ObjectResult>();
            for (int i = 0; i < quantity; i++)
            {
                listResponses.Add((ObjectResult) await bookingController.BookAppointment(consultantCalendars.First().Id ?? default));
            }

            // Assert

            // Check if only one of the responses is success and remaining are bad request
            Assert.Single(listResponses.Where(r => r.GetType() == typeof(OkObjectResult)));
            Assert.Equal(quantity - 1, listResponses.Count(r => r.GetType() == typeof(BadRequestObjectResult)));
        }
        [Fact]
        public async Task BookingTwiceSame_Should_Return_BadRequest()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
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

            // Mock HttpContext and HttpContextAccessor to provide claim with patient Id
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1") }));
            httpContext.Setup(ctx => ctx.User).Returns(user);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(acc => acc.HttpContext).Returns(httpContext.Object);

            // Instantiate booking controller
            BookingController bookingController = new(bookingService, httpContextAccessor.Object);

            // Act

            // Book first appointment
            ObjectResult firstObjectResult = (ObjectResult) await bookingController.BookAppointment(consultantCalendars[0].Id ?? default);
            // Book second appointment
            ObjectResult secondObjectResult = (ObjectResult) await bookingController.BookAppointment(consultantCalendars[0].Id ?? default);

            // Assert

            // Check if second appointment is not success
            Assert.Equal(200, firstObjectResult.StatusCode);
            Assert.NotEqual(200, secondObjectResult.StatusCode);
        }
        [Fact]
        public async Task Booking_Should_Return_NotFound()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IBookingRepository bookingRepository = new BookingRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IBookingService bookingService = new BookingService(bookingRepository, mapper);

            // Mock HttpContext and HttpContextAccessor to provide claim with patient Id
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1") }));
            httpContext.Setup(ctx => ctx.User).Returns(user);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(acc => acc.HttpContext).Returns(httpContext.Object);

            // Instantiate booking controller
            BookingController bookingController = new(bookingService, httpContextAccessor.Object);

            // Act

            // Book first appointment
            var objectResult = await bookingController.BookAppointment(consultantCalendars[0].Id ?? default);

            // Assert

            // Check if second appointment is not success
            Assert.IsType<NotFoundResult>(objectResult);
        }
        [Fact]
        public async Task BookingWithoutClaim_Should_Return_Unauthorized()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
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

            // Mock HttpContext and HttpContextAccessor to provide an empty claim
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(Array.Empty<Claim>()));
            httpContext.Setup(ctx => ctx.User).Returns(user);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(acc => acc.HttpContext).Returns(httpContext.Object);
            
            // Instantiate booking controller
            BookingController bookingController = new(bookingService, httpContextAccessor.Object);

            // Act

            // Book appointment
            var objectResult = await bookingController.BookAppointment(consultantCalendars[0].Id ?? default);
            // Assert

            // Check if all the responses are success
            Assert.IsType<UnauthorizedResult>(objectResult);
        }
    }
}