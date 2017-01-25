using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blog.Services.Interfaces;
using Blog.ViewModels.HomeViewModels;
using Microsoft.AspNet.Identity;

namespace Blog.Web.Controllers
{
    public class BlogController : ApiController
    {
        public BlogController(IBlogService blogServ)
        {
            BlogService = blogServ;
        }
        private IBlogService BlogService;


        public IEnumerable<PostPreviewViewModel> GetPosts(int count)
        {
            return BlogService.GetNextPosts(User.Identity.GetUserId(), count);
        }

        public UserRoleStatViewModel GetUserStatus()
        {
            return new UserRoleStatViewModel() { IsAdmin = User.IsInRole("admin"), IsUser = User.IsInRole("user") };
        }

        public PostViewModel GetPost(int id)
        {
            return BlogService.GetPostViewModel(id, User.Identity.GetUserId());
        }

        public IEnumerable<string> GetThemes()
        {
            return BlogService.GetThemes();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public bool AddPost(PostViewModel obj)
        {
            if (obj.Text == "" || obj.Topic == "" || obj.Text == null || obj.Topic == null)
            {
                return false;
            }

            PostViewModel pvm = BlogService.AddPost(obj.Topic, obj.Text, User.Identity.GetUserId());
            return true;
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public object AddComment(WriteCommentModel obj)
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

        [HttpDelete]                       
        [Authorize(Roles = "user")]
        public bool DeleteComment(int id)
        {
            if (BlogService.IsCommentAuthor(id, User.Identity.GetUserId()) || User.IsInRole("admin"))
            {
                if (BlogService.DeleteComment(id))
                {
                    return true;
                }
            }
            return false;
        }

        [HttpDelete]                                                      
        [Authorize(Roles = "admin")]
        public bool DeletePost(int id)
        {
            BlogService.DeletePost(id);
            return true;
        }
    }
}