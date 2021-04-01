    using Microsoft.AspNetCore.Mvc;
    using AlifAdminMiniMarketV2.Models;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    namespace AlifAdminMiniMarketV2.Controllers
    {
        public class ProductController : Controller
        {
            public DataContext _context;
            public ProductController(DataContext context)
            {
                this._context = context;
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
                var li = _context.Products.OrderByDescending(p => p).Include(p => p.ProductCategory).Where(p=> p.ProductCategory.Category == category).ToList();
                if(category == "Все")
                    li = _context.Products.OrderByDescending(p=> p).Include(p=> p.ProductCategory).ToList();
                return View(li);
            }
            public IActionResult Add()
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Add(Product model,int id)
            {
                var Category = await _context.Categories.SingleAsync(c=> c.Id == id);
                model.ProductCategory = Category;
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                var model = await _context.Products.SingleAsync(p=> p.Id == id);
                _context.Remove(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            public IActionResult Edit(int id)
            {
                var model = _context.Products.Include(p=> p.ProductCategory).Single(p=> p.Id == id);
                var li = _context.Categories.Where(p=> p.Id == model.ProductCategory.Id).ToList();
                foreach (var x in _context.Categories.ToList())
                {
                    li.Add(x);
                }
                ViewBag.Categories = li;
                return View(model);
            }
            [HttpPost]
            public async Task<IActionResult> Edit(Product model,int xid)
            {
                var lastmodel = _context.Products.Single(p=> p.Id == model.Id);
                var Category = _context.Categories.Single(p=> p.Id == xid);
                lastmodel.ProductName = model.ProductName;
                lastmodel.Price = model.Price;
                lastmodel.ProductCategory = Category;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }