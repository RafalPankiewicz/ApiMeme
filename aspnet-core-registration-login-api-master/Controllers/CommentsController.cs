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

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {

            return Ok(await _commentService.GetAllComment());
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {

            try
            {
                // save 
                var Comment = await _commentService.GetCommentByIdAsync(id);
                return Ok(Comment);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }



        }

        // PUT: api/Comments/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }


            try
            {
                // save 
                await _commentService.UpdateCommentAsync(comment);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }



        }

        // POST: api/Comments
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody]Comment comment)
        {

            try
            {
                // save 
                await _commentService.CreateCommentAsync(comment);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE: api/Comments/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            try
            {
                // save 
                await _commentService.DeleteCommentAsync(id);
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
