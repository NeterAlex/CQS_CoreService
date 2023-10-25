using SqlSugar;

namespace CQS_CoreService.Core.Entity.Relations;

[SugarTable("Region_RegionData")]
public class RegionDataRelation
{
    [SugarColumn(IsPrimaryKey = true)] public int RegionId { get; set; }

    [SugarColumn(IsPrimaryKey = true)] public int RegionDataId { get; set; }
}