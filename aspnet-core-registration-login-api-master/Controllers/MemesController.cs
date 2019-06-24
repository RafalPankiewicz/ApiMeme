using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Database;
using Api.Database.Entity;
using Api.Service;
using Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using MimeTypes;
using AutoMapper;
using Api.DTO;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MemesController : ControllerBase
    {
        private readonly IMemeService _memeService;
        private IMapper _mapper;
        public MemesController(IMemeService memeService, IMapper mapper)
        {
            _memeService = memeService;
            _mapper = mapper;
        }

        // GET: api/Memes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meme>>> GetMemes()
        {
            var memes = await _memeService.GetAllMeme();
            var Dtos = _mapper.Map<IList<MemeDto>>(memes);
            return Ok(Dtos);
        }

        // GET: api/Memes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meme>> GetMeme(int id)
        {
            try
            {
                var meme = await _memeService.GetMemeByIdAsync(id);
                var memeDto = _mapper.Map<MemeDto>(meme);
                return Ok(memeDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Memes
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostMeme([FromBody] MemeDto memeDto)
        {
            try
            {
                memeDto.CerationDate = DateTime.Now;
                var meme = _mapper.Map<Meme>(memeDto);
                await _memeService.CreateMemeAsync(meme);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Memes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Meme>> DeleteMeme(int id)
        {
            try
            {
                // save 
                await _memeService.DeleteMemeAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [Route("UpRate/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> UpRate(int id)
        {
            try
            {
                await _memeService.UpRateMemeAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [Route("DownRate/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> DownRate(int id)
        {
            try
            {
                await _memeService.DownRateMemeAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
