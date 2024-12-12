using API.Dto;
using Core.Models;

namespace Core.InterfaceContracts;

public interface ICommunityRepository
{
    Task<Community> Add(Community community);
    Task<Community> GetById(Guid id);
    Task<List<Community>> GetAll();
    Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request, List<PostLike> likes, Guid userId);
    Task<int> GetPostQuantity(Guid communityId);
}