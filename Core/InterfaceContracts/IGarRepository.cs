using Core.Models.Gar;

namespace Core.InterfaceContracts;

public interface IGarRepository
{
    public Task<AsAddrObj> Test(Guid objectGuid);
    public Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectGuid);
}