using Core.Models;

namespace API.Dto;

public class PostListRequest//TODO: figure out weather it's supposed to be in Core
{
    public List<Tag>? Tags { get; set; }
    public string? PartOfAuthorName { get; set; }// tick
    public int? MinReadingTime { get; set; }// tick
    public int? MaxReadingTime { get; set; }// tick
    public PostSorting? Sorting { get; set; }//tick
    public bool OnlyMyCommunities { get; set; }// delayed TODO: finish it.
    public int? Page { get; set; } = 1;// tick //TODO: set default values for Page and PageSize
    public int? PageSize { get; set; } = 5; // tick
}