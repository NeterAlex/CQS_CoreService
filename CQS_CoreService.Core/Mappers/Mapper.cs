using System.Linq;
using CQS_CoreService.Core.Dto;
using CQS_CoreService.Core.Entity;
using CQS_CoreService.Core.Utils;
using Mapster;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SoilDataEntity, SoilDataSimpleDto>()
            .Map(d => d.RawJson, s => s.RawJson != null)
            .Map(d => d.ExtraJson, s => s.ExtraJson != null && s.ExtraJson != "{}")
            .Map(d => d.GeoJson, s => s.GeoJson != null)
            .Map(d => d.RegionJson, s => s.RegionJson != null);

        config.ForType<UserGroupEntity, UserGroupDto>()
            .Map(d => d.Admin, s => s.Admin.Select(i => i.Id).ToList());

        config.ForType<UserEntity, UserDesensitizedDto>()
            .Map(d => d.Phone, s => Desensitizer.GetSafePhone(s.Phone))
            .Map(d => d.IdNumber, s => Desensitizer.GetSafeIdNumber(s.IdNumber));

        config.ForType<SoilDataEntity, SoilDataDetailDto>()
            .Map(d => d.RawJson, s => s.RawJson)
            .Map(d => d.ExtraJson, s => s.ExtraJson)
            .Map(d => d.GeoJson, s => s.GeoJson)
            .Map(d => d.RegionJson, s => s.RegionJson);
    }
}