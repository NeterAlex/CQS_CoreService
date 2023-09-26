using SqlSugar;

namespace CQS_CoreService.Core.Entity.Relations;

[SugarTable("User_UserGroup")]
public class UserGroupRelation
{
    [SugarColumn(IsPrimaryKey = true)] public int UserId { get; set; }

    [SugarColumn(IsPrimaryKey = true)] public int UserGroupId { get; set; }
}