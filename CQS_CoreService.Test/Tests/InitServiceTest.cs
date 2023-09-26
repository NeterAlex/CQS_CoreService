using CQS_CoreService.Application;
using CQS_CoreService.Core.Entity;
using SqlSugar;
using Xunit.Abstractions;

namespace CQS_CoreService.Test;

public class InitServiceTest
{
    private readonly ISqlSugarClient _db;
    private readonly IInitService _initService;
    private readonly ITestOutputHelper _testOutputHelper;

    public InitServiceTest(IInitService initService, ITestOutputHelper testOutputHelper, ISqlSugarClient db)
    {
        _initService = initService;
        _testOutputHelper = testOutputHelper;
        _db = db;
    }

    [Theory]
    [InlineData(true)]
    public async void CheckDataInit(bool isExec)
    {
        if (isExec) await _initService.InitDatabaseData();
        Assert.True(true);
    }

    [Fact]
    public async void CheckBaseRoleAndGroup()
    {
        var baseRole = await _db.Queryable<UserRoleEntity>().Where(i => i.Name == "user").FirstAsync();
        var baseRoleAdmin = await _db.Queryable<UserRoleEntity>().Where(i => i.Name == "dev-admin").FirstAsync();
        var baseUserGroup = await _db.Queryable<UserGroupEntity>().Where(i => i.Name == "default").FirstAsync();
        Assert.NotNull(baseRole);
        Assert.NotNull(baseRoleAdmin);
        Assert.NotNull(baseUserGroup);
    }
}