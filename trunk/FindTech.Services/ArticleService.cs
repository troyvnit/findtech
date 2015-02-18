using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FindTech.Entities.Models;
using FindTech.Entities.Models.Enums;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace FindTech.Services
{
    public interface IArticleService : IService<Article>
    {
        IEnumerable<Article> GetHotArticles();
        IEnumerable<Article> GetLatestReviews(int skip = 0, int take = 20);
        IEnumerable<Article> GetPopularReviews(int skip = 0, int take = 20);
        Article GetArticle(int articleId);
        Article GetArticleDetail(string seoTitle);
    }

    public class ArticleService : Service<Article>, IArticleService
    {
        private readonly IRepositoryAsync<Article> _articleRepository;
        public ArticleService(IRepositoryAsync<Article> articleRepository)
            : base(articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IEnumerable<Article> GetHotArticles()
        {
            return
                _articleRepository.Queryable()
                    .Where(a => a.IsHot == true)
                    .OrderByDescending(a => a.Priority)
                    .ThenByDescending(a => a.PublishedDate)
                    .Include(a => a.Source)
                    .Include(a => a.ArticleCategory);
        }

        public IEnumerable<Article> GetLatestReviews(int skip = 0, int take = 20)
        {
            return
                _articleRepository.Queryable()
                    .Where(a => a.ArticleType == ArticleType.Reviews && a.IsHot != true)
                    .OrderByDescending(a => a.Priority)
                    .ThenByDescending(a => a.PublishedDate)
                    .Skip(skip)
                    .Take(take)
                    .Include(a => a.Source)
                    .Include(a => a.ArticleCategory);
        }

        public IEnumerable<Article> GetPopularReviews(int skip = 0, int take = 20)
        {
            return
                _articleRepository.Queryable()
                    .Where(a => a.ArticleType == ArticleType.Reviews && a.IsHot != true)
                    .OrderByDescending(a => a.ViewCount)
                    .ThenByDescending(a => a.Priority)
                    .ThenByDescending(a => a.PublishedDate)
                    .Skip(skip)
                    .Take(take)
                    .Include(a => a.Source)
                    .Include(a => a.ArticleCategory);
        }
        public Article GetArticle(int articleId)
        {
            return _articleRepository.Queryable().Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.Opinions).FirstOrDefault(a => a.ArticleId == articleId);
        }

        public Article GetArticleDetail(string seoTitle)
        {
            return _articleRepository.Queryable().Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.Opinions).FirstOrDefault(a => a.SeoTitle == seoTitle);
        }

    }
}
