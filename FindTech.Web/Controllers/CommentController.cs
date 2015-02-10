using AutoMapper;
using FindTech.Entities.Models;
using FindTech.Services;
using FindTech.Web.Models;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTech.Web.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService commentService { get; set; }
        private IUnitOfWorkAsync unitOfWork { get; set; }

        public CommentController(ICommentService commentService, IUnitOfWorkAsync unitOfWork)
        {
            this.commentService = commentService;
            this.unitOfWork = unitOfWork;
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetComments(string objectType, string objectId)
        {


            return Json(false);
        }

        public ActionResult Create(string model)
        {
            var commentViewModel = JsonConvert.DeserializeObject<CommentModel>(model);
                var comment = Mapper.Map<Comment>(commentViewModel);
                commentService.Insert(comment);
                unitOfWork.SaveChanges();
                //commentViewModel.Add(Mapper.Map<BrandBOViewModel>(brand));
            //return Json(brandBOViewModels, JsonRequestBehavior.AllowGet);
                return Json(false);
        }
        public ActionResult Update(string comment)
        {
            return Json(false);
        }
        public ActionResult Delete(string comment)
        {
            return Json(false);
        }
    }
}