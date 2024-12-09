using API.Dto;
using Core.Models.Gar;

namespace Core.ServiceContracts;

public interface IGarService
{
    public Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectId);
}