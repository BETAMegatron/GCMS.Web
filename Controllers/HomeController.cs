using GCMS.Web.Data.Context;
using GCMS.Web.Models;
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
        [HttpGet]
        public IActionResult NewsCategory()
        {
            var newsCat = _context.NewsCategories.Where(nc => nc.IsActive && !nc.Deleted).ToList();
            if(!newsCat.Any())
            {
                return Content("دسته بندی اخباری وجود ندارد.");
            }
            var model = newsCat.Select(nc => new NewsCategoryViewModel
            {
                NewsCategoryTitle = nc.NewsCategoryTitle,
                NewsCategoryDescription = nc.NewsCategoryDescription,
                NewsQuantitiy = nc.NewsQuantitiy,
                NewsCategoryImageName = nc.NewsCategoryImageName,


            }).ToList();
            return View("~/Views/News/NewsCategory.cshtml", model);
        }

















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
