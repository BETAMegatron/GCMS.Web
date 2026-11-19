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
        private readonly IWebHostEnvironment _iwebHostEnvironment;
        public NewsCategoryController(INewsCategoryRepository newsCategoryRepository, PersianDate PersianDate, IWebHostEnvironment IWebHostEnvironment)
        {
            _newsCategoryRepository = newsCategoryRepository;
            _persiandate = PersianDate;
            _iwebHostEnvironment = IWebHostEnvironment;
        }
        //نمایشش دسته بندی ها
        [HttpGet]
        public IActionResult Index()
        {
            var nwc = _newsCategoryRepository.GetNewsCategories();
            if (!nwc.Any())
            {
                return Content("خطا در نمایش دسته بندی ها: هیچ دسته بندیی یافت نشد/");
            }
            var model = nwc.Select(model => new NewsCategoryShowViewModel
            {
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
        //غیرفعالسازی
        [HttpPost]
        public async Task<IActionResult> DeactiveNewsCategory(int id)
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
            _newsCategoryRepository.DeactivateNewsCategory(nwc);
            _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory");
        }
       // فعالسازی
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
        //حذف دسته بندی
        [HttpPost]
        public async Task<IActionResult> DeleteNewsCategory(int id)
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
            nwc.Deleted = true;
            _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory");
        }
        [HttpGet]
        public IActionResult InsertNewCategory()
        {
            return View("~/Areas/Admin/Views/NewsCategory/Insert.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertNewCategory(NewCategoryInsertViewModel categoory)
        {
            if (!ModelState.IsValid)
            {
                return Content("خطا در نمایش: مدل ورودی معتبر نمی باشد");
            }
            var nwc = new Web.Models.Entities.News.NewsCategory();
            nwc.NewsCategoryID = categoory.NewsCategoryID;
            nwc.NewsCategoryTitle = categoory.NewsCategoryTitle;
            nwc.NewsCategoryDescription = categoory.NewsCategoryDescription;
            nwc.IsActive = categoory.IsActive;
            if (nwc.NewsCategoryImageName == null || nwc.NewsCategoryImageName.Length <= 0)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(categoory.NewsCategoryImage.FileName);
                var path = Path.Combine(_iwebHostEnvironment.WebRootPath, "Image", "NewsCategories", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await categoory.NewsCategoryImage.CopyToAsync(stream);
                }
                nwc.NewsCategoryImageName = filename;
            }
            _newsCategoryRepository.InsertNewsCategory(nwc);
            await _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult EditeNewsCategory(int id)
        {
            if (id <= 0)
            {
                return Content("شناسه نا معتبر می باشد");
            }
            var nwc = _newsCategoryRepository.GetNewsCategoryByID(id);
            if (nwc == null)
            {
                return Content("دسته بندی  معتبر نیست");
            }
            var model = new NewsCategoryEditViewModel
            {
                NewsCategoryID = nwc.NewsCategoryID,
                NewsCategoryTitle = nwc.NewsCategoryTitle,
                NewsCategoryDescription = nwc.NewsCategoryDescription,
                NewsCategoryImageName = nwc.NewsCategoryImageName,
                IsActive = nwc.IsActive,
                NewsCategoryCreationDate = nwc.NewsCategoryCreationDate
            };
            return View("~/Areas/Admin/Views/NewsCategory/Edit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditeNewsCategory(NewsCategoryEditViewModel category, int id)
        {
            if (id <= 0)
            {
                return Content("شناسه دسته بندی معتبر نیست.");
            }
            var nwc = _newsCategoryRepository.GetNewsCategoryByID(id);
            if (nwc == null)
            {
                return Content("دسته بندی مورد نظر یافت نشد.");
            }
            if (!ModelState.IsValid)
            {
                return Content("مدل ورودی معتبر نیست.");
            }
            nwc.NewsCategoryTitle = category.NewsCategoryTitle;
            nwc.NewsCategoryDescription = category.NewsCategoryDescription;
            nwc.IsActive = category.IsActive;
            if (category.NewsCategoryImage != null && category.NewsCategoryImage.Length > 0)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(category.NewsCategoryImage.FileName);
                var path = Path.Combine(_iwebHostEnvironment.WebRootPath, "Image", "NewsCategories", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await category.NewsCategoryImage.CopyToAsync(stream);
                }
                nwc.NewsCategoryImageName = filename;
            }
            _newsCategoryRepository.UpdateNewsCategory(nwc);
            await _newsCategoryRepository.SaveInAsync();
            return RedirectToAction("Index", "NewsCategory", new { area = "Admin" });
        }
    }
}
