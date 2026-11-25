using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.News;
using Microsoft.EntityFrameworkCore;

namespace GCMS.Web.Areas.Admin.Services.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly GCMSContext _db;
        public NewsRepository(GCMSContext GCMSContext)
        {
            _db = GCMSContext;
        }
        public void DeleteNews(News news)
        {
            _db.Entry(news).State = EntityState.Deleted;
        }

        public List<News> GetAllNews()
        {
            return _db.News.Where(n => !n.Deleted).ToList();
        }

        public News GetNewsByID(int newsID)
        {
            return  _db.News.FirstOrDefault(n => !n.Deleted && n.NewsID == newsID);
        }
        public void InsertNews(News news)
        {
            _db.News.Add(news);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsunc()
        {
            await _db.SaveChangesAsync();
        }

        public string ShowCategoryForNews(int newsID)
        {
            var nw = _db.News.FirstOrDefault(n => !n.Deleted && n.NewsID == newsID);
            var category = _db.NewsCategories.FirstOrDefault(nc => !nc.Deleted && nc.NewsCategoryID == nw.NewsCategoryID);
            return category.NewsCategoryTitle;  
        }

        public void UpdateNews(News news)
        {
            _db.Entry(news).State = EntityState.Modified;
        }
    }
}
