using CQS_CoreService.Core.Sqlsugar;
using Furion.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

[assembly: TestFramework("CQS_CoreService.Test.TestProgram", "CQS_CoreService.Test")]

namespace CQS_CoreService.Test;

public class TestProgram : TestStartup
{
    public TestProgram(IMessageSink messageSink) : base(messageSink)
    {
        Serve.RunNative(services =>
        {
            services.AddRemoteRequest();
            services.AddSqlsugarSetup();
        });
    }
}