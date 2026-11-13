using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using GCMS.Web.Models.ViewModels.Account;
using Microsoft.IdentityModel.Tokens;

namespace GCMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly GCMSContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly IWebHostEnvironment _iwebHostEnvironment;
        public AccountController(GCMSContext GCMSContext, IWebHostEnvironment iwebHostEnvironment)
        {
            _context = GCMSContext;
            _iwebHostEnvironment = iwebHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if(!ModelState.IsValid)
            {
                return Content("Error 404");
            }
            var usr = _context.Users.FirstOrDefault(u => u.Username == user.Username && !u.Deleted && u.UserIsActived);
            if(usr == null)
            {
                return Content("اطلاعات کاربر یافت نشد");
            }
            var res = _passwordHasher.VerifyHashedPassword(usr, usr.Password, user.Password);
            if(res == PasswordVerificationResult.Failed)
            {
                return Content("پسورد اشتباه می باشد");
            }
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,usr.UserID.ToString()),
                new Claim(ClaimTypes.Name,usr.Username),
            };
            var Identity = new ClaimsIdentity(Claims,CookieAuthenticationDefaults.AuthenticationScheme);
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
            if(usr)
            {
                return Content("نام کاربری قبلا انتخاب شده است");
            }
            var euser = new User();            
            euser.FirstName = user.FirstName;
            euser.LastName = user.LastName;
            euser.Email = user.Email;
            euser.Password = _passwordHasher.HashPassword(euser, user.Password);
            euser.Username = user.Username;
            if(user.UserProfileImage != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(user.UserProfileImage.FileName);
                string path = Path.Combine(_iwebHostEnvironment.WebRootPath, "Image", "Profile", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path!));
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await user.UserProfileImage.CopyToAsync(stream);
                }
                euser.UserProfileImageName = filename;
            }
            await _context.Users.AddAsync(euser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }



    }
}

/*
             if (user.UserProfileImage != null)
            {
                string fileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(user.UserProfileImage.FileName);

                string path = Path.Combine(_iwebHostEnvironment.WebRootPath, "images", "profiles", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await user.UserProfileImage.CopyToAsync(stream);
                }

                euser.UserProfileImageName = fileName;
            }
 */

















/*
         [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return Content("اطلاعات وارد شده معتبر نیست.");
            }

            var usr = _context.Users.FirstOrDefault(u =>
                u.Username == user.Username &&
                u.UserIsActived == true &&
                u.Deleted == false
            );

            if (usr == null)
            {
                return Content("نام کاربری یا رمز عبور اشتباه می‌باشد.");
            }

            var res = _passwordHasher.VerifyHashedPassword(usr, usr.Password, user.Password);
            if (res == PasswordVerificationResult.Failed)
            {
                return Content("نام کاربری یا رمز عبور اشتباه می‌باشد.");
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usr.UserID.ToString()),
                    new Claim(ClaimTypes.Name, usr.Username)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
        }
 */
