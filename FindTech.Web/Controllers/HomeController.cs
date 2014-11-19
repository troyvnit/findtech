using System.Data.Entity;
using System.Web.Mvc;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FindTech.Services;
using FindTech.Web.Models;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Controllers
{
    public class HomeController : Controller
    {
        private IArticleService articleService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }

        public HomeController(IUnitOfWorkAsync unitOfWork, IArticleService articleService)
        {
            this.articleService = articleService;
            this.unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            var articles = articleService.Queryable().OrderByDescending(a => a.PublishedDate).Include(a => a.Source).Include(a => a.ArticleCategory).Select(Mapper.Map<ArticleViewModel>);
            ViewBag.Articles = articles;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}