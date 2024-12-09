using Core.Models.Gar;

namespace Core.InterfaceContracts;

public interface IGarRepository
{
    public Task<AsAddrObj> GetAddressObjByGuidAsync(Guid objectGuid);
    public Task<AsHouses> GetHouseByGuidAsync(Guid objectGuid);
    public Task<AsHouses> GetHouseByIdAsync(long objectId);
    public Task<AsAddrObj> GetAddressObjByIdAsync(long objectId);
    public Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectId);
    public Task<List<SearchAddressModel>> FillAddressChain(long currentId, List<SearchAddressModel> chain);
    public Task<SearchAddressModel> GetParentObject(long objectId);
}
