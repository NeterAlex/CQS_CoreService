using System;
using SqlSugar;

namespace CQS_CoreService.Core.Entity;

[SugarTable("Region_Data")]
public class RegionDataEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(InsertServerTime = true)] public DateTime CreatedTime { get; set; }

    [SugarColumn(UpdateServerTime = true)] public DateTime UpdatedTime { get; set; }

    [SugarColumn(ColumnDataType = "text")] public string ExtraJson { get; set; }

    [SugarColumn(ColumnDataType = "text")] public string GeoJson { get; set; }

    public string Type { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string FId { get; set; }
    public int OwnerId { get; set; }

    [Navigate(NavigateType.OneToOne, nameof(OwnerId))]
    public UserEntity Owner { get; set; }
}