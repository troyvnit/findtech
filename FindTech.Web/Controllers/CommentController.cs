using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTech.Web.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetComments(string objectType, string objectId)
        {


            return Json(false);
        }

        public ActionResult Save(string comment)
        {
            return Json(false);
        }
    }
}