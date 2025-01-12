using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;


        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var commentsDTO = await _commentRepository.GetAllCommentsAsync();
            var comments = commentsDTO.Select(x => x.ToCommentDTO());
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment == null)
            {
                return BadRequest("");
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute] int id, [FromBody] CreateCommentDTO commentDTO){
            var exists = await _stockRepository.StockExists(id);
            if(!exists){
                return BadRequest("");
            }
            var comment = commentDTO.ToCommentFromCreateDTO(id);
            await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetOne), new { id = comment.Id }, comment.ToCommentDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EditCommentDTO editCommentDTO)
        {
            var commentModel = await _commentRepository.UpdateAsync(id, editCommentDTO);
            
            if (commentModel == null){
                return NotFound();
            }
            return Ok(commentModel.ToCommentDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var model = await _commentRepository.DeleteAsync(id);
            if(model == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}