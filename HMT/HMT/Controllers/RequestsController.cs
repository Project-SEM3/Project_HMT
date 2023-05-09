using AspNetCoreHero.ToastNotification.Abstractions;
using Azure.Core;
using HMT.Data;
using HMT.Models;
using HMT.Models.HMTModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;
using PagedList.Core;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing.Printing;
using System.Security.Claims;

namespace HMT.Controllers
{
    public class RequestsController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private readonly HMTContext _context;
        private readonly INotyfService _toastNotification;

        public RequestsController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, INotyfService toastNotification, HMTContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            _toastNotification = toastNotification;
            _context = context;
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
                            .Where(r => r.UserId == user.Id && r.IsDeleted == false
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

            return View(viewModel);
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


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestPostModel requestModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (requestModel.product == null || requestModel.product.Count() < requestModel.quantity.Count())
            {
                _toastNotification.Error("Please select stationery");
                return RedirectToAction("Create");
            }

            // xử lí trùng lặp product
            await DuplicateRequestProcessing(requestModel);

            int totalQty = 0;
            foreach (int item in requestModel.quantity)
            {
                totalQty += item;
            }

            var request = new Models.Request
            {
                Request_Status = '0',
                TotalQuantity = totalQty,
                TotalPrice = 0,
                Create_at = DateTime.Now,
                IsDeleted = false,
                UserId = user.Id,
                UserManagerId = requestModel.manager.ToString()
            };
            _context.Add(request);
            await _context.SaveChangesAsync();


            var countRequests = requestModel.product.Count();
            for (int i = 0; i < countRequests; i++)
            {
                var requestDetails = new RequestDetail
                {
                    Status = '0',
                    Quantity = requestModel.quantity[i],
                    Price = 0,
                    Create_at = DateTime.Now,
                    Note = requestModel.note[i] == null ? "" : requestModel.note[i],
                    ProductId = requestModel.product[i],
                    RequestId = request.Id
                };
                _context.Add(requestDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detail", new { codeRequest = request.CodeRequest });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? codeRequest)
        {
            try
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

                var request = await _context.Requests.FirstOrDefaultAsync(r => r.CodeRequest == codeRequest);
                if (request == null || request.Request_Status != '0')
                {
                    return RedirectToAction("Index");
                }

                ViewBag.requsetId = request.Id;
                ViewBag.codeRequest = codeRequest;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditRequest(RequestPostModel requestModel)
        {
            //return Ok(requestModel);
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }

            if (requestModel.product == null)
            {
                _toastNotification.Error("Please select stationery");
                return RedirectToAction("Edit", new { id = requestModel.requestId });
            }

            int totalQty = 0;
            foreach (int item in requestModel.quantity)
            {
                totalQty += item;
            }

            if (requestModel.requestId == null) return RedirectToAction("Index");

            // xử lí trùng lặp product
            await DuplicateRequestProcessing(requestModel);

            var request = await _context.Requests.FindAsync(requestModel.requestId);
            var requestDetails = await _context.RequestDetails.Where(r => r.RequestId == requestModel.requestId).ToListAsync();

            // lọc và xóa những requestDetails bị người dùng xóa.
            var distinctId = requestDetails.Select(r => r.Id).Distinct();
            var distinctIdsNotInArray = distinctId.Except(requestModel.requestDetails.Cast<int>());
            var countDistinctIdsNotInArray = distinctIdsNotInArray.Count();
            if (countDistinctIdsNotInArray >= 1)
            {
                foreach (var number in distinctIdsNotInArray)
                {
                    if (number >= 1)
                    {
                        var requestDetailDelete = await _context.RequestDetails.FindAsync(number);
                        _context.Remove(requestDetailDelete);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (request == null) return RedirectToAction("Index");

            request.TotalQuantity = totalQty;
            request.UserManagerId = requestModel.manager.ToString();
            _context.Update(request);
            await _context.SaveChangesAsync();


            var countRequests = requestModel.product.Count();
            for (int i = 0; i < countRequests; i++)
            {
                if (requestModel.requestDetails[i] != 0)
                {
                    var requestDetail = await _context.RequestDetails.FindAsync(requestModel.requestDetails[i]);
                    if (request == null) return RedirectToAction("Index");
                    requestDetail.ProductId = requestModel.product[i];
                    requestDetail.Quantity = requestModel.quantity[i];
                    requestDetail.Note = requestModel.note[i] == null ? "" : requestModel.note[i];
                    _context.Update(requestDetail);
                    await _context.SaveChangesAsync();
                }
                else
                {

                    var requestDetail = new RequestDetail
                    {
                        Status = '0',
                        Quantity = requestModel.quantity[i],
                        Price = 0,
                        Create_at = DateTime.Now,
                        Note = requestModel.note[i] == null ? "" : requestModel.note[i],
                        ProductId = requestModel.product[i],
                        RequestId = request.Id
                    };
                    _context.Add(requestDetail);
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction("Detail", new { codeRequest = request.CodeRequest });
        }

        [HttpPost]
        public IActionResult DeleteRequest(UserRequestModel requestModel)
        {
            //return Ok(requestModel);

            if (requestModel.requestId == null) return RedirectToAction("Index");

            Models.Request request = _context.Requests.Find(requestModel.requestId);
            if (request == null || request.Request_Status == '3')
            {
                return RedirectToAction("Index");
            }

            request.IsDeleted = true;

            _context.Update(request);
            _context.SaveChanges();

            _toastNotification.Success("Successful deletion");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public Task DuplicateRequestProcessing([Required] RequestPostModel requestModel)
        {
            //List<int> productIds = new List<int>() { 1, 2, 3, 4, 3, 4, 2, 2, 3, 2, 2, 7, 2, 7 };
            //List<int> requestDetailIds = new List<int>(); //{ 1, 2, 3, 4, 3, 4, 2,3, 2, 2, 2, 7, 2, 7 }
            //List<string>? notes = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "K", "J", "K" };
            //List<int>? quantities = new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            List<int> productIds = requestModel.product;
            List<int> requestDetailIds = requestModel.requestDetails;
            List<string>? notes = requestModel.note;
            List<int>? quantities = requestModel.quantity;

            int productIdsCount = productIds.Count;
            int requestDetailIdsCount = requestDetailIds.Count;
            int notesCount = notes.Count;
            int quantitiesCount = quantities.Count;

            bool areEqual = productIdsCount == notesCount && productIdsCount == quantitiesCount;

            bool hasDuplicates = productIds != null && productIds.GroupBy(x => x).Any(g => g.Count() > 1);
            if (hasDuplicates && areEqual)
            {
                // Duyệt qua danh sách requestDetails từ cuối về đầu 
                // để tránh việc khi xóa sẽ bị đặt lại chỉ mục
                for (int i = productIds.Count - 1; i >= 0; i--)
                {
                    int number = productIds[i];
                    // Lấy tất cả các chỉ mục của số đang xét
                    List<int> indexes = productIds.Select((n, index) => new { Number = n, Index = index })
                                                      .Where(n => n.Number == number)
                                                      .Select(n => n.Index)
                                                      .ToList();
                    // Nếu số đang xét xuất hiện từ lần thứ 2 trở đi
                    if (indexes.Count > 1)
                    {
                        // Xóa số đang xét khỏi danh sách, trừ chỉ mục đầu tiên indexes[0]
                        int index = indexes[0];
                        indexes.RemoveAt(0);
                        foreach (int indexToRemove in indexes.OrderByDescending(x => x))
                        {
                            productIds.RemoveAt(indexToRemove);
                            if (requestDetailIdsCount > 0)
                            {
                                requestDetailIds.RemoveAt(indexToRemove);
                            }
                            notes.RemoveAt(indexToRemove);
                            quantities[index] += quantities[indexToRemove];
                            quantities.RemoveAt(indexToRemove);
                            if (indexToRemove != indexes.First())
                            {
                                i--;
                            }
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers()
        {
            string[] roleNames = { "ADMIN", "DIRECTOR" };

            var usersInRoles = new List<User>();
            foreach (string roleName in roleNames)
            {

                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
                usersInRoles.AddRange(usersInRole);
            }

            return Ok(new
            {
                dataManageres = usersInRoles,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryList = new List<object>();
            foreach (var category in categories)
            {
                var categoryObject = new
                {
                    id = category.Id.ToString(),
                    nameCategory = category.NameCategory,
                };
                categoryList.Add(categoryObject);
            }


            return Ok(new
            {
                dataCategories = categoryList,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int? categoryId)
        {
            var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();


            var productList = new List<object>();
            foreach (var product in products)
            {
                var productObject = new
                {
                    id = product.Id.ToString(),
                    nameProduct = product.NameProduct,
                    categoryId = product.CategoryId.ToString(),
                };
                productList.Add(productObject);
            }

            return Ok(new
            {
                dataProducts = productList

            });
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestDetails(int? requestId)
        {
            object requestObject = new { };

            var requestDetailList = new List<object>();
            if (requestId != null)
            {
                var request = await _context.Requests.FindAsync(requestId);
                requestObject = new
                {
                    id = request.Id.ToString(),
                    status = request.Request_Status,
                    totalQty = request.TotalQuantity,
                    totalPrice = request.TotalPrice,
                    userFrom = request.UserId,
                    userTo = request.UserId,
                };

                var requestDetails = await _context.RequestDetails
                .Where(r => r.RequestId == requestId)
                .Include(r => r.Product)
                    .ThenInclude(p => p.Category)
                .ToListAsync();


                foreach (var requestDetail in requestDetails)
                {
                    var requestDetailObject = new
                    {
                        id = requestDetail.Id.ToString(),
                        categoryId = requestDetail.Product.Category.Id,
                        categoryName = requestDetail.Product.Category.NameCategory,
                        productId = requestDetail.Product.Id,
                        productName = requestDetail.Product.NameProduct,
                        qty = requestDetail.Quantity,
                        note = requestDetail.Note,
                    };
                    requestDetailList.Add(requestDetailObject);
                }
            }


            return Ok(new
            {
                dataRequest = requestObject,
                dataRequestDetails = requestDetailList

            });
        }
    }
}

