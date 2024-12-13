using System.Globalization;
using Core;
using Core.InterfaceContracts;
using Core.Models.Gar;
using Microsoft.EntityFrameworkCore;

namespace Persistence.GarContext;

public class GarRepository: IGarRepository
{
    private readonly GarDbContext _context;
    private readonly GarConverter _garConverter;
    private readonly Guid _tomskRegionGuid = Guid.Parse("889b1f3a-98aa-40fc-9d3d-0f41192758ab");
    private readonly long _tomskRegionId = 1281271;
    private readonly Guid _tomskCityGuid = Guid.Parse("e3b0eae8-a4ce-4779-ae04-5c0797de66be");
    private readonly long _tomskCityId = 1281284;

    public GarRepository(GarDbContext context, GarConverter garConverter)
    {
        _context = context;
        _garConverter = garConverter;
    }

    //TODO: manage awaiting in these 4 functions
    public async Task<AsAddrObj> GetAddressObjByGuidAsync(Guid objectGuid)
    {
        var addresses = _context.AsAddrObj.AsQueryable();
        AsAddrObj searchedAddress = addresses.Where(
            a => a.Objectguid == objectGuid
        ).ToList()[0]; //TODO: manage UTC thing
        
        if (searchedAddress == null)
            throw new KeyNotFoundException("Address with id=" + objectGuid.ToString() + "not found in database.");
            
        return searchedAddress;
        // catch
        // {
        //     return null;
        // }
    }
    
    public async Task<AsAddrObj> GetAddressObjByIdAsync(long objectId)
    {
        try
        {
            var addresses = _context.AsAddrObj.AsQueryable();
            AsAddrObj searchedAddress = addresses.Where(
                a => a.Objectid == objectId
            ).ToList()[0]; //TODO: manage UTC thing
            return searchedAddress;
        }
        catch
        {
            return null;
        }
    }

    public async Task<AsHouses> GetHouseByGuidAsync(Guid objectGuid)
    {
        var houses = _context.AsHouses.AsQueryable();
        try
        {
            AsHouses searchedHouse = houses.Where(
                a => a.Objectguid == objectGuid 
            ).ToList()[0]; //TODO: manage UTC thing
            return searchedHouse;

        }
        catch
        {
            return null;
        }
    }

    public async Task<AsHouses> GetHouseByIdAsync(long objectId)
    {
        try
        {
            var houses = _context.AsHouses.AsQueryable();
            AsHouses searchedHouse = houses.Where(
                a => a.Objectid == objectId
            ).ToList()[0]; //TODO: manage UTC thing
            return searchedHouse;
        }
        catch
        {
            return null;
        }
    }
    public async Task<SearchAddressModel> GetParentObject(long objectId)
    {
        var _garConverter = new GarConverter();
        long parentId = 0;
        try
        {
            var hierarchyObjects = _context.AsAdmHierarchy.AsQueryable();
            parentId = (long)hierarchyObjects.Where(
                a => a.Objectid == objectId &&
                     a.Enddate > DateOnly.FromDateTime(DateTime.UtcNow)
            ).ToList()[0].Parentobjid; //TODO: manage UTC thing
        }
        catch
        {
            // Console.Error.WriteLine(parentId);
            // throw new Exception("Something went wrong, parentId = " + parentId);
            return null;
        }

        var result = new SearchAddressModel();
        try
        {
            var parentObject = await GetAddressObjByIdAsync(parentId);
            result = _garConverter.AsAddrObjToSearchAddressModel(parentObject);
        }
        catch
        {
            var parentHouse = await GetHouseByIdAsync(parentId);
            if (parentHouse != null)
                result = _garConverter.AsHousesToSearchAddressModel(parentHouse);
        }
        return result;
    }

    public async Task<List<SearchAddressModel>> GetAddressChainAsync(Guid objectGuid)
    {
        var chain = new List<SearchAddressModel>();
        var searchedObject = await GetAddressObjByGuidAsync(objectGuid);
        var model = new SearchAddressModel();
        long currentId;
        if (searchedObject == null)
        {
            var searchedHouse = await GetHouseByGuidAsync(objectGuid);
            model = _garConverter.AsHousesToSearchAddressModel(searchedHouse);
            currentId = searchedHouse.Objectid;
        }
        else
        {
            model = _garConverter.AsAddrObjToSearchAddressModel(searchedObject);
            currentId = searchedObject.Objectid;
        }
        chain.Add(model);
        
        var hierarchyObjects = _context.AsAdmHierarchy.AsQueryable();
        FillAddressChain(currentId, chain);
        
        return chain;
    }


    public async Task<List<SearchAddressModel>> FillAddressChain(long currentId, List<SearchAddressModel> chain)
    {
        if (currentId == _tomskRegionId)
        {
            chain.Reverse();
            return chain;
        }
        var searchedModel = await GetParentObject(currentId);
        chain.Add(searchedModel);
        
        FillAddressChain(searchedModel.ObjectId, chain);
        return new List<SearchAddressModel>();
    }

    public async Task<SearchAddressModel> GetObject(long objectId)
    {
        var result = new SearchAddressModel();
        try
        {
            var addressObject = await GetAddressObjByIdAsync(objectId);
            result = _garConverter.AsAddrObjToSearchAddressModel(addressObject);
        }
        catch
        {
            var house = await GetHouseByIdAsync(objectId);
            if (house != null)
                result = _garConverter.AsHousesToSearchAddressModel(house);
        }

        return result;
    }

    public async Task<List<SearchAddressModel>> Search(long parentObjectId, string? query)
    {
        var hierarchyObjects = _context.AsAdmHierarchy.AsQueryable();
        var searchedItems = await hierarchyObjects.Where(h => h.Parentobjid == parentObjectId).ToListAsync();
        var searchedItemsConverted = new List<SearchAddressModel>();
        foreach (var searchedItem in searchedItems)
        {
            var curItem = await GetObject((long)searchedItem.Objectid);
            if (curItem.ObjectId != 0)
                if (curItem.Text.Contains(query))
                    searchedItemsConverted.Add(await GetObject((long)searchedItem.Objectid));
        }
        return searchedItemsConverted;
    }

    public async Task<List<SearchAddressModel>> SearchFirstTen(long parentObjectId)
    {
        var outputLimit = 10;
        var hierarchyObjects = _context.AsAdmHierarchy.AsQueryable();
        var searchedItems = await hierarchyObjects.Where(h => h.Parentobjid == parentObjectId).ToListAsync();
        var searchedItemsConverted = new List<SearchAddressModel>();
        for (int i = 0; i < Math.Min(outputLimit, searchedItems.Count); i++)
        {
            var curItem = await GetObject((long)searchedItems[i].Objectid);
            if (curItem.ObjectId != 0)
                searchedItemsConverted.Add(await GetObject((long)searchedItems[i].Objectid));
        }
        return searchedItemsConverted;
    }

    public async Task<bool> IsAddressGuidReal(Guid objectGuid)
    {
        var houses = _context.AsHouses.AsQueryable();
        List<AsHouses> houseList = await houses.Where(
            a => a.Objectguid == objectGuid 
        ).ToListAsync(); //TODO: manage UTC thing
        if (!houseList.Any())
            return false;
        
        return true;
    }

    public async Task ConfirmAddressIsReal(Guid objectGuid)
    {
        if (!await IsAddressGuidReal(objectGuid))
            throw new KeyNotFoundException("Address not found");
    }
}
