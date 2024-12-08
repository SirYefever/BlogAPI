using Core;
using Core.InterfaceContracts;
using Core.Models.Gar;
using Persistence.GarContext;

namespace Persistence;

public class GarRepository: IGarRepository
{
    private readonly GarDbContext _context;

    public GarRepository(GarDbContext context)
    {
        _context = context;
    }

    public async Task<AsAddrObj> GetPresentAddressObjAsync(Guid objectGuid)
    {
        var addresses = _context.AsAddrObj.AsQueryable();
        AsAddrObj searchedAddress = addresses.Where(
                 a => a.Objectguid == objectGuid && 
                 a.Enddate > DateOnly.FromDateTime(DateTime.UtcNow)
                 ).ToList()[0]; //TODO: manage UTC thing
        return searchedAddress;
    }
    
    public async Task<AsHouses> GetPresentHouseAsync(Guid objectGuid)
    {
        var houses = _context.AsHouses.AsQueryable();
        AsHouses searchedHouse = houses.Where(
                 a => a.Objectguid == objectGuid && 
                 a.Enddate > DateOnly.FromDateTime(DateTime.UtcNow)
                 ).ToList()[0]; //TODO: manage UTC thing
        return searchedHouse;
    }


    public async Task<AsAddrObj> Test(Guid objectGuid)
    {
        var searchedAddress = await GetPresentAddressObjAsync(objectGuid);
        throw new NotImplementedException();
    }

    public async Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectGuid)
    {
        var searchedObject = await GetPresentAddressObjAsync(objectGuid);
        if (searchedObject == null)
        {
            var searchedHouse = await GetPresentHouseAsync(objectGuid);
            //Create search address model for house
        }
            //Create search address model for addr_obj
        throw new NotImplementedException();
    }
}