using CQS_CoreService.Application;
using StackExchange.Profiling.Internal;
using Xunit.Abstractions;

namespace CQS_CoreService.Test;

public class SoilDataServiceTest
{
    private readonly ISoilDataService _soilDataService;
    private readonly ITestOutputHelper _testOutputHelper;

    public SoilDataServiceTest(ISoilDataService soilDataService, ITestOutputHelper testOutputHelper)
    {
        _soilDataService = soilDataService;
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(2)]
    public async Task GetSoilDataFromUserGroup(int userGroupId)
    {
        var result = await _soilDataService.GetSoilDataFromUserGroup(userGroupId);
        _testOutputHelper.WriteLine(result.ToJson());
        Assert.NotNull(result);
    }
}