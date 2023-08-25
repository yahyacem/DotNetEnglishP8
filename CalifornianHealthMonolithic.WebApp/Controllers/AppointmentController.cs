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
using System.Net.Sockets;

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IAPIService _apiService;
        private readonly IAuthenticationService _authenticationService;
        public AppointmentController(IAPIService apiService, IAuthenticationService authenticationService)
        {
            _apiService = apiService;
            _authenticationService = authenticationService;
        }
        [HttpGet("/Appointment/{id}")]
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            // If web app tries to reach an api, but is not available, redirect to maintenance page
            try
            {
                // Get the access token value
                var accessToken = await _authenticationService.GetValidTokenAsync(User);
                // Get appointment from api
                var appointmentViewModel = await _apiService.GetAppointmentViewModelAsync(id, accessToken);
                return appointmentViewModel != null ? View(model: appointmentViewModel) : RedirectToAction("Index", "NotFound", new { area = "" });
            } catch (Exception ex) when (ex is SocketException ||
                                         ex is HttpRequestException)
            {
                return RedirectToAction("Index", "Maintenance", new { area = "" });
            }
        }
    }
}