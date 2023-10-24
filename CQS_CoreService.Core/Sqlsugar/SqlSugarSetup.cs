using System.Reflection;
using CQS_CoreService.Core.Entity;
using CQS_CoreService.Core.Entity.Relations;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace CQS_CoreService.Core.Sqlsugar;

public static class SqlSugarSetup
{
    public static void AddSqlsugarSetup(this IServiceCollection services)
    {
        var configConnection = new ConnectionConfig
        {
            ConnectionString = "PORT=5432;DATABASE=cqs_db;HOST=localhost;PASSWORD=1q2w3e$r5tGh;USER ID=postgres",
            DbType = DbType.PostgreSQL,
            IsAutoCloseConnection = true,
            LanguageType = LanguageType.Chinese,
            ConfigureExternalServices = new ConfigureExternalServices
            {
                EntityNameService = (type, entity) => { entity.IsDisabledDelete = true; },
                EntityService = (c, p) =>
                {
                    if (p.IsPrimarykey == false &&
                        new NullabilityInfoContext().Create(c).WriteState is NullabilityState.Nullable)
                        p.IsNullable = true;
                }
            }
        };

        var sqlSugar = new SqlSugarScope(configConnection,
            db =>
            {
                // 单例参数配置，所有上下文生效
                db.Aop.OnLogExecuting = (sql, pars) => { };
            });

        services.AddSingleton<ISqlSugarClient>(sqlSugar);

        CreateDb(sqlSugar);
    }

    private static void CreateDb(ISqlSugarClient db)
    {
        db.DbMaintenance.CreateDatabase();
        db.CodeFirst.InitTables(typeof(UserEntity));
        db.CodeFirst.InitTables(typeof(UserGroupEntity));
        db.CodeFirst.InitTables(typeof(UserRoleEntity));
        db.CodeFirst.InitTables(typeof(SoilDataEntity));
        db.CodeFirst.InitTables(typeof(UserRoleRelation));
        db.CodeFirst.InitTables(typeof(UserGroupRelation));
        db.CodeFirst.InitTables(typeof(RegionDataEntity));
    }
}