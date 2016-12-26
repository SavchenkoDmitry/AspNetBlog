using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;
using Blog.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Blog.BLL.Interfaces;
using Blog.DAL.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Blog.BLL.Services
{
    public class BlogService : IBlogService
    {
        const int PostsInOnePage = 3;
        const string StandartRole = "user";

        IBlogUnitOfWork Database { get; set; }

        public BlogService(IBlogUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> CreateUser(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                await Database.UserManager.CreateAsync(user, userDto.Password);
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                if (userDto.Role != StandartRole)
                {
                    await Database.UserManager.AddToRoleAsync(user.Id, StandartRole);
                }
                await Database.SaveAsync();
                return new OperationDetails(true, "Register successful", "");

            }
            else
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        
        private PostDTO GetDPostDTO(Post p, string userId)
        {
            List<CommentDTO> comentsDTO = new List<CommentDTO>();
            foreach (Comment c in p.coments)
            {
                comentsDTO.Add(GetCommentDTO(c, userId));
            }
            return new PostDTO() { author = p.author.UserName, id = p.id, text = p.text, time = p.time.ToString(), topic = p.topic, coments = comentsDTO };
        }
        private List<PostDTO> GetPostListDTO(List<Post> posts, string userId)
        {
            List<PostDTO> postsDTO = new List<PostDTO>();
            List<Comment> Comments = Database.CommentRep.GetInRange(posts);

            foreach (Post p in posts)
            {
                List<CommentDTO> comentsDTO = new List<CommentDTO>();
                foreach (Comment c in Comments.Where(c => c.post == p))
                {
                    comentsDTO.Add(GetCommentDTO(c, userId));
                }
                postsDTO.Add(new PostDTO() { author = p.author.UserName, id = p.id, text = p.text, time = p.time.ToString(), topic = p.topic, coments = comentsDTO });
            }
            return postsDTO;
        }


        public PostDTO AddPost(string topic, string text, string userId)
        {
            Post p = new Post() { text = text, topic = topic, author = Database.UserManager.FindById(userId), time = DateTime.Now };
            Database.PostRep.Create(p);
            return GetDPostDTO(p, userId);
        }

        public void DeletePost(int id)
        {
            Post p = Database.PostRep.Get(id);
            foreach (Comment c in p.coments)
            {
                Database.CommentRep.Delete(c);
            }
            Database.PostRep.Delete(p);
            Database.SaveAsync();
        }

        public List<PostDTO> GetNextPosts(string userId, int skip)
        {
            List<Post> posts = Database.PostRep.GetForPage(skip, PostsInOnePage);
            return GetPostListDTO(posts, userId); ;
        }


        public CommentDTO AddComment(int postId, string text, string userId)
        {
            Comment c = new Comment() { author = Database.UserManager.FindById(userId), post = Database.PostRep.Get(postId), text = text, time = DateTime.Now };
            Database.CommentRep.Create(c);
            return GetCommentDTO(c, userId);
        }
        public void DeleteComment(int id)
        {
            Database.CommentRep.Delete(id);
            Database.SaveAsync();
        }
        private CommentDTO GetCommentDTO(Comment c, string userId)
        {
            return new CommentDTO() { id = c.id, author = c.author.UserName, text = c.text, time = c.time.ToString(), isAuthor = userId == c.author.Id };
        }
        public CommentDTO GetCommentById(int id, string userId)
        {
            return GetCommentDTO(Database.CommentRep.Get(id), userId);
        }
        public bool IsCommentAuthor(int commentId, string authorId)
        {
            return  Database.CommentRep.Get(commentId).author.Id == authorId;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
