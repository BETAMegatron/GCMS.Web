using GCMS.Web.Areas.Admin.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GCMS.Web.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/NewsCategory/Index.cshtml");
        }
    }
}
