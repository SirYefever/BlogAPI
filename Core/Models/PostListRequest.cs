using Core.Models;

namespace API.Dto;

public class PostListRequest//TODO: figure out weather it's supposed to be in Core
{
    public List<Tag>? Tags { get; set; }
    public string? PartOfAuthorName { get; set; }
    public int? MinReadingTime { get; set; }
    public int? MaxReadingTime { get; set; }
    public PostSorting? Sorting { get; set; }
    public bool? OnlyMyCommunities { get; set; }
    public int? Page { get; set; }//TODO: set default values for Page and PageSize
    public int? PageSize { get; set; }
}