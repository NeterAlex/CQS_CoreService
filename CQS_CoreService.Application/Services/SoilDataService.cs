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

    public Task<List<SoilDataEntity>> GetSoilDataFromUserGroup(int userGroupId, int pageSize = 50, int pageIndex = 1);
    public Task<SoilDataEntity> GetSoilData(int soilDataId);
    public Task<List<SoilDataEntity>> GetSoilDataListFromUser(int userId, int pageSize = 50, int pageIndex = 1);
}

public class SoilDataService : ISoilDataService, ITransient
{
    private readonly ISqlSugarClient _db;

    public SoilDataService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    ///     根据数据文件创建计算耕地数据
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="location"></param>
    /// <param name="description"></param>
    /// <param name="rawFile"></param>
    /// <param name="regionFile"></param>
    /// <returns></returns>
    /// <exception cref="FileLoadException"></exception>
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
                regionJson = Encoding.UTF8.GetString(bytesRegion).Replace("\r\n", "");
            }

            if (rawFile.Length == 0) throw new FileLoadException("文件为空");
            var bytesRaw = new byte[rawFile.Length];
            await using var streamRaw = rawFile.OpenReadStream();
            _ = await streamRaw.ReadAsync(bytesRaw.AsMemory(0, (int)rawFile.Length));

            // 请求 PyServer
            var result = await QueryPyServer(bytesRaw);
            var user = await _db.Queryable<UserEntity>().Where(i => i.Id == userId).SingleAsync();
            //Console.WriteLine(result.RawJson);

            // 构建 SoilData
            var soilData = new SoilDataEntity
            {
                RegionJson = regionJson.Trim(),
                RawJson = result.RawJson.Replace("'", "\""),
                GeoJson = result.GeoJson.Replace("'", "\""),
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

    /// <summary>
    ///     分页获取指定用户的所有耕地数据
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public async Task<List<SoilDataEntity>> GetSoilDataListFromUser(int userId, int pageSize = 50, int pageIndex = 1)
    {
        try
        {
            RefAsync<int> count = 0;
            var list = await _db.Queryable<SoilDataEntity>()
                .Includes(i => i.Owner)
                .Where(i => i.OwnerId == userId)
                .ToPageListAsync(pageIndex, pageSize, count);
            return list;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     获取指定UserGroup下的所有耕地数据
    /// </summary>
    /// <param name="userGroupId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public async Task<List<SoilDataEntity>> GetSoilDataFromUserGroup(int userGroupId, int pageSize = 50, int pageIndex = 1)
    {
        try
        {
            RefAsync<int> count = 0;
            var list = await
                _db.Queryable<SoilDataEntity, UserEntity>((d, u) => d.OwnerId == u.Id)
                    .LeftJoin<UserGroupEntity>((d, u, ug) => u.UserGroups.Contains(ug))
                    .Where((d, u, ug) => ug.Id == userGroupId)
                    .Select((d, u) => d)
                    .ToPageListAsync(pageIndex, pageSize, count);
            return list;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     获取指定的耕地数据
    /// </summary>
    /// <param name="soilDataId"></param>
    /// <returns></returns>
    public async Task<SoilDataEntity> GetSoilData(int soilDataId)
    {
        try
        {
            var result = await _db.Queryable<SoilDataEntity>().Where(i => i.Id == soilDataId).SingleAsync();
            return result;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     请求PyServer
    /// </summary>
    /// <param name="rawFile"></param>
    /// <returns></returns>
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