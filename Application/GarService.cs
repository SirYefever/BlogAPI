using Core;
using Core.InterfaceContracts;
using Core.Models;
using Core.Models.Gar;
using Core.ServiceContracts;

namespace Application;

public class GarService: IGarService
{
    private readonly IGarRepository _garRepository;
    
    public GarService(IGarRepository garRepository)
    {
        _garRepository = garRepository;
    }
    
    
    public async Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectId)
    {
        var result = await _garRepository.GetAddressChainAsync(objectId);
        return result;
    }

}