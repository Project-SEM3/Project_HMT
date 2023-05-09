using AspNetCoreHero.ToastNotification.Abstractions;
using HMT.Data;
using HMT.Models;
using HMT.Models.HMTModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Principal;

namespace HMT.Controllers
{
    public class AccountsController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private readonly HMTContext _context;
        private readonly INotyfService _toastNotification;

        public AccountsController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, INotyfService toastNotification, HMTContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            _toastNotification = toastNotification;
            _context = context;
        }



        public async Task<ActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var usersWithRoles = new UserWithRolesViewModel
            {
                User = currentUser,
                Roles = (List<string>)_userManager.GetRolesAsync(currentUser).Result
            };

            return View(usersWithRoles);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginView model, string? returnUrl)
        {
            bool isPersistent = true; // Lưu cookie = true
            bool lockoutOnFailure = false; //  Khóa tk nếu đăng nhập sai nhiều lần = false

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                _toastNotification.Error("Incorrect email!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent, lockoutOnFailure);

            var role = await _userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                if (role.FirstOrDefault() == "Admin" || role.FirstOrDefault() == "Director")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Create", "Requests");
                }
            }
            else if (result.IsLockedOut)
            {
                // user is locked out
                _toastNotification.Error("Your account is locked out!");
            }
            else if (result.IsNotAllowed)
            {
                _toastNotification.Error("Your account is not verified!");
            }
            else
            {
                // authentication failed
                _toastNotification.Error("Incorrect Email or Password!");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View();

            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction("Login", "Accounts");
        }

        public string AccessDenied(string ReturnUrl)
        {
            return $"Bạn không có quyền truy cập vào đường dẫn {ReturnUrl}";
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _toastNotification.Success("Change password successfully");
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    _toastNotification.Warning("Please log in to use this feature");
                    return RedirectToAction("Login", "Accounts");
                }
            }
            _toastNotification.Error("Password change failed");
            return View();
        }
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Profile()
        //{
        //    var userId = User.FindFirstValue("UserId");
        //    if (userId == null)
        //    {
        //        _toastNotification.Warning("Please log in to use this feature");
        //        return RedirectToAction("Login", "Accounts");
        //    }
        //    var user = _context.Users.Find(int.Parse(userId));
        //    return View(user);
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EditProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EditProfile(User userEdit)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _toastNotification.Warning("Please log in to use this feature");
                return RedirectToAction("Login", "Accounts");
            }
            var user = await _userManager.GetUserAsync(User);
            try
            {
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

                // cập nhật thông tin người dùng
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
                    return View(userEdit);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            _toastNotification.Success("Successful profile editing");
            return RedirectToAction("Index");
        }

        public IActionResult CheckUserLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok();
            }

            _toastNotification.Warning("Please log in to use this feature");
            return RedirectToAction("Login", "Accounts");
        }

    }
}
