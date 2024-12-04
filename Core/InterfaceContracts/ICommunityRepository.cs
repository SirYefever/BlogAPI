using API.Dto;
using Core.Models;

namespace Core.InterfaceContracts;

public interface ICommunityRepository
{
    Task<Community> Add(Community community);
    Task<Community> GetById(Guid id);
    Task<List<Community>> GetAll();
    Task<Community> UpdateById(Guid id, Community newCommunity);
    Task DeleteById(Guid id);
    Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request, List<PostLike> likes);
}