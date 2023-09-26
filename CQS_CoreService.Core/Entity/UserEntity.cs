using System;
using System.Collections.Generic;
using CQS_CoreService.Core.Entity.Relations;
using SqlSugar;

namespace CQS_CoreService.Core.Entity;

[SugarIndex("unique_User_Username", nameof(Username), OrderByType.Desc, true)]
[SugarIndex("unique_User_Phone", nameof(Phone), OrderByType.Desc, true)]
[SugarTable("User")]
public class UserEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(InsertServerTime = true)] public DateTime CreatedTime { get; set; }

    [SugarColumn(UpdateServerTime = true)] public DateTime UpdatedTime { get; set; }

    [SugarColumn(IsNullable = true)] public string Address { get; set; }
    [SugarColumn(IsNullable = true)] public string Email { get; set; }
    [SugarColumn(IsNullable = true)] public string Fullname { get; set; }
    [SugarColumn(IsNullable = true)] public string Phone { get; set; }
    public string State { get; set; }
    [SugarColumn(IsNullable = true)] public string IdNumber { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    [Navigate(typeof(UserRoleRelation), nameof(UserRoleRelation.UserId), nameof(UserRoleRelation.UserRoleId))]
    public List<UserRoleEntity> Roles { get; set; }

    [Navigate(typeof(UserGroupRelation), nameof(UserGroupRelation.UserId), nameof(UserGroupRelation.UserGroupId))]
    public List<UserGroupEntity> UserGroups { get; set; }
}