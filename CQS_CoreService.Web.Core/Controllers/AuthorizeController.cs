using System;
using System.Threading.Tasks;
using CQS_CoreService.Application;
using CQS_CoreService.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CQS_CoreService.Web.Core.Controllers;

[DynamicApiController]
public class AuthorizeController
{
    private readonly IAuthorizeService _authorizeService;

    public AuthorizeController(IAuthorizeService authorizeService)
    {
        _authorizeService = authorizeService;
    }

    [HttpPost]
    public async Task<UserEntity> RegisterNormalUser([FromForm] string username, [FromForm] string password)
    {
        var newUser = await _authorizeService.RegisterNormalUser(username, password);
        return newUser;
    }
}