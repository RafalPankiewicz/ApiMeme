using Api.Database;
using Api.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository
{

    public interface IMemeRepository
    {
        Task <IEnumerable<Meme>> GetAllMeme();
        Task<Meme> GetMemeByIdAsync(int id); 
        void AddMeme(Meme meme);
        void DeleteMeme(Meme meme);
        void UpdateMeme(Meme meme);
        Task SaveAsync();
    }
    public class MemeRepository : IMemeRepository
    {
        private readonly DataContext _context;

        public MemeRepository(DataContext context)
        {
            _context = context;
        }

        public void AddMeme(Meme meme)
        {
            _context.Memes.Add(meme);
        }

        public void DeleteMeme(Meme meme)
        {
            _context.Memes.Remove(meme);
        }

        public async Task<IEnumerable<Meme>> GetAllMeme()
        {
            return await _context.Memes.Include(c => c.User).ToListAsync();
        }

        public async Task<Meme> GetMemeByIdAsync(int id)
        {
            return await _context.Memes.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateMeme(Meme meme)
        {
            _context.Entry(meme).State = EntityState.Modified;
        }
    }
}
