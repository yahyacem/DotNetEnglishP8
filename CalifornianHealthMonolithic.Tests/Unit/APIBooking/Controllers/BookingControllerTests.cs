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

namespace CalifornianHealthMonolithic.Tests.Unit.APIBooking.Controllers
{
    public class BookingControllerTests : UnitBase
    {
        public BookingControllerTests() : base() {}
        [Fact]
        public async Task Booking_Should_Return_StatusCode500()
        {
            // Arrange

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);

            // Set up parameters and instantiate service
            var bookingService = new Mock<IBookingService>();
            bookingService.Setup(x => x.BookAppointmentAsync(It.IsAny<int>(), It.IsAny<int>()))
                             .ReturnsAsync(value: new BookAppointmentResponseModel()
                             {
                                Status = BookAppointmentResponseModel.StatusType.InternalError
                             });
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));

            // Mock HttpContext and HttpContextAccessor to provide claim with patient Id
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1") }));
            httpContext.Setup(ctx => ctx.User).Returns(user);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(acc => acc.HttpContext).Returns(httpContext.Object);

            // Instantiate booking controller
            BookingController bookingController = new(bookingService.Object, httpContextAccessor.Object);


            // Act
            var response = await bookingController.BookAppointment(1);
                
            // Assert

            // Response should be NotFound
            Assert.IsType<StatusCodeResult>(response);
        }
    }
}