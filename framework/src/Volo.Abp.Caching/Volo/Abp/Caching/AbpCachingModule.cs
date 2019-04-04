﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Serialization;
using Volo.Abp.Threading;

namespace Volo.Abp.Caching
{
    [DependsOn(
        typeof(AbpThreadingModule),
        typeof(AbpSerializationModule),
        typeof(AbpMultiTenancyModule),
        typeof(AbpJsonModule))]
    public class AbpCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMemoryCache();
            context.Services.AddDistributedMemoryCache();

            context.Services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));

            context.Services.Configure<CacheOptions>(cacheOptions =>
            {
                cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
            });
        }
    }
}
