using System;
using System.Data.Entity;
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
        private ISourceService sourceService { get; set; }
        private IArticleCategoryService articleCategoryService { get; set; }
        private IArticleService articleService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }

        public ArticleController(IUnitOfWorkAsync unitOfWork, ISourceService sourceService, IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            this.sourceService = sourceService;
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
            if (!articleCategoryService.Queryable().Any(a => a.ArticleCategoryName == "Tin tổng hợp"))
            {
                articleCategoryService.Insert(new ArticleCategory{ArticleCategoryName = "Tin tổng hợp", Color = "Info", IsActived = true, Priority = 1});
                unitOfWork.SaveChanges();
            }
            var category = articleCategoryService.Queryable().FirstOrDefault(a => a.ArticleCategoryName == "Tin tổng hợp");
            var rssSources = sourceService.Queryable().Include(a => a.Xpaths).ToList();
            foreach (var rssSource in rssSources)
            {
                var xmlReader = XmlReader.Create(rssSource.Link);
                var feed = SyndicationFeed.Load(xmlReader);
                xmlReader.Close();
                if (feed != null)
                {
                    foreach (var feedItem in feed.Items)
                    {
                        var summary = new HtmlAgilityPack.HtmlDocument();
                        summary.LoadHtml(feedItem.Summary.Text);
                        var contentDocument = new HtmlAgilityPack.HtmlDocument();
                        var link = feedItem.Links.FirstOrDefault();
                        if (link != null)
                        {
                            var absoluteUri = link.GetAbsoluteUri();
                            if (absoluteUri != null) contentDocument = new HtmlWeb().Load(absoluteUri.AbsoluteUri);
                        }
                        var contentXpaths = rssSource.Xpaths.Where(a => a.ArticleField == ArticleField.Content);
                        var content = "";
                        foreach (var contentNode in contentXpaths.Select(contentXpath => contentDocument.DocumentNode.SelectSingleNode(contentXpath.XpathString)).Where(contentNode => contentNode != null))
                        {
                            content = contentNode.InnerHtml;
                            break;
                        }
                        var title = feedItem.Title.Text;
                        if (!articleService.Queryable().Any(a => a.Title == title))
                        {
                            var article = new Article
                            {
                                ArticleCategoryId = category.ArticleCategoryId,
                                ArticleCategory = category,
                                Title = title,
                                Avatar = summary.DocumentNode.Descendants("img").Select(n => n.Attributes["src"].Value).ToArray().FirstOrDefault(),
                                Description = summary.DocumentNode.InnerText,
                                IsActived = false,
                                Priority = 1,
                                PublishedDate = feedItem.PublishDate.DateTime,
                                BoxSize = BoxSize.Box1,
                                SourceId = rssSource.SourceId,
                                Source = rssSource,
                                Content = content,
                                ArticleType = ArticleType.News
                            };
                            articleService.Insert(article);
                        }
                    }
                    unitOfWork.SaveChangesAsync();
                }
            }
            
            return Json(true);
        }
    }
}