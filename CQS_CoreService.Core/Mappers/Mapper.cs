using System.Linq;
using CQS_CoreService.Core.Dto;
using CQS_CoreService.Core.Entity;
using Mapster;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SoilDataEntity, SoilDataSimpleDto>()
            .Map(d => d.RawJson, s => s.RawJson != null)
            .Map(d => d.ExtraJson, s => s.ExtraJson != null)
            .Map(d => d.GeoJson, s => s.GeoJson != null)
            .Map(d => d.RegionJson, s => s.RegionJson != null);
        config.ForType<UserGroupEntity, UserGroupDto>()
            .Map(d => d.Admin, s => s.Admin.Select(i => i.Id).ToList());
    }
}