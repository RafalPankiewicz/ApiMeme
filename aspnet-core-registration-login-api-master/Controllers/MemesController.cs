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

namespace WebApi.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class MemesController : ControllerBase
    {
        private readonly IMemeService _memeService;

        public MemesController(IMemeService memeService)
        {
            _memeService = memeService;
        }

        // GET: api/Memes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meme>>> GetMemes()
        {
    
            return Ok(await _memeService.GetAllMeme()); 
        }

        // GET: api/Memes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meme>> GetMeme(int id)
        {

            try
            {
                // save 
                var meme = await _memeService.GetMemeByIdAsync(id);
                return Ok(meme);
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
                 // save 
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
