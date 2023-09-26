using System;
using System.Collections.Generic;
using CQS_CoreService.Core.Entity.Relations;
using SqlSugar;

namespace CQS_CoreService.Core.Entity;

[SugarTable("User_Group")]
public class UserGroupEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(InsertServerTime = true)] public DateTime CreatedTime { get; set; }

    [SugarColumn(UpdateServerTime = true)] public DateTime UpdatedTime { get; set; }

    [SugarColumn(IsNullable = true)] public string Description { get; set; }

    public string Name { get; set; }

    // 关系
    [SugarColumn(IsNullable = true)] public List<UserEntity> Admin { get; set; }

    [Navigate(typeof(UserGroupRelation), nameof(UserGroupRelation.UserGroupId), nameof(UserGroupRelation.UserId))]
    public List<UserEntity> Users { get; set; }
}