using Api.Database.Entity;
using Api.Helpers;
using Api.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComment();
        Task<Comment> GetCommentByIdAsync(int id);

        Task CreateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
        Task UpdateCommentAsync(Comment comment);

    }
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _CommentRepository;

        public CommentService(ICommentRepository CommentRepository)
        {
            this._CommentRepository = CommentRepository;
        }

        public async Task CreateCommentAsync(Comment Comment)
        {
            _CommentRepository.AddComment(Comment);
            await _CommentRepository.SaveAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _CommentRepository.GetCommentByIdAsync(id);

            if (comment == null)
                throw new AppException("Comment not found");

            _CommentRepository.DeleteComment(comment);
            await _CommentRepository.SaveAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {
            return await _CommentRepository.GetAllComment();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {

            var comment = await _CommentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                throw new AppException("Comment not foud");
            }
            return comment;
        }



        public async Task UpdateCommentAsync(Comment comment)
        {
            var commentt = await _CommentRepository.GetCommentByIdAsync(comment.Id);

            if (commentt == null)
                throw new AppException("Comment not found");



            _CommentRepository.UpdateComment(commentt);
            await _CommentRepository.SaveAsync();
        }
    }
}
