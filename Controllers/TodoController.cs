using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Data;
using webapp.Models;

namespace webapp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<SimpleInterestController> _logger;
        private readonly AppDbContext _dbContext;

        public TodoController(ILogger<SimpleInterestController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        
        public IActionResult Index()
        {
            List<TodoItem> items = _dbContext.TodoItems.ToList();
            return View(items);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TodoItem model)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                
                _dbContext.TodoItems.Add(model);
                _dbContext.SaveChanges();
                ViewBag.Success = "Todo Item Successfully Added";
                return View();
            }
            catch (Exception ex)
            {
                // TODO
                ModelState.AddModelError(ex.Message,null);
                return View();
            }
        }

        [Authorize]
        public IActionResult SingleItem(int Id)
        {
            try
            {
                if (Id <= 0) return RedirectToAction("Index");

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == Id);

                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No item with the id of {Id} in the database";
                    return View();
                }
                
                return View(item);   
            }
            catch (Exception ex)
            {
                // TODO
                ModelState.AddModelError(ex.Message,null);
                return View();
            }
        }

        [Authorize]
        public IActionResult EditItem(int Id)
        {
            try
            {
                if (Id <= 0) return RedirectToAction("Index");

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == Id);

                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No item with the id of {Id} in the database";
                    return View();
                }
                
                return View(item);   
            }
            catch (Exception ex)
            {
                // TODO
                ModelState.AddModelError(ex.Message,null);
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(TodoItem model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == model.Id);

                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No item with the id of {model.Id} in the database";
                    return View();
                }

                item.Name = model.Name;
                item.Description = model.Description;
                item.DueDate = model.DueDate;
                
                _dbContext.TodoItems.Update(item);
                _dbContext.SaveChanges();
                ViewBag.Success = "Todo Item Successfully Updated";
                return View(item);
            }
            catch (Exception ex)
            {
                // TODO
                ModelState.AddModelError(ex.Message,null);
                return View();
            }
        }

        [Authorize]
        public IActionResult DeleteItem(int Id)
        {
            try
            {
                if (Id <= 0) return RedirectToAction("Index");

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == Id);

                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No item with the id of {Id} in the database";
                    return View();
                }
                
                return View(item);   
            }
            catch (Exception ex)
            {
                // TODO
                ModelState.AddModelError(ex.Message,null);
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(TodoItem model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == model.Id);

                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No item with the id of {model.Id} in the database";
                    return View();
                }
                
                _dbContext.TodoItems.Remove(item);
                _dbContext.SaveChanges();
                ViewBag.Success = "Todo Item Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // TODO
                ModelState.AddModelError(ex.Message,null);
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