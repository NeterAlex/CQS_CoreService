using System;
using SqlSugar;

namespace CQS_CoreService.Core.Entity;

[SugarTable("Soil_Data")]
public class SoilDataEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(InsertServerTime = true)] public DateTime CreatedTime { get; set; }

    [SugarColumn(UpdateServerTime = true)] public DateTime UpdatedTime { get; set; }

    [SugarColumn(IsJson = true)] public string ExtraJson { get; set; }

    [SugarColumn(IsJson = true)] public string GeoJson { get; set; }

    [SugarColumn(IsJson = true)] public string RegionJson { get; set; }

    [SugarColumn(IsJson = true)] public string RawJson { get; set; }

    public string Type { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    // 关系
    public int OwnerId { get; set; }

    [Navigate(NavigateType.OneToOne, nameof(OwnerId))]
    public UserEntity Owner { get; set; }
}