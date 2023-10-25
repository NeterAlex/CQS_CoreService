using System.Text.Json;
using BAMCIS.GeoJSON;
using CQS_CoreService.Core.Entity;
using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IRegionDataService
{
    public Task<bool> ImportDataFromGeoJson(string regionName, string regionLocation, string description, IFormFile jsonFile);
    public Task<RegionEntity> GetData(int regionId);
}

public class RegionDataService : IRegionDataService, ITransient
{
    private readonly ISqlSugarClient _db;

    public RegionDataService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<bool> ImportDataFromGeoJson(string regionName, string regionLocation, string description, IFormFile jsonFile)
    {
        try
        {
            if (jsonFile.Length != 0)
            {
                // 读取到文件流
                await using var stream = jsonFile.OpenReadStream();
                using var geoJson = await JsonDocument.ParseAsync(stream);
                var featureCollection = FeatureCollection.FromJson(geoJson.RootElement.ToString());
                // 组装数据
                var dataList = new List<RegionDataEntity>();
                foreach (var feature in featureCollection.Features)
                {
                    _ = feature.Properties.TryGetValue("FID", out var fid);
                    var data = new RegionDataEntity
                    {
                        Type = "normal",
                        FId = fid?.ToString() ?? "-1",
                        GeoJson = feature.ToJson(),
                        ExtraJson = "{}",
                        Description = "未设置描述",
                        Location = regionLocation
                    };
                    dataList.Add(data);
                }

                var region = new RegionEntity
                {
                    Name = regionName,
                    Location = regionLocation,
                    Features = dataList
                };

                // 插入数据库
                _ = await _db
                    .InsertNav(region)
                    .Include(
                        r => r.Features,
                        new InsertNavOptions { OneToManyIfExistsNoInsert = true }
                    )
                    .ExecuteCommandAsync();
                return true;
            }
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }

        return false;
    }

    public async Task<RegionEntity> GetData(int regionId)
    {
        try
        {
            var region = await _db.Queryable<RegionEntity>()
                .Where(i => i.Id == regionId)
                .Includes(r => r.Features)
                .SingleAsync();
            return region;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}