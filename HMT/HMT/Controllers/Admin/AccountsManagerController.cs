using AspNetCoreHero.ToastNotification.Abstractions;
using HMT.Data;
using HMT.Models;
using HMT.Models.HMTModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using PagedList.Core;
using HMT.EmailService;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Data;

namespace HMT.Controllers.Admin
{
    [Authorize(Policy = "ElevatedRights")]
    public class AccountsManagerController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private readonly HMTContext _context;
        private readonly INotyfService _toastNotification;
        private readonly EmailService.IEmailSender _emailSender;

        public AccountsManagerController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            INotyfService toastNotification,
            HMTContext context,
            EmailService.IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            _toastNotification = toastNotification;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            // Lấy danh sách người dùng và vai trò của họ
            var usersWithRoles = _userManager.Users.Select(user => new UserWithRolesViewModel
            {
                User = user,
                Roles = (List<string>)_userManager.GetRolesAsync(user).Result
            }).OrderBy(u => u.User).ToList();

            // Tạo danh sách người dùng phân trang
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;
            int pageSize = 8;
            ViewBag.totalPage = (int)Math.Ceiling((double)usersWithRoles.Count() / pageSize);
            PagedList<UserWithRolesViewModel> models = new PagedList<UserWithRolesViewModel>(usersWithRoles.AsQueryable(), pageNumber, pageSize);

            // Chuyển dữ liệu sang view để hiển thị
            return View("~/Views/Admin/AccountsManager/Index.cshtml", models);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var currentUser = await _userManager.FindByIdAsync(id);

            var usersWithRoles = new UserWithRolesViewModel
            {
                User = currentUser,
                Roles = (List<string>)_userManager.GetRolesAsync(currentUser).Result
            };

            return View("~/Views/Admin/AccountsManager/Detail.cshtml", usersWithRoles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            // Gán danh sách các vai trò vào ViewBag
            ViewBag.Roles = new SelectList(roles, "NormalizedName", "Name");
            return View("~/Views/Admin/AccountsManager/Create.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            //Save image to wwwroot/image
            var img = userModel.FrontImage;
            string imgURL = "";
            if (img != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(img.FileName);
                string extension = Path.GetExtension(img.FileName);
                imgURL = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine("wwwroot/img", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
            }

            User user = new User()
            {
                Email = userModel.Email,
                FullName = userModel.FullName,
                UserName = userModel.FullName.Replace(" ", ""),
                PhoneNumber = userModel.PhoneNumber,
                Address = userModel.Address,
                Image = imgURL,
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return RedirectToAction("Create");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { "lukelukky1122@gmail.com" }, "Confirmation email link", confirmationLink, null, user.Email, userModel.Password);
            await _emailSender.SendEmailAsync(message);

            await _userManager.AddToRoleAsync(user, userModel.Rolle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var currentUser = await _userManager.FindByIdAsync(id);
            List<string> currentRoles = (List<string>)_userManager.GetRolesAsync(currentUser).Result;

            var usersWithRoles = new UserWithRolesViewModel
            {
                User = currentUser,
                Roles = currentRoles
            };
            var roles = await _roleManager.Roles.ToListAsync();

            ViewBag.Roles = roles;

            return View("~/Views/Admin/AccountsManager/Edit.cshtml", usersWithRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserWithRolesViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.Error("Failed Edition");
                return RedirectToAction("Edit", new { id = userModel.User.Id });
            }

            try
            {
                var userEdit = userModel.User;
                var user = await _userManager.FindByIdAsync(userEdit.Id);
                //Save image to wwwroot/image
                var img = userEdit.FrontImage;
                string imgURL = "";
                if (img == null || img.ToString() == user.Image)
                {
                    imgURL = user.Image;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(img.FileName);
                    string extension = Path.GetExtension(img.FileName);
                    imgURL = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine("wwwroot/img", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }

                    // Xóa ảnh cũ nếu tồn tại
                    string oldImagePath = Path.Combine("wwwroot/img", user.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                //Update User
                user.Image = imgURL;
                user.FullName = userEdit.FullName;
                user.PhoneNumber = userEdit.PhoneNumber;
                user.Address = userEdit.Address;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    _toastNotification.Error("Failed Edition");
                    return RedirectToAction("Edit", new { id = userModel.User.Id });
                }

                var oldRoles = await _userManager.GetRolesAsync(user);
                var res = await _userManager.RemoveFromRolesAsync(user, oldRoles);
                await _userManager.AddToRoleAsync(user, userModel.Roles?.FirstOrDefault());

                //Send Mail
                var accountDetailLink = Url.Action("Index", "Accounts", Request.Scheme);
                var message = new Message(new string[] { "lukelukky1122@gmail.com" }, "Your account has been updated", accountDetailLink, null, null, null);
                await _emailSender.SendEmailAsync(message);

            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Edit", new { id = userModel.User.Id });
            }
            _toastNotification.Success("Successful update");
            return RedirectToAction("Detail", new { id = userModel.User.Id });
        }

        //private async Task DeleteRolesAsync(List<string> deleteList, User? user)
        //{
        //    if (user != null)
        //    {
        //        foreach (var roleName in deleteList)
        //        {
        //            await _userManager.RemoveFromRoleAsync(user, roleName);
        //        }
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(string? userId)
        {
            //return Ok(userId);

            if (userId == null) return RedirectToAction("Index");

            List<Request> request = _context.Requests.Where(r => r.UserId == userId || r.UserManagerId == userId).ToList();
            if (request == null || request.Count > 0)
            {
                _toastNotification.Error("Failed deletion");
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    string oldImagePath = Path.Combine("wwwroot/img", user.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    _toastNotification.Success("Successful deletion");
                    return RedirectToAction("Index");
                }
            }
            _toastNotification.Error("Failed deletion");
            return RedirectToAction("Index");

        }
    }
}
