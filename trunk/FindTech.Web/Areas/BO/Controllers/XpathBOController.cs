using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;
using FindTech.Services;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.Areas.BO.Controllers
{
    public class XpathBOController : Controller
    {

        
        private IUnitOfWorkAsync unitOfWork { get; set; }
        // GET: BO/Xpath

        public XpathBOController(IUnitOfWorkAsync unitOfWork)
        {
            
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

        

    }
}