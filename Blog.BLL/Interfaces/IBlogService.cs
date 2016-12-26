using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IBlogService : IDisposable
    {
        Task<OperationDetails> CreateUser(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        PostDTO AddPost(string topic, string text, string userId);
        void DeletePost(int id);
        List<PostDTO> GetNextPosts(string userId, int skip);
        CommentDTO AddComment(int postId, string text, string userId);
        void DeleteComment(int id);
        CommentDTO GetCommentById(int id, string userId);
        bool IsCommentAuthor(int commentId, string userId);

    }
}
