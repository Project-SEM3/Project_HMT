using AspNetCoreHero.ToastNotification.Abstractions;
using HMT.Data;
using HMT.EmailService;
using HMT.Models;
using HMT.Models.HMTModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PagedList.Core;
using System.ComponentModel.DataAnnotations;

namespace HMT.Controllers.Admin
{
    [Authorize(Policy = "ElevatedRights")]
    public class RequestsConfirmController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private readonly HMTContext _context;
        private readonly INotyfService _toastNotification;
        private readonly EmailService.IEmailSender _emailSender;

        public RequestsConfirmController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, INotyfService toastNotification, HMTContext context, EmailService.IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            _toastNotification = toastNotification;
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index(int? page, string? searchCodeRequest, char sortStatusRequest = '4')
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var viewModel = new UserRequestModel();

            viewModel.Requests = await _context.Requests
                            .Where(r => r.UserManagerId == user.Id && r.IsDeleted == false
                                    && (string.IsNullOrEmpty(searchCodeRequest) || r.CodeRequest.Contains(searchCodeRequest.ToUpper()))
                                    && (sortStatusRequest == '4' || r.Request_Status == sortStatusRequest))
                            .OrderBy(a => a.Create_at)
                            .Include(r => r.RequestDetails)
                                .ThenInclude(r => r.Product)
                            .Include(r => r.User)
                            .Include(r => r.UserManager)
                            .AsNoTracking()
                            .ToListAsync();

            // Tạo danh sách sản phẩm phân trang
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var requests = viewModel.Requests.ToList().AsQueryable();
            var totalRequests = viewModel.Requests.Count();
            ViewBag.totalPage = (int)Math.Ceiling((double)totalRequests / pageSize);
            var pagedRequests = new PagedList<Models.Request>(requests, pageNumber, pageSize);
            viewModel.PagedRequests = pagedRequests;

            ViewBag.codeRequestValue = searchCodeRequest;
            ViewBag.statusRequestValue = sortStatusRequest;

