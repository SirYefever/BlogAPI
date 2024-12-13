using Core;
using Core.InterfaceContracts;
using Core.Models;
using Core.Models.Gar;

namespace Persistence.GarContext;

public class GarConverter
{
    private Dictionary<GarAddressLevel, string> _levels;

    public GarConverter()
    {
        _levels = new Dictionary<GarAddressLevel, string>();
        _levels.Add(GarAddressLevel.Region, "Субъект РФ");
        _levels.Add(GarAddressLevel.AdministrativeArea, "Административный район");
        _levels.Add(GarAddressLevel.MunicipalArea, "Муниципальный район");
        _levels.Add(GarAddressLevel.RuralUrbanSettlement, "Сельское/городское поселение");
        _levels.Add(GarAddressLevel.City, "Город");
        _levels.Add(GarAddressLevel.Locality, "Населенный пункт");
        _levels.Add(GarAddressLevel.ElementOfPlanningStructure, "Элемент планировочной структуры");
        _levels.Add(GarAddressLevel.ElementOfRoadNetwork, "Элемент улично-дорожной сети");
        _levels.Add(GarAddressLevel.Land, "Земельный участок");
        _levels.Add(GarAddressLevel.Building, "Здание (сооружение)");
        _levels.Add(GarAddressLevel.Room, "Помещение");
        _levels.Add(GarAddressLevel.RoomInRooms, "Помещения в пределах помещения");
        _levels.Add(GarAddressLevel.AutonomousRegionLevel, "Уровень автономного округа (устаревшее)");
        _levels.Add(GarAddressLevel.IntracityLevel, "Уровень внутригородской территории (устаревшее)");
        //In task's swagger AdditionalTerritoriesLevel are mapped to "Элемент планировочной структуры" which is №7
        _levels.Add(GarAddressLevel.AdditionalTerritoriesLevel, "Уровень дополнительных территорий (устаревшее)");
        _levels.Add(GarAddressLevel.LevelOfObjectsInAdditionalTerritories, "Уровень объектов на дополнительных территориях (устаревшее)");
        _levels.Add(GarAddressLevel.CarPlace, "Машиноместо");
    }

    public string ConstructTextFieldForAddrObj(AsAddrObj obj)
    {
        string result = "";
        if (obj.Typename != null)
            result += obj.Typename + " ";
        
        result += obj.Name;
        return result;
    }

    public string ConstructTextFieldForHouse(AsHouses house)
    {
        string result = "";
        if (house.Housenum != null)
            result = house.Housenum;

        if (house.Addnum2 != null)
            result += " к. " + house.Addnum2;
        
        if (house.Addnum1 != null)
        {
            if (house.Addnum1 == "3")
                result += " соор. " + house.Addnum1;
            else
                result += " стр. " + house.Addnum1;
        }

        return result;
    }

    public SearchAddressModel AsAddrObjToSearchAddressModel(AsAddrObj asAddrObj)
    {
        var model = new SearchAddressModel();
        model.ObjectGuid = asAddrObj.Objectguid;
        model.ObjectId = asAddrObj.Objectid;
        model.Text = ConstructTextFieldForAddrObj(asAddrObj);
        
        
        int.TryParse(asAddrObj.Level, out int level);
        model.ObjectLevel = (GarAddressLevel)level;
        model.ObjectLevelText = _levels[(GarAddressLevel)level];
        return model;
    }
    
    public SearchAddressModel AsHousesToSearchAddressModel(AsHouses house)
    {
        var model = new SearchAddressModel();
        model.ObjectGuid = house.Objectguid;
        model.ObjectId = house.Objectid;
        model.Text = ConstructTextFieldForHouse(house);
        
        model.ObjectLevel = GarAddressLevel.Building;  
        model.ObjectLevelText = _levels[GarAddressLevel.Building];
        
        return model;
    }

    // public async Task<SearchAddressModel> AsAdmHierarchyToSearchAddressModel(AsAdmHierarchy obj, IGarRepository garRepository)
    // {
    //     var model = new SearchAddressModel();
    //     model.ObjectId = (long)obj.Objectid;
    //     try
    //     {
    //         var searchedHouse = await garRepository.GetPresentHouseByIdAsync((long)obj.Objectid);
    //         model = AsHousesToSearchAddressModel(searchedHouse);
    //     }
    //     catch
    //     {
    //         var searchedAddressObject = await garRepository.GetPresentAddressObjByIdAsync((long)obj.Objectid);
    //         model = AsAddrObjToSearchAddressModel(searchedAddressObject);
    //     }
    //
    //     return model;
    // }
}