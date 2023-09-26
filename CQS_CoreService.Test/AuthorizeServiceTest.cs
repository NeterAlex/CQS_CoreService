using CQS_CoreService.Application;
using Furion.DistributedIDGenerator;
using Xunit.Abstractions;

namespace CQS_CoreService.Test;

public class AuthorizeServiceTest
{
    private readonly IAuthorizeService _authorizeService;
    private readonly ITestOutputHelper _testOutputHelper;

    public AuthorizeServiceTest(IAuthorizeService authorizeService, ITestOutputHelper testOutputHelper)
    {
        _authorizeService = authorizeService;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async void RegisterNormalUser()
    {
        var username = ShortIDGen.NextID();
        var user = await _authorizeService.RegisterNormalUser(username, "123456");
        Assert.NotNull(user);
        Assert.Equal(user.Username, username);
    }

    [Theory]
    [InlineData("neteralex", "123456")]
    public async void Login(string username, string password)
    {
        var token = await _authorizeService.Login(username, password);
        _testOutputHelper.WriteLine(token);
        Assert.NotNull(token);
        Assert.IsType<string>(token);
    }
}