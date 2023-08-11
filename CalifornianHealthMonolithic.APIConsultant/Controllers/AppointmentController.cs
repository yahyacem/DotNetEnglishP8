using CalifornianHealthMonolithic.Shared.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CalifornianHealthMonolithic.APIConsultant.Services;
using Microsoft.AspNetCore.Authorization;

namespace CalifornianHealthMonolithic.APIBooking.Controllers
{
    [Route("[controller]")]
    public class AppoinmentController : Controller
    {
        private readonly IConsultantService _consultantService;
        public AppoinmentController(IConsultantService consultantService) 
        {
            _consultantService = consultantService;
        }
        /// <summary>Get Appointment record with the specified Id</summary>
        /// <param name="id">Id of the appointment to return</param>
        /// <returns>Returns Appointment record</returns>
        [HttpGet("/Appointment/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAppointmentByIdAsync(int id) 
        {
            // Get requested appointment model
            var appointment = await _consultantService.GetAppointmentModelByIdAsync(id);

            // If null, means that it doesn't exist
            if (appointment == null)
            {
                return NotFound();
            }
            
            // Return Ok response with the requested appointment
            return Ok(appointment);
        }
    }
}