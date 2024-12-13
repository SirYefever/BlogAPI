using API.Dto;

namespace Core.Models;

public class PostListRequest
{
    public PostListRequest(List<Guid>? tags, string? author, int? min, int? max, PostSorting? sorting,
        bool onlyMyCommunities, int? page, int? size)
    {
        Tags = tags;
        PartOfAuthorName = author;
        MinReadingTime = min;
        MaxReadingTime = max;
        Sorting = sorting;
        OnlyMyCommunities = onlyMyCommunities;
        Page = page;
        PageSize = size;
    }

    public List<Guid>? Tags { get; set; }
    public string? PartOfAuthorName { get; set; }
    public int? MinReadingTime { get; set; }
    public int? MaxReadingTime { get; set; }
    public PostSorting? Sorting { get; set; }
    public bool OnlyMyCommunities { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 5;
}