using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.News;
using GCMS.Web.Models.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace GCMS.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly GCMSContext _context;
        public NewsController(GCMSContext GCMSContext)
        {
            _context = GCMSContext;
        }
        [HttpGet]
        public IActionResult NewsSingle(int? id)
        {
            if(id == null)
            {
                return Content("خبر یافت نشد");
            }
            var nw = _context.News.FirstOrDefault(n => n.NewsID == id.Value
                                  && !n.Deleted
                                  && n.NewsIsPublished);
            var model = new NewsViewModel();
            model.NewsID = nw.NewsID;
            model.NewsCategoryID = nw.NewsCategoryID;
            model.NewsTitle = nw.NewsTitle;
            model.NewsDescription = nw.NewsDescription;
            model.NewsPublishedDate = nw.NewsPublishedDate;
            model.NewsIsPublished = nw.NewsIsPublished;
            model.NewsImageName = nw.NewsImageName;
            return View("~/Views/News/NewsSingle.cshtml", model);
        }
        [HttpGet]
        public IActionResult NewsGroup(int id)
        {
            if(id == null)
            {
                return Content("مقدار شناسه دسته بندی  اشتباه می باشد");
            }
            var newcat = _context.NewsCategories.FirstOrDefault(nc => nc.NewsCategoryID == id
                                                 && !nc.Deleted && nc.IsActive);
            if(newcat == null)
            {
                return Content("دسته بندی مورد نظر یافت نشد.");
            }
            var news = _context.News.Where(n => !n.Deleted && n.NewsIsPublished
                                     && n.NewsCategoryID == newcat.NewsCategoryID)
                                    .Select(model => new NewsViewModel
                                    {
                                        NewsID = model.NewsID,
                                        NewsTitle = model.NewsTitle,
                                        NewsPublishedDate = model.NewsPublishedDate,
                                        NewsImageName = model.NewsImageName,
                                        NewsCategoryID = model.NewsCategoryID
                                    }).ToList();
            if(news != null)
            {
                return View("~/Views/News/NewsGroup.cshtml", news);
            }
            return Content("این دسته بندی هیچ خبری ندارد.");
        }

    }
}
