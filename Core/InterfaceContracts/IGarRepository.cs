using Core.Models.Gar;

namespace Core.InterfaceContracts;

public interface IGarRepository
{
    Task<AsAddrObj> GetAddressObjByGuidAsync(Guid objectGuid);
    Task<AsHouses> GetHouseByGuidAsync(Guid objectGuid);
    Task<AsHouses> GetHouseByIdAsync(long objectId);
    Task<AsAddrObj> GetAddressObjByIdAsync(long objectId);
    Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectId);
    Task<List<SearchAddressModel>> FillAddressChain(long currentId, List<SearchAddressModel> chain);
    Task<SearchAddressModel> GetParentObject(long objectId);
    Task<List<SearchAddressModel>> Search(long parentObjectId, string? query);
    Task<List<SearchAddressModel>> SearchFirstTen(long parentObjectId);
    Task<bool> IsAddressGuidReal(Guid objectGuid);
    Task ConfirmAddressIsReal(Guid objectGuid);
}