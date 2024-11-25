using Core.Models;

namespace API.Dto;

public class PostPagesListDto
{
    public PostDto[] Posts { get; set; }
    public PageInfoModel Pagination { get; set; }
}