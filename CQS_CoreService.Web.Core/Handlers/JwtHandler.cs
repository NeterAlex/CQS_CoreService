﻿using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CQS_CoreService.Web.Core;

public class JwtHandler : AppAuthorizeHandler
{
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 授权判断逻辑，授权通过返回 true，否则返回 false

        return Task.FromResult(true);
    }
}
