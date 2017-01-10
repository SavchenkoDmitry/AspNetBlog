using Blog.Services.Common;
using Blog.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Blog.Services.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using Blog.DAL.EF;
using Blog.DAL.Repositories;
using Blog.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Blog.ViewModels;
using System.Data.Entity;

namespace Blog.Services.Services
{
    public class BlogService : IBlogService
    {
        const int PostsInOnePage = 3;
        const string StandartRole = "user";


        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private PostRepository postRep;
        private CommentRepository commentRep;
        
        public BlogService()
        {
            db = new ApplicationContextFactory().Create();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            postRep = new PostRepository(db);
            commentRep = new CommentRepository(db);
            db.Posts.Load();
            db.Comments.Load();     
        }

        #region user
        public async Task<OperationDetails> CreateUser(UserViewModel userVM)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(userVM.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userVM.Email, UserName = userVM.Email };
                await userManager.CreateAsync(user, userVM.Password);
                await userManager.AddToRoleAsync(user.Id, userVM.Role);
                if (userVM.Role != StandartRole)
                {
                    await userManager.AddToRoleAsync(user.Id, StandartRole);
                }
                await db.SaveChangesAsync();
                return new OperationDetails(true, "Register successful", "");

            }
            else
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userVM)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await userManager.FindAsync(userVM.Email, userVM.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        #endregion


        #region post
        private PostViewModel GetDPostViewModel(Post p, string userId)
        {
            List<CommentViewModel> comentsViewModel = new List<CommentViewModel>();
            foreach (Comment c in FindPostComments(p))
            {
                comentsViewModel.Add(GetCommentViewModel(c, userId));
            }
            return new PostViewModel() { Author = p.Author.UserName, Id = p.Id, Text = p.Text, Time = p.Time.ToString(), Topic = p.Topic, Coments = comentsViewModel };
        }

        private List<PostViewModel> GetPostViewModelList(List<Post> posts, string userId)
        {
            List<PostViewModel> postsViewModel = new List<PostViewModel>();
            List<Comment> Comments = commentRep.GetAll().ToList().Where(c => posts.Contains(c.Post)).ToList(); ;

            foreach (Post p in posts)
            {
                List<CommentViewModel> comentsViewModel = new List<CommentViewModel>();
                foreach (Comment c in Comments.Where(c => c.Post == p))
                {
                    comentsViewModel.Add(GetCommentViewModel(c, userId));
                }
                postsViewModel.Add(new PostViewModel() { Author = p.Author.UserName, Id = p.Id, Text = p.Text, Time = p.Time.ToString(), Topic = p.Topic, Coments = comentsViewModel });
            }
            return postsViewModel;
        }


        public PostViewModel AddPost(string topic, string text, string userId)
        {
            Post p = new Post() { Text = text, Topic = topic, Author = userManager.FindById(userId), Time = DateTime.Now };
            db.Posts.Add(p);
            postRep.Save();
            return GetDPostViewModel(p, userId);
        }

        public void DeletePost(int id)
        {
            Post p = db.Posts.Find(id);
            if (p == null)
            {
                return;
            }
            foreach (Comment c in FindPostComments(p))
            {
                commentRep.Delete(c);
            }
            postRep.Delete(p);
            db.SaveChangesAsync();
        }

        public List<PostViewModel> GetNextPosts(string userId, int skip)
        {
            List<Post> posts = postRep.GetForPage(skip, PostsInOnePage);
            return GetPostViewModelList(posts, userId); ;
        }

        #endregion

        #region comment
        public CommentViewModel AddComment(int postId, string text, string userId)
        {
            Comment c = new Comment() { Author = userManager.FindById(userId), Post = postRep.FindById(postId), Text = text, Time = DateTime.Now };
            if (c.Post == null)
            {
                return null;
            }
            commentRep.Add(c);
            commentRep.Save();
            return GetCommentViewModel(c, userId);
        }
        public bool DeleteComment(int id)
        {
            Comment c = commentRep.FindById(id);
            if (c == null)
            {
                return false;
            }
            commentRep.Delete(c);
            db.SaveChangesAsync();
            return true;
        }
        private CommentViewModel GetCommentViewModel(Comment c, string userId)
        {
            return new CommentViewModel() { Id = c.Id, Author = c.Author.UserName, Text = c.Text, Time = c.Time.ToString(), IsAuthor = userId == c.Author.Id };
        }
        public CommentViewModel GetCommentById(int id, string userId)
        {
            return GetCommentViewModel(commentRep.FindById(id), userId);
        }
        public bool IsCommentAuthor(int commentId, string authorId)
        {
            return  commentRep.FindById(commentId).Author.Id == authorId;
        }
        private List<Comment> FindPostComments(Post p)
        {
            return commentRep.GetAll().ToList().Where(c => c.Post == p).ToList();
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    postRep.Dispose();
                    commentRep.Dispose();
                }
                this.disposed = true;
            }
        }
        #endregion
    }
}
