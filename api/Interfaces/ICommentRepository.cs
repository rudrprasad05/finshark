using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
       Task<List<Comment>> GetAllCommentsAsync();
       Task<Comment?> GetByIdAsync(int id);

       Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, EditCommentDTO editCommentDTO);

        Task<Comment?> DeleteAsync(int id);

    }
}