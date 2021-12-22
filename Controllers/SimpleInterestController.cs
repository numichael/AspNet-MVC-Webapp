using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    public class SimpleInterestController : Controller
    {
        private readonly ILogger<SimpleInterestController> _logger;

        public SimpleInterestController(ILogger<SimpleInterestController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

         [HttpPost]
        public IActionResult index(SimpleInterestModel model)
        {
            try
            {
                if(!ModelState.IsValid) return View();
                int interest = (int.Parse(model.Price) * 10 * int.Parse(model.Time)) / 100;

                //ViewBag.Interest = interest;

                SimpleInterestModel vm = new SimpleInterestModel()
                {
                    Interest = interest.ToString()
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
        }


        [HttpPost]
        public IActionResult Interest(SimpleInterestModel model)
        {
            try
            {
                if(!ModelState.IsValid) return View();
                int interest = (int.Parse(model.Price) * 10 * (int.Parse(model.Time)*12)) / 100;

                //ViewBag.Interest = interest;

                SimpleInterestModel vm = new SimpleInterestModel()
                {
                    Price = model.Price,
                    Time = model.Time,
                    Interest = interest.ToString()
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}