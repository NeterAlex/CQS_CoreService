using System;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQS_CoreService.Web.Core.Controllers;

[Microsoft.AspNetCore.Components.Route("api/region")]
[ApiDescriptionSettings(Tag = "区域尺度数据API")]
[DynamicApiController]
public class RegionDataController
{
    private readonly IRegionDataService _regionDataService;

    public RegionDataController(IRegionDataService regionDataService)
    {
        _regionDataService = regionDataService;
    }

    /// <summary>
    ///     通过GeoJSON创建区域信息
    /// </summary>
    /// <param name="regionName"></param>
    /// <param name="regionLocation"></param>
    /// <param name="description"></param>
    /// <param name="regionJson"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<bool> CreateRegion([FromForm] string regionName, [FromForm] string regionLocation, [FromForm] string description,
        IFormFile regionJson)
    {
        var result = await _regionDataService.ImportDataFromGeoJson(regionName, regionLocation, description, regionJson);
        return result;
    }

    /// <summary>
    ///     获得指定区域的信息
    /// </summary>
    /// <param name="regionId"></param>
    /// <returns></returns>
    [HttpGet("single")]
    public async Task<RegionEntity> GetSingleRegionData(int regionId)
    {
        var result = await _regionDataService.GetData(regionId);
        return result;
    }
}