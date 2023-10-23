namespace CQS_CoreService.Core.Entity.Types;

public class GeoJson
{
    public string Type { get; set; }
    public Feature[] Features { get; set; }
}

public class Feature
{
    public string Type { get; set; }
    public Geometry Geometry { get; set; }
    public Properties Properties { get; set; }
}

public class Geometry
{
    public string Type { get; set; }
    public double[] Coordinates { get; set; }
}

public class Properties
{
    public int Id { get; set; }
    public double Predic { get; set; }
    public int Tier { get; set; }
}