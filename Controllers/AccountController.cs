using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.Users;
using GCMS.Web.Models.ViewModels.Account;
using GCMS.Web.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GCMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly GCMSContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly IWebHostEnvironment _iwebHostEnvironment;
        private readonly SaveProfileImage _saveprofileimage;

        public AccountController(GCMSContext GCMSContext, IWebHostEnvironment iwebHostEnvironment , SaveProfileImage SaveProfileImage)
        {
            _context = GCMSContext;
            _iwebHostEnvironment = iwebHostEnvironment;
            _saveprofileimage = SaveProfileImage;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return Content("Error 404");
            }
            var usr = _context.Users.FirstOrDefault(u => u.Username == user.Username && !u.Deleted && u.UserIsActived);
            if (usr == null)
            {
                return Content("اطلاعات کاربر یافت نشد");
            }
            var res = _passwordHasher.VerifyHashedPassword(usr, usr.Password, user.Password);
            if (res == PasswordVerificationResult.Failed)
            {
                return Content("پسورد اشتباه می باشد");
            }
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,usr.UserID.ToString()),
                new Claim(ClaimTypes.Name,usr.Username),
            };
            var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Princple = new ClaimsPrincipal(Identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Princple);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return Content("اطلاعات نا مغتبر می باشد");
            }
            var usr = _context.Users.Any(u => u.Username == user.Username && !u.Deleted);
            if (usr)
            {
                return Content("نام کاربری قبلا انتخاب شده است");
            }
            var euser = new User();
            euser.FirstName = user.FirstName;
            euser.LastName = user.LastName;
            euser.Email = user.Email;
            euser.Password = _passwordHasher.HashPassword(euser, user.Password);
            euser.Username = user.Username;
            if (user.UserProfileImage != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(user.UserProfileImage.FileName);
                string path = Path.Combine(_iwebHostEnvironment.WebRootPath, "Image", "Profile", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path!));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await user.UserProfileImage.CopyToAsync(stream);
                }
                euser.UserProfileImageName = filename;
            }
            await _context.Users.AddAsync(euser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        //Profile Actions:
        //تمامی اکشن هایی که مربوط به پروفایل می باشد
        //نمایش اطلاعات کاربری
        [HttpGet]
        public  IActionResult Profile()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userID = int.Parse(id);
            if (!int.TryParse(id, out int idd))
            {
                return RedirectToAction("Login", "Account");
            }
            var usr = _context.Users.FirstOrDefault(u => u.UserID == userID);
            var uvm = new ProfileViewModel();
            uvm.UserID = usr.UserID;
            uvm.FirstName = usr.FirstName;
            uvm.LastName = usr.LastName;
            uvm.Username = usr.Username;
            uvm.Email = usr.Email;
            uvm.UserProfileImageName = usr.UserProfileImageName;
            uvm.UserCreationDate = usr.UserCreationDate;
            uvm.UserIsActived = usr.UserIsActived;
            return View(uvm);

        }
        //ویرایش اطلاعات کاربری 
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel user)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userID, out int ID))
            {
                return RedirectToAction("Login", "Account");
            }
            var usr = _context.Users.FirstOrDefault(u => u.UserID == ID && !u.Deleted);

            if (usr == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View("Profile", user);
            }

            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.Username = user.Username;
            usr.Email = user.Email;

            if(user.UserProfileImageName != null && user.UserProfileImageName.Length > 0)
            {
                usr.UserProfileImageName = await _saveprofileimage.SaveProfileImages(user.UserProfileImage);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "Account");
        }
        //تغییر رمز عبور
        [HttpPost]
        public async Task<IActionResult>ChangePassword(ChangePasswordViewModel changepassword)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Views/General/Error.cshtml");
            }
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userID = int.Parse(id);
            if(!int.TryParse(id,out int idd))
            {
                return Content("شناسه کاربر نا معتبر می باشد.");
            }
            var usr = _context.Users.FirstOrDefault(u => u.UserID == userID && !u.Deleted && u.UserIsActived);
            if(usr ==null)
            {
                return Content("کاربر یافت نشد");
            }
            var res = _passwordHasher.VerifyHashedPassword(usr, usr.Password, changepassword.CurrentPassword);
            if( res == PasswordVerificationResult.Failed)
            {
                return Content("پسورد وارد شده اشتباه می باشد.");
            }
            usr.Password = _passwordHasher.HashPassword(usr, changepassword.NewPassword);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", "Account");
        }

    }
}
