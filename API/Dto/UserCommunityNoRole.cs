namespace API.Dto;

public class UserCommunityNoRole //TODO: Delete?
{
    public UserCommunityNoRole(Guid communityId, Guid userId)
    {
        UserId = userId;
        CommunityId = communityId;
    }
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
}