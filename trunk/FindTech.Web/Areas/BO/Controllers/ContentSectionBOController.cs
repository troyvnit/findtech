using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;
using AutoMapper;
using FindTech.Entities.Models;
using FindTech.Services;
using FindTech.Web.Areas.BO.Models;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Areas.BO.Controllers
{
    public class ContentSectionBOController : Controller
    {
        private IContentSectionService contentSectionService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }
        // GET: BO/ContentSection

        public ContentSectionBOController(IContentSectionService contentSectionService, IUnitOfWorkAsync unitOfWork)
        {
            this.contentSectionService = contentSectionService;
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetContentSections(int articleId)
        {
            var contentSections = contentSectionService.Query(a => a.Article.ArticleId == articleId).Select();
            return Json(contentSections.Select(Mapper.Map<ContentSectionBOViewModel>), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(string models, int articleId)
        {
            var contentSectionBOViewModels = JsonConvert.DeserializeObject<List<ContentSectionBOViewModel>>(models);
            for (var i = 0; i < contentSectionBOViewModels.Count; i++)
            {
                var contentSectionBOViewModel = contentSectionBOViewModels.ElementAt(i);
                var contentSection = Mapper.Map<ContentSection>(contentSectionBOViewModel);
                contentSection.ArticleId = articleId;
                contentSectionService.Insert(contentSection);
                unitOfWork.SaveChanges();
                contentSectionBOViewModels.RemoveAt(i);
                contentSectionBOViewModels.Add(Mapper.Map<ContentSectionBOViewModel>(contentSection));
            }
            return Json(contentSectionBOViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(string models, int articleId)
        {
            var contentSectionBOViewModels = JsonConvert.DeserializeObject<List<ContentSectionBOViewModel>>(models);
            for (var i = 0; i < contentSectionBOViewModels.Count; i++)
            {
                var contentSectionBOViewModel = contentSectionBOViewModels.ElementAt(i);
                var contentSection = Mapper.Map<ContentSection>(contentSectionBOViewModel);
                contentSection.ArticleId = articleId;
                contentSectionService.Update(contentSection);
                unitOfWork.SaveChanges();
                contentSectionBOViewModels.RemoveAt(i);
                contentSectionBOViewModels.Add(Mapper.Map<ContentSectionBOViewModel>(contentSection));
            }
            return Json(contentSectionBOViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Destroy(string models)
        {
            var contentSectionBOViewModels = JsonConvert.DeserializeObject<List<ContentSectionBOViewModel>>(models);
            for (var i = 0; i < contentSectionBOViewModels.Count; i++)
            {
                var contentSectionBOViewModel = contentSectionBOViewModels.ElementAt(i);
                var contentSection = Mapper.Map<ContentSection>(contentSectionBOViewModel);
                contentSectionService.Delete(contentSection);
                unitOfWork.SaveChanges();
                contentSectionBOViewModels.RemoveAt(i);
            }
            return Json(contentSectionBOViewModels, JsonRequestBehavior.AllowGet);
        }
    }
}