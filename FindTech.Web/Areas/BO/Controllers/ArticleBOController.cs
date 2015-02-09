﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
using FindTech.Web.Models;
using HtmlAgilityPack;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;
using TestImageCrop;
using System.IO;
using System.Configuration;
using System.Net;
using System.Drawing;

namespace FindTech.Web.Areas.BO.Controllers
{
    [Authorize]
    public class ArticleBOController : BaseController
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
                var article = articleService.Queryable().Include(a => a.ContentSections).FirstOrDefault(a =>a.ArticleId == articleId);
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

        public ActionResult GetArticles(int skip, int take, String filter)
        {

            var total = articleService.Query().Select().Count(a => a.IsDeleted != true);
            var articles = new List<Article>();
            
            if (filter != null)
            {
                var articleGridListFiltersBOViewModel = JsonConvert.DeserializeObject<ArticleGridListFiltersBOViewModel>(filter);
                var listParame = new List<string>();
                var query = buildingWhereClause(articleGridListFiltersBOViewModel, listParame);

                int from = skip + 1;
                int to =  skip + take;

                var whereClause =  new SqlParameter
                {
                    ParameterName = "whereClause",
                    Value = query.ToString(),
                    
                };

                var paramFrom = new SqlParameter
                {
                    ParameterName = "from",
                    Value = from.ToString(CultureInfo.InvariantCulture)
                };

                var paramTo = new SqlParameter
                {
                    ParameterName = "to",
                    Value = to.ToString(CultureInfo.InvariantCulture)
                };

                articles = articleService.SelectQuery("exec ifadmin.getArticlesByFiltersPaging @whereClause, @from, @to", whereClause, paramFrom, paramTo).ToList();
            }
            else
            {  
               articles  = articleService.Queryable().Where(a => a.IsDeleted != true).OrderByDescending(a => a.CreatedDate).Skip(skip).Take(take).ToList();
            }            
            return Json(new { articles = articles.Select(Mapper.Map<ArticleGridBOViewModel>) , totalCount = total}, JsonRequestBehavior.AllowGet);
        }

        private StringBuilder buildingWhereClause(ArticleGridListFiltersBOViewModel articleGridListFiltersBOViewModel, List<String> Params )
        {
            var query = new StringBuilder();
            query.Append(" ( ");
         
            for (int i = 0; i < articleGridListFiltersBOViewModel.Filters.Count; i++)
            {
                var filter = articleGridListFiltersBOViewModel.Filters[i];
                if (i > 0)
                {
                    query.Append(" ");
                    query.Append(articleGridListFiltersBOViewModel.Logic);
                    query.Append(" ");
                }
                query.Append(filter.Field);
                query.Append(" ");
                if (filter.Operator.Equals("eq"))
                {
                    query.Append(" = '" + filter.Value + "'");
                }
                else if (filter.Operator.Equals("ne"))
                {
                    query.Append(" <>'" + filter.Value + "'" );
                }
                else if (filter.Operator.Equals("contains"))
                {
                    query.Append("Like N'%" + filter.Value + "%' ");
                }
                else if (filter.Operator.Equals("startswith"))
                {
                    query.Append("Like N'%" + filter.Value + "'");
                }
                else if (filter.Operator.Equals("endswith"))
                {
                    query.Append("Like N'" + filter.Value + "%' ");
                }
            }
           
            query.Append(" ) ");
            return query;
        }

        [ValidateInput(false)]
        public ActionResult Destroy(string models)
        {
            var articleBOViewModel = JsonConvert.DeserializeObject<ArticleGridBOViewModel>(models);
            var article = Mapper.Map<Article>(articleBOViewModel);
            article.IsDeleted = true;
            articleService.Update(article);
            var result = unitOfWork.SaveChanges();
            return Json(result);
        }

        [ValidateInput(false)]
        public ActionResult Update(string models)
        {
            var articleBOViewModel = JsonConvert.DeserializeObject<ArticleGridBOViewModel>(models);
            var article = Mapper.Map<Article>(articleBOViewModel);
            article.CreatedUserId = CurrentUser.Id;
            article.CreatedUserDisplayName = CurrentUser.DisplayName;
            articleService.Update(article);
            var result = unitOfWork.SaveChanges();
            return Json(result);
        }



        [HttpPost]
        public ActionResult ReadRss()
        {
            var errorArticles = new List<object>();
            if (!articleCategoryService.Queryable().Any(a => a.ArticleCategoryName == "Tin tổng hợp"))
            {
                articleCategoryService.Insert(new ArticleCategory
                {
                    ArticleCategoryName = "Tin tổng hợp",
                    Color = "info",
                    IsActived = true,
                    Priority = 1,
                    SeoName = "Tin tổng hợp".GenerateSeoTitle()
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
                        var title = feedItem.Title.Text;
                        if (!articleService.Queryable().Any(a => a.Title == title))
                        {
                            var summary = new HtmlAgilityPack.HtmlDocument();
                            summary.LoadHtml(feedItem.Summary != null ? feedItem.Summary.Text : "");
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
                            if (category != null)
                            {
                                try
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
                                        ArticleType = ArticleType.News,
                                        SeoTitle = title.GenerateSeoTitle(),
                                        IsHot = false,
                                        CreatedUserId = CurrentUser.Id,
                                        CreatedUserDisplayName = CurrentUser.DisplayName
                                    };
                                    articleService.Insert(article);
                                    unitOfWork.SaveChanges();
                                }
                                catch(Exception e)
                                {
                                    errorArticles.Add(new { Title = title, PublishedDate = feedItem.PublishDate.DateTime });
                                }
                            }
                        }
                    }
                }
            }

            return Json(new { Success = errorArticles.Count == 0, ErrorArticles = errorArticles});
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
                    existedArticle.CreatedUserId = CurrentUser.Id;
                    existedArticle.CreatedUserDisplayName = CurrentUser.DisplayName;
                    articleService.Update(existedArticle);
                    unitOfWork.SaveChanges();
                    articleId = existedArticle.ArticleId;
                }
            }
            else
            {
                var newArticle = Mapper.Map<Article>(articleBOViewModel);
                newArticle.CreatedUserId = CurrentUser.Id;
                newArticle.CreatedUserDisplayName = CurrentUser.DisplayName;
                articleService.Insert(newArticle);
                unitOfWork.SaveChanges();
                articleId = newArticle.ArticleId;
            }
            var url = Url.Action("Create", "ArticleBO", new { articleId }, Request.Url.Scheme);
            return Json(url, JsonRequestBehavior.AllowGet);
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

        public virtual ActionResult CropImage(string imagePath, float scale, int w, int h, int x, int y)
        {
            Rectangle cropRect = new Rectangle((int)(x / scale), (int)(y / scale), (int)(w / scale), (int)(h / scale));
            Bitmap source = System.Drawing.Image.FromFile(imagePath) as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(source, new Rectangle(0, 0, cropRect.Width, cropRect.Height), cropRect, GraphicsUnit.Pixel);
            }
            return Json(true);
        }
    }
}