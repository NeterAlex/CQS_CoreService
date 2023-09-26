using Furion;
using System.Reflection;

namespace CQS_CoreService.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "CQS_CoreService.Application",
            "CQS_CoreService.Core",
            "CQS_CoreService.Web.Core"
        };
    }
}