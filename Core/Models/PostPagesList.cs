
namespace Core.Models;

public class PostPagesList //TODO: figure out weather it's supposed to be in Core
{
    public Post[] Posts { get; set; }
    public PageInfoModel Pagination { get; set; }
}