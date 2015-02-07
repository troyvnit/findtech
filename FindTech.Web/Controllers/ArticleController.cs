using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using FindTech.Entities.Models.Enums;
using FindTech.Services;
using FindTech.Web.Models;
using FindTech.Web.Models.Enums;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService articleService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }

        public ArticleController(IUnitOfWorkAsync unitOfWork, IArticleService articleService)
        {
            this.articleService = articleService;
            this.unitOfWork = unitOfWork;
        }
        // GET: Article
        //public ActionResult Detail(int id)
        //{
        //    var article = articleService.Queryable().Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.ContentSections).FirstOrDefault(a => a.ArticleId == id);
        //    ViewBag.Article = Mapper.Map<ArticleViewModel>(article);
        //    return View();
        //}

        public ActionResult Detail(string seoTitle)
        {
            var article = articleService.Queryable().Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.ContentSections).Include(a => a.Opinions).FirstOrDefault(a => a.SeoTitle == seoTitle);
            if (article == null) return null;
            ViewBag.Article = Mapper.Map<ArticleViewModel>(article);
            var articles = articleService.Queryable().OrderByDescending(a => a.PublishedDate).Take(10);
            ViewBag.Articles = articles.Select(Mapper.Map<ArticleViewModel>);
            article.ViewCount++;
            articleService.Update(article);
            unitOfWork.SaveChangesAsync();
            return View();
        }

        public ActionResult _NewsBoxs(int skip = 0, int take = 20)
        {
            var articles = articleService.Queryable().Where(a => a.ArticleType == ArticleType.News && a.IsHot != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.PublishedDate).Skip(skip).Take(take).Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.ContentSections);
            return View(new ArticleListViewModel { Articles = articles.Select(Mapper.Map<ArticleViewModel>).ToList() });
        }

        public ActionResult _Widget(SearchType searchType, string keyword, ArticleListViewModel articleListViewModel)
        {
            var query = articleService.Queryable();
            switch (searchType)
            {
                case SearchType.Category:
                    query = query.Where(a => a.ArticleCategory.SeoName == keyword && a.IsHot != true);
                    break;
                case SearchType.Tags:
                    query = query.Where(a => a.Tags.Contains(keyword) && a.IsHot != true);
                    break;
                default: break;
            }
            var articles = query.OrderByDescending(a => a.Priority).ThenByDescending(a => a.PublishedDate).Skip(0).Take(20);
            articleListViewModel.Articles = articles.Select(Mapper.Map<ArticleViewModel>).ToList();
            return View(articleListViewModel);
        }
    }
}