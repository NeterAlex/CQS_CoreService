using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Dto;
using CQS_CoreService.Core.Vo;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQS_CoreService.Web.Core.Controllers;

[Microsoft.AspNetCore.Components.Route("api/user")]
[ApiDescriptionSettings(Tag = "用户与用户组 User & UserGroup API")]
public class UserController : IDynamicApiController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    ///     获取指定用户数据（脱敏）
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("single")]
    [Authorize]
    public async Task<UserDesensitizedDto> GetSingleUser(int userId)
    {
        var user = await _userService.GetUser(userId);
        return user.Adapt<UserDesensitizedDto>();
    }

    /// <summary>
    ///     删除指定用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete("single")]
    [Authorize]
    public async Task<bool> DeleteSingleUser(int userId)
    {
        return await _userService.DeleteUser(userId);
    }

    /// <summary>
    ///     获取所有用户列表（分页、脱敏）
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("list")]
    [Authorize]
    public async Task<List<UserDesensitizedDto>> GetUserList([FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 1)
    {
        var list = await _userService.GetUserList(pageSize, pageIndex);
        return list.Select(i => i.Adapt<UserDesensitizedDto>()).ToList();
    }

    /// <summary>
    ///     获取指定用户组下的所有用户（分页、脱敏）
    /// </summary>
    /// <param name="userGroupId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("list/from-group")]
    [Authorize]
    public async Task<List<UserDesensitizedDto>> GetUserListFromUserGroup([FromQuery] int userGroupId, [FromQuery] int pageSize = 50,
        [FromQuery] int pageIndex = 1)
    {
        var list = await _userService.GetUserListByGroup(userGroupId, pageSize, pageIndex);
        return list.Select(i => i.Adapt<UserDesensitizedDto>()).ToList();
    }

    /// <summary>
    ///     更新指定用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userUpdateVo"></param>
    /// <returns></returns>
    [HttpPut("single")]
    [Authorize]
    public async Task<UserDesensitizedDto> UpdateUser([FromQuery] int userId, [FromQuery] UserUpdateVo userUpdateVo)
    {
        var user = await _userService.GetUser(userId);
        user.Address = userUpdateVo.Address;
        user.Email = userUpdateVo.Email;
        user.Fullname = userUpdateVo.Fullname;
        user.State = userUpdateVo.State;
        user.IdNumber = userUpdateVo.IdNumber;
        var result = await _userService.UpdateUserInfo(user.Id, user);
        return result.Adapt<UserDesensitizedDto>();
    }
}