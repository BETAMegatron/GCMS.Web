using GCMS.Web.Areas.Admin.Models.ViewModel.User;
using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Models.Entities.Users;
using GCMS.Web.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GCMS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _iuserRepository;
        private readonly PasswordHasher<User> _ipasswordhasher;
        private readonly PersianDate _persianDate;   

        public UserController(IUserRepository IUserRepository , PersianDate PersianDate)
        {
            _iuserRepository = IUserRepository;
            _persianDate = PersianDate;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var usr = _iuserRepository.GetAllUsers();
            if(!usr.Any())
            {
                return Content("خطا در نمایش: هیچ کاربری برای نمایش یافت نشد");
            }
            var model = usr.Select(model => new UserShowViewModel
            {
                UserID = model.UserID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                UserCreationDate = model.UserCreationDate,
                UserCreationDateToShamsi = _persianDate.PersianDateCalender(model.UserCreationDate),
                UserProfileImageName = model.UserProfileImageName,
                UserIsActived = model.UserIsActived,
            }).ToList();
            return View("~/Areas/Admin/Views/Users/Index.cshtml",model);
        }
        [HttpPost]
        public async Task<IActionResult> UserDeactivation(int id)
        {
            if(id <= 0)
            {
                return Content("خطا در نمایش: شناسه کاربر معتبر نیست");
            }
            var usr = _iuserRepository.GetUserByID(id);
            if(usr == null)
            {
                return Content("خطا در نمایش: کاربر مورد نظر یافت نشد");
            }
            _iuserRepository.UserDeactive(usr.UserID);
            await _iuserRepository.SaveInAsync();
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }
        [HttpPost]
        public async Task<IActionResult> UserActivation(int id)
        {
            if (id <= 0)
            {
                return Content("خطا در نمایش: شناسه کاربر معتبر نیست");
            }
            var usr = _iuserRepository.GetUserByID(id);
            if (usr == null)
            {
                return Content("خطا در نمایش: کاربر مورد نظر یافت نشد");
            }
            _iuserRepository.UserActive(usr.UserID);
            await _iuserRepository.SaveInAsync();
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }
    }
}
