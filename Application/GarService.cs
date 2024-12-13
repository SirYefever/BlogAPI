using Core;
using Core.InterfaceContracts;
using Core.ServiceContracts;

namespace Application;

public class GarService : IGarService
{
    private readonly IGarRepository _garRepository;

    public GarService(IGarRepository garRepository)
    {
        _garRepository = garRepository;
    }

    public async Task<List<SearchAddressModel>> Search(long parentId, string? query)
    {
        if (query == null)
            return await _garRepository.SearchFirstTen(parentId);

        return await _garRepository.Search(parentId, query);
    }

    public async Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectId)
    {
        var result = await _garRepository.GetAddressChainAsync(objectId);
        return result;
    }
}