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
            var comments = new List<CommentModel>()
            {
                new CommentModel
                {
                    CommentatorEmail = "troy@ifind.vn",
                    Content = "Nhờ có panel LCD chất lượng cao nên cả 2 đều cho góc nhìn rộng rất tốt đặc biệt là Iphone 6. Màn hình của Sony có chế độ siêu nhạy cho phép bạn có thể sử dụng ngay cả khi đang mang găng tay, một điều khá tiện dụng với người dùng nhất là khi mùa đông đang đến."
                },
                new CommentModel
                {
                    CommentatorEmail = "troy@ifind.vn",
                    Content = "Nhờ có panel LCD chất lượng cao nên cả 2 đều cho góc nhìn rộng rất tốt đặc biệt là Iphone 6. Màn hình của Sony có chế độ siêu nhạy cho phép bạn có thể sử dụng ngay cả khi đang mang găng tay, một điều khá tiện dụng với người dùng nhất là khi mùa đông đang đến."
                },
                new CommentModel
                {
                    CommentatorEmail = "troy@ifind.vn",
                    Content = "Nhờ có panel LCD chất lượng cao nên cả 2 đều cho góc nhìn rộng rất tốt đặc biệt là Iphone 6. Màn hình của Sony có chế độ siêu nhạy cho phép bạn có thể sử dụng ngay cả khi đang mang găng tay, một điều khá tiện dụng với người dùng nhất là khi mùa đông đang đến."
                },
                new CommentModel
                {
                    CommentatorEmail = "troy@ifind.vn",
                    Content = "Nhờ có panel LCD chất lượng cao nên cả 2 đều cho góc nhìn rộng rất tốt đặc biệt là Iphone 6. Màn hình của Sony có chế độ siêu nhạy cho phép bạn có thể sử dụng ngay cả khi đang mang găng tay, một điều khá tiện dụng với người dùng nhất là khi mùa đông đang đến."
                },
                new CommentModel
                {
                    CommentatorEmail = "troy@ifind.vn",
                    Content = "Nhờ có panel LCD chất lượng cao nên cả 2 đều cho góc nhìn rộng rất tốt đặc biệt là Iphone 6. Màn hình của Sony có chế độ siêu nhạy cho phép bạn có thể sử dụng ngay cả khi đang mang găng tay, một điều khá tiện dụng với người dùng nhất là khi mùa đông đang đến."
                }
            };

            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(string model)
        {
            var commentViewModel = JsonConvert.DeserializeObject<CommentModel>(model);
                var comment = Mapper.Map<Comment>(commentViewModel);
                commentService.Insert(comment);
                unitOfWork.SaveChanges();
                return Json(commentViewModel, JsonRequestBehavior.AllowGet);
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