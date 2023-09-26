using Furion.RemoteRequest.Extensions;

namespace CQS_CoreService.Test;

public class NetworkTest
{
    [Fact]
    public async Task CnNetworkConnection()
    {
        var resp = await "https://www.baidu.com".GetAsync();
        Assert.True(resp.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GlobalNetworkConnection()
    {
        var resp = await "https://www.google.com".GetAsync();
        Assert.True(resp.IsSuccessStatusCode);
    }
}