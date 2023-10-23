using CQS_CoreService.Core.Sqlsugar;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Project.UnifyResult;

namespace CQS_CoreService.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter();
        services.AddRemoteRequest();
        services.AddJwt<JwtHandler>();
        services.AddCorsAccessor();
        services.AddControllers()
            .AddFriendlyException()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.Converters.AddLongTypeConverters();
                options.SerializerSettings.Converters.AddClayConverters();
            })
            .AddInjectWithUnifyResult<RESTfulResultProvider>();
        services.AddSqlsugarSetup();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCorsAccessor();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseInject(string.Empty);
        app.UseUnifyResultStatusCodes();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}