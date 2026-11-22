using GCMS.Web.Areas.Admin.Model.News;
using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Models.Entities.News;

namespace GCMS.Web.Areas.Admin.Services.Repository
{
    public class NewsRepository : INewsRepository
    {
        public void DeleteNews(News news)
        {
            throw new NotImplementedException();
        }

        public List<News> GetAllNews()
        {
            throw new NotImplementedException();
        }

        public List<NewsCountInterfaceModel> GetAllNewsCategoryNewsCount()
        {
            throw new NotImplementedException();
        }

        public News GetNewsByID(int newsID)
        {
            throw new NotImplementedException();
        }

        public int GetNewsCountPerNewsCategory(int newsCategoryID)
        {
            throw new NotImplementedException();
        }

        public void InsertNews(News news)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsunc()
        {
            throw new NotImplementedException();
        }

        public void UpdateNews(News news)
        {
            throw new NotImplementedException();
        }
    }
}
