using API.Dto;
using Core.Models;

namespace API.Converters;

public class UserCommunityConverters
{
    public static CommunityUserDto UserCommunityToCommunityUserDto(UserCommunity userCommunity)
    {
        var communityUserDto = new CommunityUserDto();
        communityUserDto.CommunityId = userCommunity.CommunityId;
        communityUserDto.UserId = userCommunity.UserId;
        communityUserDto.Role = userCommunity.HighestRole;
        return communityUserDto;
    }
}