using System;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Dto;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQS_CoreService.Web.Core.Controllers;

[DynamicApiController]
public class SoilDataController
{
    private readonly ISoilDataService _soilDataService;

    public SoilDataController(ISoilDataService soilDataService)
    {
        _soilDataService = soilDataService;
    }

    [HttpPost]
    public async Task<SoilDataSimpleDto> CreateSoilData([FromForm] int userId, [FromForm] string location,
        [FromForm] string description,
        IFormFile rawFile,
        IFormFile regionFile)
    {
        var resultSoilData = await _soilDataService.CreateSoilData(userId, location, description, rawFile, regionFile);
        if (resultSoilData is null) throw new Exception("数据计算失败");
        var result = resultSoilData.Adapt<SoilDataSimpleDto>();
        return result;
    }
}