using API.Dto;
using Core.Models;

namespace API.Converters;

public class CommunityConverters
{
    public static Community CreateCommunityDtoToCommunity(CreateCommunityDto dto)
    {
        var community = new Community();
        community.Name = dto.Name;
        community.Description = dto.Description;
        community.CreateTime = DateTime.Now;
        community.IsClosed = dto.IsClosed;
        return community;
    }
    
    public static CommunityDto CommunityToCommunityDto(Community community)
    {
        var communityDto = new CommunityDto();
        communityDto.Id = community.Id;
        communityDto.Name = community.Name;
        communityDto.Description = community.Description;
        communityDto.IsClosed = community.IsClosed;
        communityDto.SubscribersCount = community.SubscribersCount;
        return communityDto;
    }
    
    public static CommunityFullDto CommunityToCommunityFullDto(Community community)
    {
        var dto = new CommunityFullDto();
        dto.Id = community.Id;
        dto.Name = community.Name;
        dto.Description = community.Description;
        dto.IsClosed = community.IsClosed;
        dto.SubscribersCount = community.SubscribersCount;
        foreach (var subscriber in community.Subscribers)
        {
            dto.Administrators.Add(UserConverters.UserToUserDto(subscriber));
        }
        return dto;
    }
}