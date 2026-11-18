using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.News;
using Microsoft.EntityFrameworkCore;

namespace GCMS.Web.Areas.Admin.Services.Repository
{
    public class NewsCategoryRepository : INewsCategoryRepository
    {
        public GCMSContext _db;
        public NewsCategoryRepository(GCMSContext GCMSContext)
        {
            _db = GCMSContext;
        }

        public void DeleteNewsCategory(NewsCategory category)
        {
            category.Deleted = true;
        }

        public IEnumerable<NewsCategory> GetNewsCategories()
        {
            return _db.NewsCategories.Where(nc => !nc.Deleted).ToList();
        }

        public NewsCategory GetNewsCategoryByID(int categoryID)
        {
            return _db.NewsCategories.FirstOrDefault(nc => !nc.Deleted && nc.NewsCategoryID == categoryID);
        }

        public void InsertNewsCategory(NewsCategory category)
        {
            
            _db.Add(category);
        }

        public bool NewsCategoryExsit(int categoryID)
        {
            return _db.NewsCategories.Any(nc => nc.NewsCategoryID == categoryID && !nc.Deleted);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveInAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void UpdateNewsCategory(NewsCategory category)
        {
            _db.Entry(category).State = EntityState.Modified;
        }

        public void ActivateNewsCategory(NewsCategory category)
        {
            category.IsActive = true;
        }

        public void DeactivateNewsCategory(NewsCategory category)
        {
            category.IsActive = false;
        }

        int INewsCategoryRepository.NewsQuantities(int categoryID)
        {
            var nwc = GetNewsCategoryByID(categoryID);
            int quantities = _db.News.Count(n => n.NewsCategoryID == nwc.NewsCategoryID);
            return quantities;
            
        }
    }
}
