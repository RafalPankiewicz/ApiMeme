using Api.Database;
using Api.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository
{
 
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllComment();
        Task<IEnumerable<Comment>> GetAllCommentByMemeId(int id);
        Task<Comment> GetCommentByIdAsync(int id);

        void AddComment(Comment comment);
        void DeleteComment(Comment comment);
        void UpdateComment(Comment comment);
        Task SaveAsync();
    }
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentByMemeId(int id)
        {
            return await _context.Comments.Where(c => c.MemeID == id).ToListAsync();
        }
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateComment(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
        }
    }
}
