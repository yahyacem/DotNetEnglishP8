using CalifornianHealthMonolithic.Shared.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CalifornianHealthMonolithic.APIConsultant.Services;

namespace CalifornianHealthMonolithic.APIBooking.Controllers
{
    [Route("[controller]")]
    public class ConsultantController : Controller
    {
        private readonly IConsultantService _consultantService;
        public ConsultantController(IConsultantService consultantService) 
        {
            _consultantService = consultantService;
        }
        [HttpGet("/Consultants/{id}/Calendar")]
        public async Task<IActionResult> GetConsultantCalendarByConsultantIdAsync(int id) 
        {
            var consultantCalendar = await _consultantService.GetConsultantCalendarByConsultantIdAsync(id);
            return Ok(consultantCalendar);
        }
        [HttpGet("/Consultants/Names")]
        public async Task<IActionResult> GetConsultantsNamesAsync()
        {
            var consultants = await _consultantService.GetConsultantsNamesAsync();
            return Ok(consultants);
        }
        [HttpGet("/Consultants")]
        public async Task<IActionResult> GetConsultantsAsync()
        {
            var consultants = await _consultantService.GetConsultantsAsync();
            return Ok(consultants);
        }
        [HttpGet("/Calendar/{id}")]
        public async Task<IActionResult> GetConsultantCalendarByIdAsync(int id)
        {
            var consultantCalendar = await _consultantService.GetConsultantCalendarByIdAsync(id);
            return Ok(consultantCalendar);
        }
    }
}