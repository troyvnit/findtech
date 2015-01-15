using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using FindTech.Services;
using FindTech.Web.Models;
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
            var article = articleService.Queryable().Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.ContentSections).FirstOrDefault(a => a.SeoTitle == seoTitle);
            ViewBag.Article = Mapper.Map<ArticleViewModel>(article);
            return View();
        }

        public ActionResult _NewsBoxs()
        {
            var articles = articleService.Queryable().Include(a => a.Source).Include(a => a.ArticleCategory).Include(a => a.ContentSections);
            return View(new ArticleListViewModel { Articles = articles.Select(Mapper.Map<ArticleViewModel>).ToList() });
        }
    }
}