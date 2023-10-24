using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IRegionDataService
{
    public Task<bool> ImportDataFromGeoJson(int userId, string location, string description, IFormFile jsonFile);
}

public class RegionDataService : IRegionDataService, ITransient
{
    public async Task<bool> ImportDataFromGeoJson(int userId, string location, string description, IFormFile jsonFile)
    {
        try
        {
            return true;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}