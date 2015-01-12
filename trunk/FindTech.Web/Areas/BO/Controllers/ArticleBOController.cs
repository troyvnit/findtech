using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Xml;
using AutoMapper;
using FindTech.Entities.Models;
using FindTech.Entities.Models.Enums;
using FindTech.Services;
using FindTech.Web.Areas.BO.CommonFunction;
using FindTech.Web.Areas.BO.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Areas.BO.Controllers
{
    public class ArticleBOController : Controller
    {
        private ISourceService sourceService { get; set; }
        private IArticleCategoryService articleCategoryService { get; set; }
        private IArticleService articleService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }

        public ArticleBOController(IUnitOfWorkAsync unitOfWork, ISourceService sourceService,
            IArticleService articleService, IArticleCategoryService articleCategoryService)
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

        public ActionResult Create(int? articleId)
        {
            var articleBOViewModel = new ArticleBOViewModel();
            var contentSections = new List<ContentSectionBOViewModel>();
            if (articleId != null)
            {
                var article = articleService.Find(articleId);
                articleBOViewModel = Mapper.Map<ArticleBOViewModel>(article);
                if (article != null && article.ContentSections != null)
                    contentSections = article.ContentSections.Select(Mapper.Map<ContentSectionBOViewModel>).ToList();
            }
            ViewBag.ContentSections = contentSections;
            return View(articleBOViewModel);
        }

        public ActionResult GetTags(string tag)
        {
            var tags = new List<object>()
            {
                new {name = "Troy"}
            };
            return Json(tags.Where(a => a.GetType().GetProperty("name").GetValue(a).ToString().Contains(tag)),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArticles(int skip, int take)
        {
            var total = articleService.Query().Select().Count(a => a.IsDeleted != true);

            //var total = articleService.Query().Select().Where(a => a.IsDeleted != true).Count();
            var articles = articleService.Queryable().Where(a => a.IsDeleted != true).OrderByDescending(a => a.CreatedDate).Skip(skip).Take(take).ToList();
            //var articles = articleService.Query().Select().Skip(skip).Take(take);
            return Json(new { articles = articles.Select(Mapper.Map<ArticleGridBOViewModel>) , totalCount = total}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy(string models)
        {
            var articleBOViewModel = JsonConvert.DeserializeObject<ArticleGridBOViewModel>(models);
            var articleId = articleBOViewModel.ArticleId;
            var article = articleService.Query().Select().Where(a => a.ArticleId == articleId).FirstOrDefault();
            //var article = Mapper.Map<Article>(articleBOViewModel);
            article.IsDeleted = true;
            articleService.Update(article);
            var result = unitOfWork.SaveChanges();
            return Json(result);
        }

        public ActionResult Update(string models)
        {
            var articleBOViewModel = JsonConvert.DeserializeObject<ArticleGridBOViewModel>(models);
            var article = Mapper.Map<Article>(articleBOViewModel);
            articleService.Update(article);
            var result = unitOfWork.SaveChanges();
            return Json(result);
        }



        [HttpPost]
        public ActionResult ReadRss(string url)
        {
            if (!articleCategoryService.Queryable().Any(a => a.ArticleCategoryName == "Tin tổng hợp"))
            {
                articleCategoryService.Insert(new ArticleCategory
                {
                    ArticleCategoryName = "Tin tổng hợp",
                    Color = "info",
                    IsActived = true,
                    Priority = 1
                });
                unitOfWork.SaveChanges();
            }
            var category =
                articleCategoryService.Queryable().FirstOrDefault(a => a.ArticleCategoryName == "Tin tổng hợp");
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
                        foreach (
                            var contentNode in
                                contentXpaths.Select(
                                    contentXpath =>
                                        contentDocument.DocumentNode.SelectSingleNode(contentXpath.XpathString))
                                    .Where(contentNode => contentNode != null))
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
                                Avatar =
                                    summary.DocumentNode.Descendants("img")
                                        .Select(n => n.Attributes["src"].Value)
                                        .ToArray()
                                        .FirstOrDefault(),
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
                    unitOfWork.SaveChanges();
                }
            }

            return Json(true);
        }



        [HttpPost]
        public ActionResult CreateOrUpdate(ArticleBOViewModel articleBOViewModel)
        {
            articleBOViewModel.SeoTitle = articleBOViewModel.Title.GenerateSeoTitle();
            var articleId = 0;
            if (articleBOViewModel.ArticleId != 0)
            {
                var count = articleService.Queryable().Count(a => a.ArticleId == articleBOViewModel.ArticleId);
                if (count > 0)
                {
                    var existedArticle = Mapper.Map<Article>(articleBOViewModel);
                    articleService.Update(existedArticle);
                    unitOfWork.SaveChanges();
                    articleId = existedArticle.ArticleId;
                }
            }
            else
            {
                var newArticle = Mapper.Map<Article>(articleBOViewModel);
                articleService.Insert(newArticle);
                unitOfWork.SaveChanges();
                articleId = newArticle.ArticleId;
            }
            return RedirectToAction("Create",new { articleId });
        }
       
        [HttpPost]
        public ActionResult ActiveArticle(string articleIds)
        {
            var activeArticleIds = articleIds.Split(',');
            foreach (var activeArticleId in activeArticleIds.Select(a => int.Parse(a)))
            {
                var article = articleService.Queryable().FirstOrDefault(a => a.ArticleId == activeArticleId);
                article.IsActived = true;
                articleService.Update(article);
            }
            unitOfWork.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}