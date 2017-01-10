using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Blog.Models;
using Blog.Services.Interfaces;
using Blog.ViewModels;

namespace Blog.Controllers
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

        [HttpGet]
        public JsonResult GetPosts(int count)
        {
            return new JsonResult() { Data = new ContentViewModel() { Content = BlogService.GetNextPosts(User.Identity.GetUserId(), count), MorePosts = false }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult AddPost(PostViewModel obj)
        {
            if (obj.Text == "" || obj.Topic == "" || obj.Text == null || obj.Topic == null)
            {
                return Json(new { success = false, errorMessage = "Message and topic is required" });
            }

            PostViewModel pvm = BlogService.AddPost(obj.Topic, obj.Text, User.Identity.GetUserId());
            return Json(new { Content = pvm, success = true });
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
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
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