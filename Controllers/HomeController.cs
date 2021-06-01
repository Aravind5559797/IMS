using IMS.Data;
using IMS.Models;
using IMS.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {


            //Dashboard dashboard = new Dashboard()
            //{
            //    Shops = _db.Shop.Include(i => i.Products).ToList(),
            //    Orders = _db.Order.ToList(),
            //    ProductNum = _db.Product.Count(),
            //    Platforms=_db.Platform.ToList()

            //};




            return View();
        }


        public IActionResult Dashboard()
        {


            DashboardJson dashboard = new DashboardJson()
            {
                Shops=_db.Shop.Include(i=>i.Products).Include(i=>i.Platform).ToList(),
                Products=_db.Product.Count(),
                Orders=_db.Order.Count()

            };


            var response = Newtonsoft.Json.JsonConvert.SerializeObject(dashboard, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(response);
        }









        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
