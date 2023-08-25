using CalifornianHealthMonolithic.Shared.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CalifornianHealthMonolithic.APIBooking.Services;
using CalifornianHealthMonolithic.APIBooking.Models;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CalifornianHealthMonolithic.APIBooking.Controllers
{
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookingController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor) 
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;

        }
        /// <summary>Books a new appointment by creating an Appointment record and changing the availability of the ConsultantCalendar to false</summary>
        /// <param name="id">Id of the ConsultantCalendar record</param>
        [HttpPost("/Booking/{id}")]
        [Authorize]
        public async Task<IActionResult> BookAppointment(int id)
        {
            // Get access token from user claims
            var userIdFromToken = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // If null, return unauthorized
            if (userIdFromToken == null)
            {
                return Unauthorized();
            }

            // Parse from string to int
            var userId = int.Parse(userIdFromToken);

            // Book appointment
            BookAppointmentResponseModel response = await _bookingService.BookAppointmentAsync(userId, id);

            // Check result of booking and return a response matching the result
            return response.Status switch
            {
                BookAppointmentResponseModel.StatusType.Success => Ok(response.Appointment),
                BookAppointmentResponseModel.StatusType.NotFound => NotFound(),
                BookAppointmentResponseModel.StatusType.NotAvailable => BadRequest("This booking is not available."),
                _ => StatusCode(500),
            };
        }
    }
}