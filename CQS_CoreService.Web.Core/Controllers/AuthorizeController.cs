using System;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CQS_CoreService.Web.Core.Controllers;

[Microsoft.AspNetCore.Components.Route("api/auth")]
[ApiDescriptionSettings(Tag = "授权与鉴权 Authorization & Authentication API")]
[DynamicApiController]
public class AuthorizeController
{
    private readonly IAuthorizeService _authorizeService;

    public AuthorizeController(IAuthorizeService authorizeService)
    {
        _authorizeService = authorizeService;
    }

    /// <summary>
    ///     注册账号
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserEntity> Register([FromForm] string username, [FromForm] string password)
    {
        var newUser = await _authorizeService.RegisterNormalUser(username, password);
        return newUser;
    }

    /// <summary>
    ///     登录并签发Token
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="usePhone"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> Login([FromForm] string username, [FromForm] string password,
        [FromForm] bool usePhone = false)
    {
        return await _authorizeService.Login(username, password);
    }

    /// <summary>
    ///     [仅开发用]快速获得Token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> FastJWT()
    {
        return await _authorizeService.Login("neteralex", "1q2w3e$r5tGh");
    }
}