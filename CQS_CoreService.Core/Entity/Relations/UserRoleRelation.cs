using SqlSugar;

namespace CQS_CoreService.Core.Entity.Relations;

[SugarTable("User_UserRole")]
public class UserRoleRelation
{
    [SugarColumn(IsPrimaryKey = true)] public int UserId { get; set; }

    [SugarColumn(IsPrimaryKey = true)] public int UserRoleId { get; set; }
}