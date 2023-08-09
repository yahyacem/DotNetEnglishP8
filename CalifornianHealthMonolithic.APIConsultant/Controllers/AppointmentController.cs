using CalifornianHealthMonolithic.Shared.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;
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
        [HttpGet("/Appointment/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAppointmentByIdAsync(int id) 
        {
            var consultantCalendar = await _consultantService.GetAppointmentViewModelByIdAsync(id);
            if (consultantCalendar == null)
            {
                return NotFound();
            }
            
            return Ok(consultantCalendar);
        }
    }
}