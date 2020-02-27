using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.DTOs;
using Web.Models;
using Web.Models.Interface;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMachineApiModel _machineApiModel;

        public HomeController(ILogger<HomeController> logger, IMachineApiModel machineApiModel)
        {
            _logger = logger;
            _machineApiModel = machineApiModel;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _machineApiModel.GetProducts());
        }
        public async Task<(string,string)> OnPostAsync([FromBody]Inserted inserted)
        {
            return await _machineApiModel.Buy(inserted);
        }
             
    }
}
