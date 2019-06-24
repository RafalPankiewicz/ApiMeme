using Api.Database.Entity;
using Api.Helpers;
using Api.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{

    public interface IMemeService
    {
        Task<IEnumerable<Meme>> GetAllMeme();
        Task<Meme> GetMemeByIdAsync(int id);
        Task CreateMemeAsync(Meme meme);
        Task DeleteMemeAsync(int id);
        Task UpdateMemeAsync(Meme meme);
        Task UpRateMemeAsync(int id);
        Task DownRateMemeAsync(int id);

    }
    public class MemeService : IMemeService
    {
        private readonly IMemeRepository _memeRepository;

        public MemeService(IMemeRepository memeRepository)
        {
            this._memeRepository = memeRepository;
        }

        public async Task CreateMemeAsync(Meme meme)
        {
            _memeRepository.AddMeme(meme);
            await _memeRepository.SaveAsync();
        }

        public async Task DeleteMemeAsync(int id)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(id);

            if (meme == null)
                throw new AppException("Meme not found");

            _memeRepository.DeleteMeme(meme);
            await _memeRepository.SaveAsync();           
        }

      

        public async Task<IEnumerable<Meme>> GetAllMeme()
        {
            return await _memeRepository.GetAllMeme();
        }

        public async Task<Meme> GetMemeByIdAsync(int id)
        {

            var meme = await _memeRepository.GetMemeByIdAsync(id);

            if (meme == null)
            {
                throw new AppException("Meme not foud");
            }
            return meme;
        }

       

        public async Task UpdateMemeAsync(Meme memee)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memee.Id);

            if (meme == null)
                throw new AppException("Meme not found");

            _memeRepository.UpdateMeme(meme);
            await _memeRepository.SaveAsync();
        }

        public async Task UpRateMemeAsync(int id)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(id);

            if (meme == null)
                throw new AppException("Meme not found");

            meme.Rate++;

            _memeRepository.UpdateMeme(meme);
            await _memeRepository.SaveAsync();
        }

        public async Task DownRateMemeAsync(int id)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(id);

            if (meme == null)
                throw new AppException("Meme not found");

            meme.Rate--;

            _memeRepository.UpdateMeme(meme);
            await _memeRepository.SaveAsync(); 
        }
    }
}
