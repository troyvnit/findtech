using System.Web.Mvc;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using System.Linq;
using FindTech.Services;
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
            var xmlReader = XmlReader.Create("http://vnexpress.net/rss/so-hoa.rss");
            var feed = SyndicationFeed.Load(xmlReader);
            xmlReader.Close();
            if (feed != null) ViewBag.FeedItems = feed.Items.Take(10);
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