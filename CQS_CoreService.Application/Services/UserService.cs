using CQS_CoreService.Core.Entity;
using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IUserService
{
}

public class UserService : IUserService, ITransient
{
    private readonly ISqlSugarClient _db;

    public UserService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    ///     更新指定用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<UserEntity> UpdateUserInfo(int userId, UserEntity user)
    {
        try
        {
            await _db.Updateable(user).IgnoreColumns(true).ExecuteCommandAsync();
            return await _db.Queryable<UserEntity>().Where(i => i.Id == userId).SingleAsync();
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     删除指定用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteUser(int userId)
    {
        try
        {
            var user = await _db.Queryable<UserEntity>().Where(i => i.Id == userId).SingleAsync();
            if (user is null) throw new Exception("用户不存在");
            return await _db.Deleteable(user).ExecuteCommandHasChangeAsync();
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     获取指定用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<UserEntity> GetUser(int userId)
    {
        try
        {
            var user = await _db.Queryable<UserEntity>().Where(i => i.Id == userId).SingleAsync();
            if (user is null) throw new Exception("用户不存在");
            return user;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     分页查询所有用户
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public async Task<List<UserEntity>> GetUserList(int pageSize, int pageIndex)
    {
        try
        {
            RefAsync<int> totalCount = 0;
            var userList = await _db.Queryable<UserEntity>().ToPageListAsync(pageIndex, pageSize, totalCount);
            return userList;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     分页查询指定角色下的所有用户
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="pagesize"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    public async Task<List<UserEntity>> Getuserlistbyrole(int roleId, int pagesize, int pageindex)
    {
        try
        {
            RefAsync<int> totalCount = 0;
            var role = await _db.Queryable<UserRoleEntity>().Where(i => i.Id == roleId).SingleAsync();
            var userList = await _db.Queryable<UserEntity>()
                .Includes(i => i.Roles)
                .Where(i => i.Roles.Contains(role))
                .ToPageListAsync(pageindex, pagesize, totalCount);
            return userList;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    public async Task<List<UserEntity>> Getuserlistbygroup(int groupId, int pagesize, int pageindex)
    {
        try
        {
            RefAsync<int> totalCount = 0;
            var group = await _db.Queryable<UserGroupEntity>().Where(i => i.Id == groupId).SingleAsync();
            var userList = await _db.Queryable<UserEntity>()
                .Includes(i => i.UserGroups)
                .Where(i => i.UserGroups.Contains(group))
                .ToPageListAsync(pageindex, pagesize, totalCount);
            return userList;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}