using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Blog.Models;
using Blog.BLL.Interfaces;
using Blog.BLL.DTO;

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
            return new JsonResult() { Data = new PostsViewModel() { content = BlogService.GetNextPosts(User.Identity.GetUserId(), count), morePosts = false }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult AddPost(PostDTO obj)
        {
            try
            {
                if (obj.text == "" || obj.topic == "" || obj.text == null || obj.topic == null)
                {
                    return Json(new { success = false, errorMessage = "Message and topic is required" });
                }

                PostDTO pvm = BlogService.AddPost(obj.topic, obj.text, User.Identity.GetUserId());
                return Json(new { content = pvm, success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public JsonResult AddComment(WriteCommentModel obj)
        {
            try
            {
                if (obj.text == "" || obj.text == null)
                {
                    return Json(new { success = false, errorMessage = "Message is required" });
                }
                CommentDTO cvm = BlogService.AddComment(obj.id, obj.text, User.Identity.GetUserId());
                return Json(new { content = cvm, success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public JsonResult DeleteComment(int id)
        {
            try
            {
                if (BlogService.IsCommentAuthor(id, User.Identity.GetUserId()) || User.IsInRole("admin"))
                {
                    BlogService.DeleteComment(id);
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult DeletePost(int id)
        {
            try
            {
                BlogService.DeletePost(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
    }
}