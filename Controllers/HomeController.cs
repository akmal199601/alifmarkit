using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AlifAdminMiniMarketV2.Models;
using Microsoft.EntityFrameworkCore;

namespace AlifAdminMiniMarketV2.Controllers
{
    public class HomeController : Controller
    {
        public DataContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            this._context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _context.Categories.ToList();
            var li = _context.Products.OrderByDescending(p => p).Include(p => p.ProductCategory).ToList();
            return View(li);
        }

        [HttpPost]
        public IActionResult Index(string category)
        {
            ViewBag.Categories = _context.Categories.ToList();
            var li = _context.Products.OrderByDescending(p => p).Include(p => p.ProductCategory).Where(p => p.ProductCategory.Category == category).ToList();
            if (category == "Все")
                li = _context.Products.OrderByDescending(p => p).Include(p => p.ProductCategory).ToList();
            return View(li);
        }

        [HttpGet]
        public IActionResult ToBasket(int id)
        {
            ViewBag.Id = _context.Products.Single(a => a.Id == id).Id;
            return View();
        }

        [HttpPost]
        public IActionResult ToBasket(Basket model,int sid)
        {
            var ProductModel = _context.Products.Single(p=> p.Id == sid);
            model.Product = ProductModel;
            model.Id = 0;
            _context.Basket.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Basket");
        }
        public IActionResult Basket()
        {
            var li = _context.Basket.Include(p=> p.Product).ToList();
            return View(li);
        }
        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            var model = _context.Basket.Single(p=> p.Id == id);
            _context.Basket.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Basket");
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
