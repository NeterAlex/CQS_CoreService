using CQS_CoreService.Core.Entity;
using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IInitService
{
}

public class InitService : IInitService
{
    private readonly ISqlSugarClient _db;

    public InitService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async void InitDatabaseType()
    {
        try
        {
            var baseRole = new UserRoleEntity
            {
                Name = "user",
                Description = "普通用户"
            };
            var baseRoleAdmin = new UserRoleEntity
            {
                Name = "dev-admin",
                Description = "开发账户"
            };
            var baseUserGroup = new UserGroupEntity
            {
                Name = "default",
                Description = "默认分组"
            };
            await _db.Insertable(baseRole).ExecuteCommandAsync();
            await _db.Insertable(baseRoleAdmin).ExecuteCommandAsync();
            await _db.Insertable(baseUserGroup).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}