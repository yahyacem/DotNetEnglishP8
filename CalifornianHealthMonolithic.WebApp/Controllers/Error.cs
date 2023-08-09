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
using Microsoft.AspNetCore.Diagnostics;

namespace CalifornianHealthMonolithic.WebApp.Controllers
{
    
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")] 
        public IActionResult Error(int statusCode) 
        {     
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            return View(new ErrorViewModel { StatusCode = statusCode, OriginalPath = feature?.OriginalPath }); 
        } 
    }
}