using GCMS.Web.Areas.Admin.Model.News;
using GCMS.Web.Models.Entities.News;

namespace GCMS.Web.Areas.Admin.Services.Interface
{
    public interface INewsRepository
    {
        public List<News> GetAllNews();
        public News GetNewsByID(int newsID);
        public void InsertNews(News news);
        public void UpdateNews(News news);
        public void DeleteNews(News news);
        public int GetNewsCountPerNewsCategory(int newsCategoryID);
        public List<NewsCountInterfaceModel> GetAllNewsCategoryNewsCount();
        public void Save();
        public Task SaveAsunc();
    }
}
