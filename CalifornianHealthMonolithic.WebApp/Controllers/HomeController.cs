using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;
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

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAPIService _apiService;
        public HomeController(IAPIService apiService, IHttpClientFactory httpClientFactory)
        {
            _apiService = apiService;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var consultantListViewModel = await _apiService.GetConsultantListViewModel();
            return View(model: consultantListViewModel);
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