using Microsoft.AspNetCore.Mvc;

namespace GCMS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/RP/Roles/R_Index.cshtml");
        }
    }
}
