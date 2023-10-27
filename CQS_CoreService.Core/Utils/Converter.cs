using System.Collections.Generic;
using System.Linq;
using BAMCIS.GeoJSON;
using CQS_CoreService.Core.Entity;

namespace CQS_CoreService.Core.Utils;

public class Converter
{
    public static FeatureCollection FeaturesToCollection(List<RegionDataEntity> list)
    {
        var featureList = list.Select(regionData => Feature.FromJson(regionData.GeoJson)).ToList();
        var collection = new FeatureCollection(featureList);
        return collection;
    }
}