using System.Text;
using CQS_CoreService.Core.Dto;
using CQS_CoreService.Core.Entity;
using CQS_CoreService.Core.Providers;
using Furion.Logging.Extensions;
using Furion.RemoteRequest;
using Furion.RemoteRequest.Extensions;

namespace CQS_CoreService.Application;

public interface ISoilDataService
{
    public Task<SoilDataEntity> CreateSoilData(int userId, string location, string description, IFormFile rawFile,
        IFormFile regionFile);
}

public class SoilDataService : ISoilDataService, ITransient
{
    private readonly ISqlSugarClient _db;

    public SoilDataService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<SoilDataEntity> CreateSoilData(int userId, string location, string description, IFormFile rawFile,
        IFormFile regionFile)
    {
        try
        {
            // 读取文件
            var regionJson = "";
            if (regionFile.Length != 0)
            {
                var bytesRegion = new byte[regionFile.Length];
                await using var streamRegion = regionFile.OpenReadStream();
                _ = await streamRegion.ReadAsync(bytesRegion.AsMemory(0, (int)regionFile.Length));
                regionJson = Encoding.UTF8.GetString(bytesRegion);
            }

            if (rawFile.Length == 0) throw new FileLoadException("文件为空");
            var bytesRaw = new byte[rawFile.Length];
            await using var streamRaw = rawFile.OpenReadStream();
            _ = await streamRaw.ReadAsync(bytesRaw.AsMemory(0, (int)rawFile.Length));

            // 请求 PyServer
            var result = await QueryPyServer(bytesRaw);
            var user = await _db.Queryable<UserEntity>().Where(i => i.Id == userId).SingleAsync();
            Console.WriteLine(result.RawJson);

            // 构建 SoilData
            var soilData = new SoilDataEntity
            {
                RegionJson = regionJson,
                RawJson = result.RawJson,
                GeoJson = result.GeoJson,
                ExtraJson = @"{}",
                Location = location ?? "未设置区域",
                Owner = user,
                OwnerId = user.Id,
                Type = "default",
                Description = description ?? "标准田块数据",
                CreatedTime = DateTime.Now
            };
            return await _db.Insertable(soilData).ExecuteReturnEntityAsync();
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    private async Task<PyQueryResultDto> QueryPyServer(byte[] rawFile)
    {
        try
        {
            var queryData = await "http://localhost:8001/api/calc"
                .SetContentType("multipart/form-data")
                .SetContentEncoding(Encoding.UTF8)
                .SetJsonSerialization<NewtonsoftJsonSerializerProvider>()
                .SetFiles(HttpFile.Create("raw_file", rawFile, "raw_file.xlsx"))
                .PostAsAsync<PyQueryResultDto>();
            return queryData;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}