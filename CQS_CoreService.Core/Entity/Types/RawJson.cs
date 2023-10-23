using Newtonsoft.Json;

namespace CQS_CoreService.Core.Entity.Types;

public class RawJson
{
    public int ID { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    [JsonProperty("有机质g/kg")] public double 有机质g_kg { get; set; }

    [JsonProperty("碱性氮mg/kg")] public double 碱性氮mg_kg { get; set; }

    [JsonProperty("有效磷mg/kg")] public double 有效磷mg_kg { get; set; }

    [JsonProperty("速效钾mg/kg")] public double 速效钾mg_kg { get; set; }

    public double PH值 { get; set; }
    public int 土层厚度cm { get; set; }

    [JsonProperty("容重g/cm3")] public double 容重g_cm3 { get; set; }

    public string 耕层质地 { get; set; }
    public string 表面障碍程度 { get; set; }
    public string 坡度 { get; set; }
    public string 排水条件 { get; set; }
    public string 灌溉能力 { get; set; }
    public string 清洁度 { get; set; }
    public double result_all { get; set; }
}