using GCMS.Web.Areas.Admin.Models.ViewModel;
using GCMS.Web.Areas.Admin.Models.ViewModel.News;
using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.News;
using Microsoft.AspNetCore.Mvc;
using GCMS.Web.Utilities;

namespace GCMS.Web.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly ErrorMessage _errorMessage;
        private readonly INewsRepository _inewsrepository;
        private readonly IWebHostEnvironment _iwebhostenvoriment;
        public NewsController(INewsRepository INewsRepository, IWebHostEnvironment IWebHostEnvironment)
        {
            _inewsrepository = INewsRepository;
            _iwebhostenvoriment = IWebHostEnvironment;
        }
        //نمایش همه اخبار 
        [HttpGet]
        public IActionResult Index()
        {
            var nws = _inewsrepository.GetAllNews();
            if (nws == null)
            {
                return Content("خبری برای نمایش پیدا نشد");
            }
            var model = nws.Select(n => new NewsShowViewModel
            {
                NewsID = n.NewsID,
                NewsTitle = n.NewsTitle,
                NewsDescription = n.NewsDescription,
                NewsImageName = n.NewsImageName,
                NewsVideoName = n.NewsVideoName,
                NewsIsPublished = n.NewsIsPublished

            }).ToList();

            return View("~/Views/NewsCategory/Index.cshtml", model);
        }
        //افزودن خبر
        [HttpGet]
        public IActionResult InsertNews()
        {
            return View("~/Areas/Admin/Views/News/Index.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> InsertNews(NewsCreateViewModel News)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "مدل دریافتی برا خبر معتبر نمی باشد.";
                TempData["ErrorCode"] = "GCMS-500";
                return RedirectToAction(nameof(Index));
            }
            var model = new News();
            model.NewsID = News.NewsID;
            model.NewsTitle = News.NewsTitle;
            model.NewsDescription = News.NewsDescription;
            if (News.NewsImage != null && News.NewsImage.Length > 0)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(News.NewsImage.FileName);
                var path = Path.Combine(_iwebhostenvoriment.WebRootPath, "Image", "News", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await News.NewsImage.CopyToAsync(stream);
                }
                model.NewsImageName = filename;
            }
            if (News.NewsVideo == null && News.NewsVideo.Length < 0)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(News.NewsVideo.FileName);
                var path = Path.Combine(_iwebhostenvoriment.WebRootPath, "Video", "News", filename);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await News.NewsVideo.CopyToAsync(stream);
                }
                model.NewsVideoName = filename;
            }
            model.NewsIsPublished = News.NewsIsPublished;
            _inewsrepository.InsertNews(model);
            await _inewsrepository.SaveAsunc();
            return RedirectToAction("Index", "News", new { areas = "Admin" });
        }
        //[HttpPost]
        //public async Task<IActionResult> EditNews(NewsEditViewModel news ,int id)
        //{
        //    if(id <=0)
        //    {
        //        TempData["ErrorMessage"] = _errorMessage.
        //        TempData[] = "شناسه اخبار برای ویرایش معتیر نیست"
        //    }
        //}



    }
}
