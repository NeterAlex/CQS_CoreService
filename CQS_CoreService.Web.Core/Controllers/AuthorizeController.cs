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

    [HttpPost]
    public async Task<UserEntity> Register([FromForm] string username, [FromForm] string password)
    {
        var newUser = await _authorizeService.RegisterNormalUser(username, password);
        return newUser;
    }

    [HttpPost]
    public async Task<string> Login([FromForm] string username, [FromForm] string password,
        [FromForm] bool usePhone = false)
    {
        return await _authorizeService.Login(username, password);
    }
}