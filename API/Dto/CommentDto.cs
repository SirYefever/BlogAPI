using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto;

public class CommentDto
{
    public CommentDto(Comment comment)
    {
        Id = comment.Id;
        Content = comment.Content;
        CreateTime = comment.CreateTime;
        ModifiedDate = comment.ModifiedDate;
        DeleteDate = comment.DeleteDate;
        AuthorId = comment.AuthorId;
        Author = comment.Author;
        SubComments = comment.SubComments;
    }

    [Required] public Guid Id { get; set; }

    [Required] public DateTime CreateTime { get; set; }

    [Required] public string Content { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeleteDate { get; set; }

    [Required] public Guid AuthorId { get; set; }

    [Required] public string Author { get; set; }

    [Required] public int SubComments { get; set; }
}