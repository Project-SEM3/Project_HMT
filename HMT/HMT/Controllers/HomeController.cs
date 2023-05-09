using AspNetCoreHero.ToastNotification.Abstractions;
using HMT.Data;
using HMT.Models;
using HMT.Models.HMTModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace HMT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HMTContext _context;
        private readonly INotyfService _toastNotification;


        public HomeController(ILogger<HomeController> logger, HMTContext context, INotyfService toastNotification)
        {
            _logger = logger;
            _context = context;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            var viewModel = new RequestAllViewModel();
            viewModel.Requests = await _context.Requests
                .Where(r => r.IsDeleted == false)
                  .Include(r => r.RequestDetails)
                    .ThenInclude(r => r.Product)
                  .Include(r => r.User)
                  .AsNoTracking()
                  .OrderBy(m => m.Id)
                  .ToListAsync();

            //Requests request = viewModel.Requests.Single();
            //viewModel.Products = viewModel.RequestDetails.Select(p => p.Product);

            viewModel.Categories = _context.Categories.ToList();
            int totalProducts = _context.Products.Count();

            viewModel.TotalProducts = await _context.TotalProducts.Include(t => t.Product).ToListAsync();
            var totalTotalProducts = viewModel.TotalProducts;
            double totalPrice = 0;
            int totalQuantity = 0;
            if (totalProducts != null)
            {
                foreach (var item in totalTotalProducts)
                {
                    totalPrice += item.TotalPrice;
                    totalQuantity += item.TotalQuantity;
                }
            }

            ViewBag.TotalPrice = totalPrice;
            ViewBag.TotalQuantity = totalQuantity;
            ViewBag.TotalProducts = totalProducts;
            return View(viewModel);
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