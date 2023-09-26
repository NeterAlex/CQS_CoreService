using CQS_CoreService.Application;
using CQS_CoreService.Core.Entity;
using Furion.DistributedIDGenerator;
using SqlSugar;
using StackExchange.Profiling.Internal;
using Xunit.Abstractions;

namespace CQS_CoreService.Test;

public class UserServiceTest
{
    private readonly IAuthorizeService _authorizeService;
    private readonly ISqlSugarClient _db;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IUserService _userService;

    public UserServiceTest(IUserService userService, ITestOutputHelper testOutputHelper, ISqlSugarClient db,
        IAuthorizeService authorizeService)
    {
        _userService = userService;
        _testOutputHelper = testOutputHelper;
        _db = db;
        _authorizeService = authorizeService;
    }


    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public async void GetUser(int id)
    {
        var user = await _userService.GetUser(id);
        Assert.NotNull(user);
        _testOutputHelper.WriteLine(user.ToJson());
    }

    [Fact]
    public async void GetUserList()
    {
        var list = await _userService.GetUserList(50, 1);
        Assert.NotNull(list);
        _testOutputHelper.WriteLine(list.ToJson());
    }


    [Fact]
    public async void UserBasicCrud()
    {
        // Prepare
        var baseRole = await _db.Queryable<UserRoleEntity>().Where(i => i.Name == "user").FirstAsync();
        var baseGroup = await _db.Queryable<UserGroupEntity>().Where(i => i.Name == "default").FirstAsync();
        // C
        var newUser = await _authorizeService.RegisterNormalUser(ShortIDGen.NextID(), "123456789");
        var newUserId = newUser.Id;
        _testOutputHelper.WriteLine($"[C] {newUser.ToJson()}");
        // R
        var newUserCopy = await _userService.GetUser(newUserId);
        _testOutputHelper.WriteLine($"[R] {newUserCopy.ToJson()}");
        // U
        var newUserInfo = new UserEntity
        {
            Address = ShortIDGen.NextID(),
            Email = $"{ShortIDGen.NextID()}@test.com",
            Fullname = ShortIDGen.NextID(),
            Phone = $"13154{new Random().Next(100000, 999999)}",
            State = "NORMAL",
            IdNumber = $"230001{new Random().Next(1964, 2023)}0122{new Random().Next(1000, 9999)}",
            Roles = new List<UserRoleEntity> { baseRole },
            UserGroups = new List<UserGroupEntity> { baseGroup }
        };
        var newUserUpdated = await _userService.UpdateUserInfo(newUserId, newUserInfo);
        _testOutputHelper.WriteLine($"[U] {newUserUpdated.ToJson()}");
        // D
        await _userService.DeleteUser(newUserId);
        Assert.Null(await _db.Queryable<UserEntity>().Where(i => i.Id == newUserId).SingleAsync());
    }
}