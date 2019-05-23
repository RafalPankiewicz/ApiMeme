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
using AutoMapper;
using Api.DTO;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private IMapper _mapper;
        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

      
       
        [Route("GetCommentsByMemeID/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByMemeID(int id)
        {

            try
            {
                var comments = await _commentService.GetAllCommentByMemeId(id);
                var Dtos = _mapper.Map<IList<CommentDto>>(comments);
                // save 
                return Ok(Dtos);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {

            try
            {
                 
                var comment = await _commentService.GetCommentByIdAsync(id);
                var dto = _mapper.Map<CommentDto>(comment);
                return Ok(dto);
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
        public async Task<IActionResult> PutComment(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest();
            }


            try
            {
                // save 
                commentDto.CreationDate = DateTime.Now;
                var comment = _mapper.Map<Comment>(commentDto);
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
        public async Task<ActionResult<Comment>> PostComment([FromBody]CommentDto commentDto)
        {

            try
            {
                // save 
                commentDto.CreationDate = DateTime.Now;
                var comment = _mapper.Map<Comment>(commentDto);
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
