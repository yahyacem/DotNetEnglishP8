using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;
using CalifornianHealthMonolithic.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IAuthenticationService = CalifornianHealthMonolithic.Shared.IAuthenticationService;
using System.Net;

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly IAPIService _apiService;
        private readonly IAuthenticationService _authenticationService;
        public BookingController(IAPIService apiService, IAuthenticationService authenticationService)
        {
            _apiService = apiService;
            _authenticationService = authenticationService;
        }
        // GET /Booking/ConsultantCalendar
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConsultantCalendar()
        {
            try
            {
                var consultantNamesViewModel = await _apiService.GetConsultantNames();
                return View(model: consultantNamesViewModel);
            } catch (Exception)
            {
                return RedirectToAction("Index", "Maintenance", new { area = "" });
            }
        }
        // GET /Booking/CreateAppointment/{id}
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateAppointment(int id)
        {
            try
            {
                var accessToken = await _authenticationService.GetValidTokenAsync(User);
                var consultantNamesViewModel = await _apiService.GetConsultantCalendarViewModel(id, accessToken);
                return consultantNamesViewModel != null ? View(model: consultantNamesViewModel) : RedirectToAction("Index", "NotFound", new { area = "" });
            } catch (Exception)
            {
                return RedirectToAction("Index", "Maintenance", new { area = "" });
            }
        }
        // GET /Booking/Appointment/{id}
        [HttpPost("/Booking/Appointment/{id}")]
        [Authorize]
        public async Task<IActionResult> ConfirmAppointment(int id)
        {
            try
            {
                // Get the access token value
                var accessToken = await _authenticationService.GetValidTokenAsync(User);
                var response = await _apiService.BookAppointment(id, accessToken);
                AppointmentViewModel appointmentViewModel = (AppointmentViewModel)response.Result;

                if (response.Response.IsSuccessStatusCode)
                {
                    return Redirect($"/Appointment/{appointmentViewModel.Id}");
                }

                return View("Error", new ErrorViewModel { StatusCode = (int)response.Response.StatusCode, OriginalPath = Request.Path });

            } catch (Exception)
            {
                return RedirectToAction("Index", "Maintenance", new { area = "" });
            }
        }
        // GET /Booking/Calendar/{id}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Calendar(int id)
        {
            ConsultantCalendarListViewModel consultantCalendarListViewModel = new()
            {
                ListConsultantsCalendar = await _apiService.GetListConsultantCalendarViewModel(id)
            };
            return PartialView("_CalendarPartialView", consultantCalendarListViewModel);
        }
    }
}