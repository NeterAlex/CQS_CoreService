using System.Text;
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
            var geoJSON = "";
            if (jsonFile.Length != 0)
            {
                var bytes = new byte[jsonFile.Length];
                await using var stream = jsonFile.OpenReadStream();
                _ = await stream.ReadAsync(bytes.AsMemory(0, (int)jsonFile.Length));
                geoJSON = Encoding.UTF8.GetString(bytes);
            }

            return true;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}