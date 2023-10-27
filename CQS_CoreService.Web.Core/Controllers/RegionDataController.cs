using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BAMCIS.GeoJSON;
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
        return await _regionDataService.GetBasicData(regionId);
    }

    /// <summary>
    ///     获得指定区域的GeoJson
    /// </summary>
    /// <param name="regionId"></param>
    /// <returns></returns>
    [HttpGet("single/geojson")]
    public async Task<FeatureCollection> GetSingleRegionGeoJson(int regionId)
    {
        return await _regionDataService.GetData(regionId);
    }

    /// <summary>
    ///     获取所有区域信息
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<RegionEntity>> GetListRegionData([FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 1)
    {
        return await _regionDataService.GetBasicDataList(pageSize, pageIndex);
    }
}