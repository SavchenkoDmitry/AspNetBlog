using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Services.Common;
using Blog.ViewModels.HomeViewModels;
using Blog.ViewModels.AccountViewModels;

namespace Blog.Services.Interfaces
{
    public interface IBlogService : IDisposable
    {
        string[] GetThemes();
        PostViewModel AddPost(string topic, string text, string userId);
        void DeletePost(int id);
        PostViewModel GetPostViewModel(int postId, string userId);
        List<PostPreviewViewModel> GetNextPosts(string userId, int skip);
        CommentViewModel AddComment(int postId, string text, string userId);
        bool DeleteComment(int id);
        CommentViewModel GetCommentById(int id, string userId);
        bool IsCommentAuthor(int commentId, string userId);

    }
}
