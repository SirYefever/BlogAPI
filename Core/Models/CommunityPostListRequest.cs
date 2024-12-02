using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto;

public class CommunityPostListRequest
{//TODO: figure out weather it's supposed to be in Core
    public Guid CommunityId { get; set; }
    public List<Guid>? TagGuids { get; set; }
    public PostSorting? Sorting { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 5; 
    
}