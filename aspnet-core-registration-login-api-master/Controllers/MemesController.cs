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

        // PUT: api/Memes/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeme(int id, [FromBody] Meme meme)
        {
            if (id != meme.Id)
            {
                return BadRequest();
            }


            try
            {
                // save 
                await _memeService.UpdateMemeAsync(meme);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
          

            
        }

        // POST: api/Memes
     //   [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostMeme([FromBody] Meme meme)
        {     
                try {
                 meme.CerationDate = DateTime.Now;
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
       // [Authorize]
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

   
    }
}
