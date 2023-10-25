using System;
using System.Threading.Tasks;
using CQS_CoreService.Application;
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

    [HttpPost("create")]
    public async Task<bool> CreateRegion([FromForm] string regionName, [FromForm] string regionLocation, [FromForm] string description,
        IFormFile regionJson)
    {
        var result = await _regionDataService.ImportDataFromGeoJson(regionName, regionLocation, description, regionJson);
        return result;
    }
}