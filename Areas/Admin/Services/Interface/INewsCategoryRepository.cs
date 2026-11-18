using GCMS.Web.Models.Entities.News;

namespace GCMS.Web.Areas.Admin.Services.Interface
{
    public interface INewsCategoryRepository
    {
        public IEnumerable<NewsCategory> GetNewsCategories();
        public NewsCategory GetNewsCategoryByID(int categoryID);
        public void DeleteNewsCategory(NewsCategory category);
        public void UpdateNewsCategory(NewsCategory category);
        public void InsertNewsCategory(NewsCategory category);
        public void Save();
        public Task SaveInAsync();
        public void ActivateNewsCategory(NewsCategory category);
        public void DeactivateNewsCategory(NewsCategory category);
        public bool NewsCategoryExsit(int categoryID);
        public int NewsQuantities(int categoryID);
    }
}