            return View("~/Views/Admin/RequestsManager/Index.cshtml", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string? codeRequest)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }

            if (codeRequest == null)
            {
                return RedirectToAction("Create");
            }

            var viewModel = new UserRequestDetailModel();

            viewModel.Request = await _context.Requests
                            .Include(r => r.RequestDetails)
                                .ThenInclude(r => r.Product)
                                .ThenInclude(r => r.Category)
                            .Include(r => r.User)
                            .Include(r => r.UserManager)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.CodeRequest == codeRequest);

            Models.Request requests = viewModel.Request;
            viewModel.RequestDetails = requests.RequestDetails;
            viewModel.UserFrom = requests.User;
            viewModel.UserTo = requests.UserManager;


            return View("~/Views/Admin/RequestsManager/Detail.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusRequest(int? requestId, char? statusRequest, bool detailPage = false)
        {
            string code = "";

            if (requestId != null && statusRequest != null)
            {
                var request = _context.Requests.Find(requestId);
                code = request.CodeRequest;

                if (request != null)
                {
                    request.Request_Status = (char)statusRequest;
                    _context.Update(request);
                    _context.SaveChanges();

                    //Send Mail
                    var requestDetailLink = Url.Action("Detail", "Requests", new { request.CodeRequest }, Request.Scheme);
                    var message = new Message(new string[] { "lukelukky1122@gmail.com" }, "Update the status of a request", requestDetailLink, null, null, null);
                    await _emailSender.SendEmailAsync(message);

                    _toastNotification.Success("Successful update");
                }
            }
            else
            {
                _toastNotification.Error("Update failed");
            }

            if ((bool)detailPage)
            {
                return RedirectToAction("Detail", new { codeRequest = code });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CompleteRequest(string? codeRequest, bool? edit)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }

            if (codeRequest == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new UserRequestDetailModel();

            viewModel.Request = await _context.Requests
                            .Include(r => r.RequestDetails)
                                .ThenInclude(r => r.Product)
                                .ThenInclude(r => r.Category)
                            .Include(r => r.User)
                            .Include(r => r.UserManager)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.CodeRequest == codeRequest);

            Models.Request requests = viewModel.Request;
            viewModel.RequestDetails = requests.RequestDetails;
            viewModel.UserFrom = requests.User;
            viewModel.UserTo = requests.UserManager;

            if (edit.HasValue && edit.Value)
            {
                return View("~/Views/Admin/RequestsManager/EditCompleteRequest.cshtml", viewModel);
            }
            else
            {
                return View("~/Views/Admin/RequestsManager/CompleteRequest.cshtml", viewModel);
            }

            //return View("~/Views/Admin/RequestsManager/CompleteRequest.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRequest(int? requestId, int[]? requestDetailIds, double[]? requestDetailPrices)
        {
            try
            {
                var countIds = requestDetailIds.Length;
                var countPrices = requestDetailPrices.Length;
                double totalPrice = requestDetailPrices.Sum();
                if (requestId == null || requestDetailIds == null || requestDetailPrices == null || countIds != countPrices)
                {
                    _toastNotification.Error("Update failed");
                    return RedirectToAction("Index");
                }
                var request = await _context.Requests.Include(r => r.RequestDetails).FirstOrDefaultAsync(r => r.Id == requestId);

                if (request == null) //|| request.Request_Status == '3'
                {
                    _toastNotification.Error("Update failed");
                    return RedirectToAction("Index");
                }

                List<RequestDetail> checkRequestDetails = request.RequestDetails.ToList();

                // Kiểm tra xem có phần tử nào trong requestDetailIds không có trong danh sách Requests hay không
                bool hasIdNotInRequests = requestDetailIds.Any(id => !checkRequestDetails.Any(r => r.Id == id));
                if (hasIdNotInRequests)
                {
                    _toastNotification.Error("Update failed");
                    return RedirectToAction("Index");
                }

                for (int i = 0; i < countIds; i++)
                {
                    var requestDetail = await _context.RequestDetails.FindAsync(requestDetailIds[i]);
                    await UpdateTotalProduct(requestDetail, requestDetailPrices[i]);
                    requestDetail.Price = requestDetailPrices[i];
                    _context.Update(requestDetail);
                    await _context.SaveChangesAsync();
                }

                request.Request_Status = '3';
                request.TotalPrice = totalPrice;
                _context.Update(request);
                await _context.SaveChangesAsync();

                //Send Mail
                var requestDetailLink = Url.Action("Detail", "Requests", new { request.CodeRequest }, Request.Scheme);
                var message = new Message(new string[] { "lukelukky1122@gmail.com" }, "Update the status of a request", requestDetailLink, null, null, null);
                await _emailSender.SendEmailAsync(message);

                _toastNotification.Success("Successful update");
                return RedirectToAction("Detail", new { codeRequest = request.CodeRequest });
            }
            catch (Exception)
            {
                _toastNotification.Error("Update failed");
                return RedirectToAction("Index");
            }

        }

        private async Task UpdateTotalProduct([Required] RequestDetail requestDetail, [Required] double requestDetailPrice)
        {
            var totalProduct = await _context.TotalProducts.Where(p => p.ProductId == requestDetail.ProductId).FirstOrDefaultAsync();

            if (totalProduct != null)
            {
                if (requestDetail.Price == 0)
                {
                    totalProduct.TotalQuantity += requestDetail.Quantity;
                }
                totalProduct.TotalPrice += (double)(requestDetailPrice - requestDetail.Price);
                _context.Update(totalProduct);
                await _context.SaveChangesAsync();
            }
            else
            {
                var totalProductNew = new TotalProduct
                {
                    TotalQuantity = requestDetail.Quantity,
                    TotalPrice = (double)requestDetailPrice,
                    ProductId = requestDetail.ProductId
                };
                _context.Add(totalProductNew);
                await _context.SaveChangesAsync();
            }

        }

        [HttpGet]
        public async Task<IActionResult> AllPurchasedProducts()
        {
            var model = await _context.Categories
                 .Select(category => new AllPurchasedProductsViewModel
                 {
                     Category = category,
                     Products = category.Products,
                     TotalProducts = _context.TotalProducts.Where(tp => tp.Product.CategoryId == category.Id).Include(tp => tp.Product).ToList(),
                 })
                 .AsNoTracking()
                 .OrderBy(u => u.Category)
                 .ToListAsync();


            return View("~/Views/Admin/RequestsManager/AllPurchasedProducts.cshtml", model);
        }
    }
}
