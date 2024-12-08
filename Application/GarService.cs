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
    
    
    public Task<List<SearchAddressModel>> GetAddressChainAsync()
    {
        var addressModel = new SearchAddressModel();
        throw new NotImplementedException();
    }

}