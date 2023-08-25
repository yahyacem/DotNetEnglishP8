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

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    
    public class MaintenanceController : BaseController
    {
        public MaintenanceController(){}
        public IActionResult Index()
        {
            ViewBag.HideBanner = true;
            return View();
        }
    }
}