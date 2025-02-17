using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentsController(CommentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await _context.UserComments.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var value = await _context.UserComments.FindAsync(id);
            if (value == null)
            {
                return NotFound("Rəy tapılmadı");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(UserComment userComment)
        {
            await _context.UserComments.AddAsync(userComment);
            await _context.SaveChangesAsync();
            return Ok("Rəy uğurla əlavə olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UserComment userComment)
        {
            var existingComment = await _context.UserComments.FindAsync(userComment.UserCommentId);
            if (existingComment == null)
            {
                return NotFound("Rəy tapılmadı");
            }

            existingComment.CommentDetail = userComment.CommentDetail;
            existingComment.ImageUrl = userComment.ImageUrl;
            existingComment.Rating = userComment.Rating;
            existingComment.CreatedDate = userComment.CreatedDate;
            existingComment.Email = userComment.Email;
            existingComment.NameSurname = userComment.NameSurname;
            existingComment.Status = userComment.Status;
            existingComment.ProductId = userComment.ProductId;

            _context.UserComments.Update(existingComment);
            await _context.SaveChangesAsync();
            return Ok("Rəy uğurla güncəlləndi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _context.UserComments.FindAsync(id);
            if (value == null)
            {
                return NotFound("Rəy tapılmadı");
            }

            _context.UserComments.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Rəy uğurla silindi");
        }

        [HttpGet("CommentListByProductId/{id}")]
        public async Task<IActionResult> CommentListByProductId(string id)
        {
            var values = await _context.UserComments.Where(x => x.ProductId == id).ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetActiveCommentCount")]
        public IActionResult GetActiveCommentCount()
        {
            int value = _context.UserComments.Where(x=>x.Status== true).Count();
            return Ok(value);
        }

        [HttpGet("GetPassiveCommentCount")]
        public IActionResult GetPassiveCommentCount()
        {
            int value = _context.UserComments.Where(x => x.Status == false).Count();
            return Ok(value);
        }

        [HttpGet("GetTotalCommentCount")]
        public IActionResult GetTotalCommentCount()
        {
            int value = _context.UserComments.Count();
            return Ok(value);
        }
    }
}
