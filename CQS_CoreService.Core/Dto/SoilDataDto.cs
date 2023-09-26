using System;
using Newtonsoft.Json.Linq;

namespace CQS_CoreService.Core.Dto;

public class SoilDataDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public JObject ExtraJson { get; set; }
    public JObject GeoJson { get; set; }
    public JObject RegionJson { get; set; }
    public JObject RawJson { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int OwnerId { get; set; }
}

public class SoilDataSimpleDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public bool ExtraJson { get; set; }
    public bool GeoJson { get; set; }
    public bool RegionJson { get; set; }
    public bool RawJson { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int OwnerId { get; set; }
}