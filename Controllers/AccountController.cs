using GCMS.Web.Areas.Admin.Services.Email;
using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.Users;
using GCMS.Web.Models.ViewModels.Account;
using GCMS.Web.Services.Jwt;
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
    //[Authorize(Roles ="مدیریت سامانه")]
    public class AccountController : Controller
    {
        private readonly GCMSContext _context;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _iemailService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly IWebHostEnvironment _iwebHostEnvironment;

        public AccountController(GCMSContext GCMSContext, IWebHostEnvironment iwebHostEnvironment , IEmailService IEmailService
            , IJwtService IJwtService
            )
        {
            _context = GCMSContext;
            _iwebHostEnvironment = iwebHostEnvironment;
            _iemailService = IEmailService;
            _jwtService = IJwtService;
        }
        //ورود به حساب
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }
        [HttpPost]
        public IActionResult LoginJwt(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("اطلاعات وارد شده معتبر نیست");
            }

            var usr = _context.Users.FirstOrDefault(u =>
                u.Username == user.Username &&
                !u.Deleted &&
                u.UserIsActived);

            if (usr == null)
            {
                return Unauthorized("اطلاعات کاربر یافت نشد");
            }

            var res = _passwordHasher.VerifyHashedPassword(
                usr, usr.Password, user.Password);

            if (res == PasswordVerificationResult.Failed)
            {
                return Unauthorized("پسورد اشتباه می‌باشد");
            }

            var token = _jwtService.GenerateToken(
                usr.UserID,
                usr.Username,
                "مدیریت سامانه");

            return Ok(new
            {
                AccessToken = token,
                TokenType = "Bearer"
            });
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
                new Claim(ClaimTypes.Role, "مدیریت سامانه")
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
            
            if (!int.TryParse(id, out int idd))
            {
                return RedirectToAction("Login", "Account");
            }
            int userID = int.Parse(id);
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
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(user.UserProfileImage.FileName);
                var path = Path.Combine(_iwebHostEnvironment.WebRootPath, "Image", "Profile", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    user.UserProfileImage.CopyToAsync(stream);
                }
                user.UserProfileImageName = filename;
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
