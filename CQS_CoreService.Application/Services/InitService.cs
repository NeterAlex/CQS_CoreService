using CQS_CoreService.Core.Entity;
using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IInitService
{
    public Task InitDatabaseData();
}

public class InitService : IInitService, ITransient
{
    private readonly ISqlSugarClient _db;

    public InitService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task InitDatabaseData()
    {
        try
        {
            var baseRoleT = await _db.Queryable<UserRoleEntity>().Where(i => i.Name == "user").FirstAsync();
            var baseRoleAdminT = await _db.Queryable<UserRoleEntity>().Where(i => i.Name == "dev-admin").FirstAsync();
            var baseUserGroupT = await _db.Queryable<UserGroupEntity>().Where(i => i.Name == "default").FirstAsync();
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
            if (baseRoleT == null) await _db.Storageable(baseRole).ExecuteCommandAsync();
            if (baseRoleAdminT == null) await _db.Storageable(baseRoleAdmin).ExecuteCommandAsync();
            if (baseUserGroupT == null) await _db.Storageable(baseUserGroup).ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}