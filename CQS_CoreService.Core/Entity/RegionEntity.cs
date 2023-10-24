﻿using System;
using System.Collections.Generic;
using SqlSugar;

namespace CQS_CoreService.Core.Entity;

[SugarTable("Region")]
public class RegionEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(InsertServerTime = true)] public DateTime CreatedTime { get; set; }

    [SugarColumn(UpdateServerTime = true)] public DateTime UpdatedTime { get; set; }

    [SugarColumn(IsNullable = true, DefaultValue = "待导入")]
    public string Location { get; set; }

    [SugarColumn(DefaultValue = "未命名区域")] public string Name { get; set; }

    [Navigate(NavigateType.OneToMany, nameof(RegionDataEntity.Id))]
    public List<RegionDataEntity> Features { get; set; }
}