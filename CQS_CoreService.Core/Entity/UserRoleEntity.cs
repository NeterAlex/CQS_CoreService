using System;
using SqlSugar;

namespace CQS_CoreService.Core.Entity;

[SugarTable("User_Role")]
public class UserRoleEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(InsertServerTime = true)] public DateTime CreatedTime { get; set; }

    [SugarColumn(UpdateServerTime = true)] public DateTime UpdatedTime { get; set; }

    [SugarColumn(IsNullable = true)] public string Description { get; set; }
    public string Name { get; set; }
}