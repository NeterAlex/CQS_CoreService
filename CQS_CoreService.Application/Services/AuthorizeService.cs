using System.Security.Authentication;
using CQS_CoreService.Core.Entity;
using Furion.DataEncryption.Extensions;
using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IAuthorizeService
{
    public Task<UserEntity> RegisterNormalUser(string username, string password);
    public Task<string> Login(string identity, string password);
}

public class AuthorizeService : IAuthorizeService, ITransient
{
    private readonly ISqlSugarClient _db;

    public AuthorizeService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<UserEntity> RegisterNormalUser(string username, string password)
    {
        try
        {
            var normalRole = await _db.Queryable<UserRoleEntity>()
                .Where(i => i.Name == "user")
                .SingleAsync();

            var defaultGroup = await _db.Queryable<UserGroupEntity>()
                .Where(i => i.Name == "default")
                .SingleAsync();

            var newUser = new UserEntity
            {
                Username = username,
                Password = password.ToMD5Encrypt(),
                Roles = new List<UserRoleEntity> { normalRole },
                UserGroups = new List<UserGroupEntity> { defaultGroup },
                State = "NORMAL"
            };

            return await _db.Insertable(newUser).ExecuteReturnEntityAsync();
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    public async Task<string> Login(string identity, string password)
    {
        try
        {
            var loginUser = await _db.Queryable<UserEntity>()
                .Where(i => i.Username == identity)
                .SingleAsync();
            if (loginUser is null) throw new AuthenticationException("登录凭据无效");
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
            {
                { "UserId", loginUser.Id }
            });
            return accessToken;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}