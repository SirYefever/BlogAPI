namespace API.Dto;

public class CommunityPostListRequest
{
    public Guid CommunityId { get; set; }
    public List<Guid>? TagGuids { get; set; }
    public PostSorting? Sorting { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 5;
}