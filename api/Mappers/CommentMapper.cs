using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel){
           
            return new CommentDTO{
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content =   commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentDTO dto, int id){
            return new Comment{
                Title = dto.Title ?? string.Empty,
                Content = dto.Content ?? string.Empty,
                CreatedOn = dto.CreatedOn,
                StockId = id

            };
        }
        public static Comment ToCommentFromUpdateDTO(this CreateCommentDTO dto){
            return new Comment{
                Title = dto.Title ?? string.Empty,
                Content = dto.Content ?? string.Empty,
            };
        }
    }
}