using CQS_CoreService.Core.Entity;
using Furion.Logging.Extensions;

namespace CQS_CoreService.Application;

public interface IUserService
{
    public Task<UserEntity> GetUser(int userId);
    public Task<bool> DeleteUser(int userId);
    public Task<UserEntity> UpdateUserInfo(int userId, UserEntity user);
    public Task<List<UserEntity>> GetUserList(int pageSize, int pageIndex);
    public Task<List<UserEntity>> GetUserListByRole(int roleId, int pageSize, int pageindex);
    public Task<List<UserEntity>> GetUserListByGroup(int roleId, int pageSize, int pageindex);
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
            var user = await _db.Queryable<UserEntity>()
                .Includes(i => i.Roles)
                .Includes(i => i.UserGroups)
                .Where(i => i.Id == userId).SingleAsync();
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
    public async Task<List<UserEntity>> GetUserList(int pageSize = 50, int pageIndex = 1)
    {
        try
        {
            RefAsync<int> totalCount = 0;
            var userList = await _db.Queryable<UserEntity>()
                .Includes(i => i.UserGroups)
                .Includes(i => i.Roles)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
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
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public async Task<List<UserEntity>> GetUserListByRole(int roleId, int pageSize = 50, int pageIndex = 1)
    {
        try
        {
            RefAsync<int> totalCount = 0;
            var role = await _db.Queryable<UserRoleEntity>().Where(i => i.Id == roleId).SingleAsync();
            var userList = await _db.Queryable<UserEntity>()
                .Includes(i => i.Roles)
                .Where(i => i.Roles.Contains(role))
                .ToPageListAsync(pageIndex, pageSize, totalCount);
            return userList;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }

    /// <summary>
    ///     分页查询指定用户组下的所有用户
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public async Task<List<UserEntity>> GetUserListByGroup(int groupId, int pageSize = 50, int pageIndex = 1)
    {
        try
        {
            RefAsync<int> totalCount = 0;
            var group = await _db.Queryable<UserGroupEntity>().Where(i => i.Id == groupId).SingleAsync();
            var userList = await _db.Queryable<UserEntity>()
                .Includes(i => i.UserGroups)
                .Where(i => i.UserGroups.Contains(group))
                .ToPageListAsync(pageIndex, pageSize, totalCount);
            return userList;
        }
        catch (Exception e)
        {
            e.Message.LogError();
            throw;
        }
    }
}