using System;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Dto;
using CQS_CoreService.Core.Entity;
using CQS_CoreService.Core.Vo;
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
    /// <param name="vo"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserEntity> Register(AuthorizeRegisterVo vo)
    {
        var newUser = await _authorizeService.RegisterNormalUser(vo.Username, vo.Password);
        return newUser;
    }

    /// <summary>
    ///     登录并签发Token
    /// </summary>
    /// <param name="vo"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AuthorizeDto> Login(AuthorizeLoginVo vo)
    {
        return new AuthorizeDto
        {
            AccessToken = await _authorizeService.Login(vo.Username, vo.Password)
        };
    }

    /// <summary>
    ///     [仅开发用]快速获得Token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<AuthorizeDto> FastJwt()
    {
        return new AuthorizeDto
        {
            AccessToken = await _authorizeService.Login("neteralex", "1q2w3e$r5tGh")
        };
    }
}