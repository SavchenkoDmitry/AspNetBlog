using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Blog.Services.Interfaces;
using Blog.ViewModels.HomeViewModels;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IBlogService blogServ)
        {
            BlogService = blogServ;
        }
        private IBlogService BlogService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPosts(int count)
        {
            return new JsonResult() { Data = BlogService.GetNextPosts(User.Identity.GetUserId(), count), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult GetUserStatus()
        {
            return new JsonResult() { Data = new UserRoleStatViewModel() { IsAdmin = User.IsInRole("admin"), IsUser = User.IsInRole("user") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetPost(int id)
        {
            return new JsonResult() { Data = BlogService.GetPostViewModel(id, User.Identity.GetUserId()), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetThemes()
        {
            return new JsonResult() { Data = BlogService.GetThemes(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult AddPost(PostViewModel obj)
        {
            if (obj.Text == "" || obj.Topic == "" || obj.Text == null || obj.Topic == null)
            {
                return Json(false);
            }

            PostViewModel pvm = BlogService.AddPost(obj.Topic, obj.Text, User.Identity.GetUserId());
            return Json(true);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public JsonResult AddComment(WriteCommentModel obj)
        {
            if (obj.Text == "" || obj.Text == null)
            {
                return Json(new { success = false, errorMessage = "Message is required" });
            }
            CommentViewModel cvm = BlogService.AddComment(obj.Id, obj.Text, User.Identity.GetUserId());
            if (cvm == null)
            {
                return Json(new { success = false, errorMessage = "Post does not exist" });
            }
            return Json(new { Content = cvm, success = true });
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public JsonResult DeleteComment(int id)
        {
            if (BlogService.IsCommentAuthor(id, User.Identity.GetUserId()) || User.IsInRole("admin"))
            {
                if (BlogService.DeleteComment(id))
                {
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult DeletePost(int id)
        {
            BlogService.DeletePost(id);
            return Json(new { success = true });
        }
    }
}