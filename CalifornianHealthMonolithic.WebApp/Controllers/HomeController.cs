using CalifornianHealthMonolithic.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.WebApp.Services;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    
    public class HomeController : BaseController
    {
        private readonly IAPIService _apiService;
        public HomeController(IAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            // If web app tries to reach an api, but is not available, redirect to maintenance page
            try
            {
                var consultantListViewModel = await _apiService.GetConsultantListViewModel();
                return View(model: consultantListViewModel);
            } catch (Exception ex) when (ex is SocketException ||
                                         ex is HttpRequestException)
            {
                return RedirectToAction("Index", "Maintenance", new { area = "" });
            }
            
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}