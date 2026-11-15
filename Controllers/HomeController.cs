using GCMS.Web.Data.Context;
using GCMS.Web.Models;
using GCMS.Web.Models.Entities.News;
using GCMS.Web.Models.ViewModels.General;
using GCMS.Web.Models.ViewModels.News;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GCMSContext _context;

        public HomeController(ILogger<HomeController> logger, GCMSContext GCMSContext)
        {
            _logger = logger;
            _context = GCMSContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            var cnt = _context.Contacts.FirstOrDefault();
            if(cnt == null)
            {
                return View("~/Views/General/Error.cshtml");
            }
            var cntmodel = new ContactViewModel();
            cntmodel.Mobile = cnt.Mobile;
            cntmodel.Phone = cnt.Phone;
            cntmodel.InstagramID = cnt.InstagramID;
            cntmodel.TelegramID = cnt.TelegramID;
            cntmodel.WahtsApp = cnt.WahtsApp;
            cntmodel.Address = cnt.Address;
            cntmodel.Email = cnt.Email;
            return View("~/Views/General/Contact.cshtml", cntmodel);
        }
        [HttpGet]
        public IActionResult About()
        {
            var abt = _context.Abouts.FirstOrDefault();
            if(abt == null)
            {
                return View("~/Views/General/Error.cshtml");
            }
            var about = new AboutViewModel();
            about.AboutShortDescription = abt.AboutShortDescription;
            about.AboutLongDescription = abt.AboutLongDescription;
            about.PictureName = abt.PictureName;
            return View("~/Views/General/About.cshtml", about);
        }
        //نمایش دسته بندی اخبار
        [HttpGet]
        public IActionResult NewsCategory()
        {

            var newscat = _context.NewsCategories.Where(nc => !nc.Deleted && nc.IsActive).Select(model => new NewsCategoryViewModel
            {
                NewsCategoryID = model.NewsCategoryID,
                NewsCategoryTitle = model.NewsCategoryTitle,
                NewsCategoryDescription = model.NewsCategoryDescription,
                NewsCategoryImageName = model.NewsCategoryImageName,
                NewsCategoryCreationDate = model.NewsCategoryCreationDate,
                NewsQuantitiy = _context.News.Count(n=> !n.Deleted
                && n.NewsIsPublished && model.NewsCategoryID == n.NewsCategoryID
                )
            }).ToList();
            if (!newscat.Any())
            {
                return Content("دسته بندی اخباری وجود ندارد.");
            }
            return View("~/Views/News/NewsCategory.cshtml", newscat);
        }
        //نمایش خبر ها
        [HttpGet]
        public IActionResult News()
        {
            var newcat = _context.NewsCategories
                      .Where(nc => !nc.Deleted && nc.IsActive)
                      .Select(model => new NewsCategoryViewModel
                      {
                          NewsCategoryID = model.NewsCategoryID,
                          NewsCategoryTitle = model.NewsCategoryTitle,
                          NewsCategoryDescription = model.NewsCategoryDescription,
                          NewsCategoryImageName = model.NewsCategoryImageName,
                          NewsQuantitiy = _context.News
                                                  .Count(n => !n.Deleted && n.NewsIsPublished
                                                   && n.NewsCategoryID == model.NewsCategoryID),
                          LatestNews = _context.News.Where(n => !n.Deleted && n.NewsIsPublished && n.NewsCategoryID == model.NewsCategoryID)
                                                    .OrderByDescending(n => n.NewsPublishedDate)
                                                    .Take(6)
                                                    .Select( nmodel =>  new NewsViewModel{
                                                        NewsID = nmodel.NewsID,
                                                        NewsTitle = nmodel.NewsTitle,
                                                        NewsPublishedDate = nmodel.NewsPublishedDate,
                                                        NewsIsPublished = nmodel.NewsIsPublished,
                                                        NewsImageName = nmodel.NewsImageName,
                                                        NewsCategoryID = nmodel.NewsCategoryID
                                                    }).ToList()
                      }).ToList();

            if (!newcat.Any())
            {
                return Content("دسته‌بندی‌ای برای نمایش وجود ندارد.");
            }

            return View("~/Views/News/News.cshtml", newcat);
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
