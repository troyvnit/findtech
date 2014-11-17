using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using AutoMapper;
using FindTech.Entities.Models;
using FindTech.Entities.Models.Enums;
using FindTech.Services;
using FindTech.Web.Areas.BO.Models;
using HtmlAgilityPack;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Areas.BO.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService articleService { get; set; }
        private IArticleCategoryService articleCategoryService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }

        public ArticleController(IUnitOfWorkAsync unitOfWork, IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            this.articleService = articleService;
            this.articleCategoryService = articleCategoryService;
            this.unitOfWork = unitOfWork;
        }

        // GET: BO/Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetArticles()
        {
            var articles = articleService.Query().Select();
            return Json(articles.Select(Mapper.Map<ArticleBOViewModel>), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReadRss(string url)
        {
            if (!articleCategoryService.Query(a => a.ArticleCategoryName == "Tin tổng hợp").Select().Any())
            {
                articleCategoryService.Insert(new ArticleCategory{ArticleCategoryName = "Tin tổng hợp", Color = "Info", IsActived = true, Priority = 1});
                unitOfWork.SaveChanges();
            }
            var category = articleCategoryService.Query(a => a.ArticleCategoryName == "Tin tổng hợp").Select().FirstOrDefault();
            var xmlReader = XmlReader.Create("http://vnexpress.net/rss/so-hoa.rss");
            var feed = SyndicationFeed.Load(xmlReader);
            xmlReader.Close();
            if (feed != null)
            {
                foreach (var item in feed.Items)
                {
                    var summary = new HtmlAgilityPack.HtmlDocument();
                    summary.LoadHtml(item.Summary.Text);
                    var contentDocument = new HtmlAgilityPack.HtmlDocument();
                    var link = item.Links.FirstOrDefault();
                    if (link != null)
                    {
                        var absoluteUri = link.GetAbsoluteUri();
                        if (absoluteUri != null) contentDocument = new HtmlWeb().Load(absoluteUri.AbsoluteUri);
                    }
                    var content =
                        contentDocument.DocumentNode.SelectSingleNode(
                            "//div[contains(@class,'fck_detail width_common')]").InnerHtml;
                    if (!articleService.Query(a => a.Title == item.Title.Text).Select().Any())
                    {
                        var article = new Article
                        {
                            ArticleCategoryId = category.ArticleCategoryId,
                            ArticleCategory = category,
                            Title = item.Title.Text,
                            Avatar = summary.DocumentNode.Descendants("img").Select(n => n.Attributes["src"].Value).ToArray().FirstOrDefault(),
                            Description = summary.DocumentNode.InnerText,
                            IsActived = false,
                            Priority = 1,
                            PublishedDate = item.PublishDate.DateTime,
                            BoxSize = BoxSize.Box1,
                            Source = "SoHoa.Net",
                            Content = content,
                        };
                        articleService.Insert(article);
                        unitOfWork.SaveChangesAsync();
                    }
                }
            }
            return Json(true);
        }
    }
}