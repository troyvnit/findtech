using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindTech.Entities.Models;
using FindTech.Services;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Areas.BO.Controllers
{
    public class ArticleCategoryBOController : Controller
    {

   
        private IArticleCategoryService articleCategoryService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }


        public ArticleCategoryBOController(IArticleCategoryService articleCategoryService,IUnitOfWorkAsync unitOfWork)
        {
            this.articleCategoryService = articleCategoryService;
            this.unitOfWork = unitOfWork;
        }


        // GET: BO/ArticleCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateArticleCategory(ArticleCategory articleCategory)
        {
            articleCategoryService.Insert(articleCategory);

            unitOfWork.SaveChanges();
            return Redirect("Index");
        }


        


    }
}