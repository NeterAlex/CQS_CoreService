using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQS_CoreService.Web.Core.Controllers;

[Microsoft.AspNetCore.Components.Route("api/soildata")]
[ApiDescriptionSettings(Tag = "土地质量数据 SoilData API")]
[DynamicApiController]
public class SoilDataController
{
    private readonly ISoilDataService _soilDataService;

    public SoilDataController(ISoilDataService soilDataService)
    {
        _soilDataService = soilDataService;
    }

    /// <summary>
    ///     根据数据源文件与区域文件计算耕地质量数据
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="location"></param>
    /// <param name="description"></param>
    /// <param name="rawFile"></param>
    /// <param name="regionFile"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("calc")]
    [Authorize]
    public async Task<SoilDataSimpleDto> Calc([FromForm] int userId, [FromForm] string location,
        [FromForm] string description,
        IFormFile rawFile,
        IFormFile regionFile)
    {
        var resultSoilData = await _soilDataService.CreateSoilData(userId, location, description, rawFile, regionFile);
        if (resultSoilData is null) throw new Exception("数据计算失败");
        var result = resultSoilData.Adapt<SoilDataSimpleDto>();
        return result;
    }

    /// <summary>
    ///     获取指定耕地质量数据
    /// </summary>
    /// <param name="dataId"></param>
    /// <returns></returns>
    [HttpGet("single")]
    [Authorize]
    public async Task<SoilDataDetailDto> GetSingleSoilData(int dataId)
    {
        var result = await _soilDataService.GetSoilData(dataId);
        var resp = result.Adapt<SoilDataDetailDto>();
        return resp;
    }

    /// <summary>
    ///     获取指定用户的所有耕地质量数据
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("list/from-user")]
    [Authorize]
    public async Task<List<SoilDataSimpleDto>> GetListFromUser(int userId)
    {
        var list = await _soilDataService.GetSoilDataListFromUser(userId);
        return list.Select(i => i.Adapt<SoilDataSimpleDto>()).ToList();
    }

    /// <summary>
    ///     获取指定用户组的所有耕地质量数据
    /// </summary>
    /// <param name="userGroupId"></param>
    /// <returns></returns>
    [HttpGet("list/from-group")]
    [Authorize]
    public async Task<List<SoilDataSimpleDto>> GetListFromUserGroup(int userGroupId)
    {
        var list = await _soilDataService.GetSoilDataFromUserGroup(userGroupId);
        return list.Select(i => i.Adapt<SoilDataSimpleDto>()).ToList();
    }
}