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

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAPIService _apiService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(IAPIService apiService, IAuthenticationService authenticationService, ILogger<AppointmentController> logger)
        {
            _apiService = apiService;
            _authenticationService = authenticationService;
            _logger = logger;
        }
        [HttpGet("/Appointment/{id}")]
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                // Get the access token value
                var accessToken = await _authenticationService.GetValidTokenAsync(User);
                var appointmentViewModel = await _apiService.GetAppointmentViewModelAsync(id, accessToken);
                return appointmentViewModel != null ? View(model: appointmentViewModel) : RedirectToAction("Index", "NotFound", new { area = "" });
            } catch (Exception)
            {
                return RedirectToAction("Index", "Maintenance", new { area = "" });
            }
        }
    }
}