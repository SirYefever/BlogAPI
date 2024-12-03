using API.Dto;
using Core.Models;

namespace API.Converters;

public class CommentConverter
{
    public static Comment CreateCommentDtoToComment(CreateCommentDto dto)
    {
        var comment = new Comment();
        comment.Content = dto.Content;
        comment.ParentId = dto.ParentId;
        return comment;
    }
    
}