using GCMS.Web.Areas.Admin.Models.ViewModel;
using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GCMS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsCategoryController : Controller
    {
        private readonly INewsCategoryRepository _newsCategoryRepository;
        private readonly PersianDate _persiandate;
        public NewsCategoryController(INewsCategoryRepository newsCategoryRepository, PersianDate PersianDate)
        {
            _newsCategoryRepository = newsCategoryRepository;
            _persiandate = PersianDate;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var nwc =_newsCategoryRepository.GetNewsCategories();
            if(!nwc.Any())
            {
                return Content("خطا در نمایش دسته بندی ها: هیچ دسته بندیی یافت نشد/");
            }
            var model = nwc.Select(model => new NewsCategoryShowViewModel {
                NewsCategoryID = model.NewsCategoryID,
                NewsCategoryTitle = model.NewsCategoryTitle,
                NewsCategoryDescription = model.NewsCategoryDescription,
                NewsCategoryCreationDate = model.NewsCategoryCreationDate,
                NewsCategoryCreationDateToShamsi = _persiandate.PersianDateCalender(model.NewsCategoryCreationDate),
                NewsQuantitiy = _newsCategoryRepository.NewsQuantities(model.NewsCategoryID),
                IsActive = model.IsActive,
                NewsCategoryImageName = model.NewsCategoryImageName,
            }).ToList();
            return View("~/Areas/Admin/Views/NewsCategory/Index.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> DeactiveNewsCategory(int id)
        {
            if(id == null || id <= 0)
            {
                return Content("خطا در نمایش دسته بندی: شناسه اشتباه می باشد/");
            }
            var nwc = _newsCategoryRepository.GetNewsCategoryByID(id);
            if(nwc == null)
            {
                return Content("خطا در نمایش دسته بندی: هیچ دسته بندیی یافت نشد/");
            }
             _newsCategoryRepository.DeactivateNewsCategory(nwc);
            _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory");
        }
        [HttpPost]
        public async Task<IActionResult> ActivateNewsCategory(int id)
        {
            if (id == null || id <= 0)
            {
                return Content("خطا در نمایش دسته بندی: شناسه اشتباه می باشد/");
            }
            var nwc = _newsCategoryRepository.GetNewsCategoryByID(id);
            if (nwc == null)
            {
                return Content("خطا در نمایش دسته بندی: هیچ دسته بندیی یافت نشد/");
            }
            _newsCategoryRepository.ActivateNewsCategory(nwc);
            _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteNewsCategory(int id)
        {
            if(id == null || id <= 0)
            {
                return Content("خطا در نمایش دسته بندی: شناسه اشتباه می باشد/");
            }
            var nwc = _newsCategoryRepository.GetNewsCategoryByID(id);
            if (nwc == null)
            {
                return Content("خطا در نمایش دسته بندی: هیچ دسته بندیی یافت نشد/");
            }
            nwc.Deleted = true;
            _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory");
        }
        [HttpGet]
        public IActionResult InsertNewCategory()
        {
            return View("~/Areas/Admin/Views/NewsCategory/Insert.cshtml");
        }
    }
}
