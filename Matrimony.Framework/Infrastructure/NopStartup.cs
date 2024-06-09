using Matrimony.Core;
using Matrimony.Framework;
using Matrimony.Framework.Authenticate;
using Matrimony.Service;
using Matrimony.Service.Account;
using Matrimony.Service.Achivements;
using Matrimony.Service.Addresss;
using Matrimony.Service.Cities;
using Matrimony.Service.Common;
using Matrimony.Service.Countries;
using Matrimony.Service.Educations;
using Matrimony.Service.Families;
using Matrimony.Service.Files;
using Matrimony.Service.Occupations;
using Matrimony.Service.ProfileFiles;
using Matrimony.Service.Profiles;
using Matrimony.Service.States;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Web.Framework.Infrastructure;

/// <summary>
/// Represents the registering services on application startup
/// </summary>
public partial class NopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //add and configure web api feature
        services.AddAppWebAPI();

        //work context
        services.AddScoped<IWorkContext, WebAPIWorkContext>();

        services.AddScoped<IAuthenticateService, AspNetCoreIdentityAuthenticate>();

        //file provider
        services.AddScoped<INopFileProvider, NopFileProvider>();

        //Servies
        services.AddScoped<IPrimaryService, PrimaryService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IStateService, StateService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IFamilyService, FamilyService>();
        services.AddScoped<IOccupationService, OccupationService>();
        services.AddScoped<IAchivementService, AchivementService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IProfileService, ProfileService>();

        //Files
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IProfileFileService, ProfileFileService>();

        //web helper
        //services.AddScoped<IWebHelper, WebHelper>();

        //user agent helper
        //services.AddScoped<IUserAgentHelper, UserAgentHelper>();

        //plugins
        //services.AddScoped<IPluginService, PluginService>();
        //services.AddScoped<OfficialFeedManager>();

        //static cache manager
        //var appSettings = Singleton<AppSettings>.Instance;
        //var distributedCacheConfig = appSettings.Get<DistributedCacheConfig>();

        //services.AddTransient(typeof(IConcurrentCollection<>), typeof(ConcurrentTrie<>));

        //services.AddSingleton<ICacheKeyManager, CacheKeyManager>();
        //services.AddScoped<IShortTermCacheManager, PerRequestCacheManager>();

        //if (distributedCacheConfig.Enabled)
        //{
        //    switch (distributedCacheConfig.DistributedCacheType)
        //    {
        //        case DistributedCacheType.Memory:
        //            services.AddScoped<IStaticCacheManager, MemoryDistributedCacheManager>();
        //            services.AddScoped<ICacheKeyService, MemoryDistributedCacheManager>();
        //            break;
        //        case DistributedCacheType.SqlServer:
        //            services.AddScoped<IStaticCacheManager, MsSqlServerCacheManager>();
        //            services.AddScoped<ICacheKeyService, MsSqlServerCacheManager>();
        //            break;
        //        case DistributedCacheType.Redis:
        //            services.AddSingleton<IRedisConnectionWrapper, RedisConnectionWrapper>();
        //            services.AddScoped<IStaticCacheManager, RedisCacheManager>();
        //            services.AddScoped<ICacheKeyService, RedisCacheManager>();
        //            break;
        //        case DistributedCacheType.RedisSynchronizedMemory:
        //            services.AddSingleton<IRedisConnectionWrapper, RedisConnectionWrapper>();
        //            services.AddSingleton<ISynchronizedMemoryCache, RedisSynchronizedMemoryCache>();
        //            services.AddSingleton<IStaticCacheManager, SynchronizedMemoryCacheManager>();
        //            services.AddScoped<ICacheKeyService, SynchronizedMemoryCacheManager>();
        //            break;
        //    }

        //    services.AddSingleton<ILocker, DistributedCacheLocker>();
        //}
        //else
        //{
        //    services.AddSingleton<ILocker, MemoryCacheLocker>();
        //    services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
        //    services.AddScoped<ICacheKeyService, MemoryCacheManager>();
        //}



        //store context
        //services.AddScoped<IStoreContext, WebStoreContext>();

        ////services
        //services.AddScoped<IBackInStockSubscriptionService, BackInStockSubscriptionService>();


        ////attribute services
        //services.AddScoped(typeof(IAttributeService<,>), typeof(AttributeService<,>));

        ////attribute parsers
        //services.AddScoped(typeof(IAttributeParser<,>), typeof(Services.Attributes.AttributeParser<,>));

        ////attribute formatter
        //services.AddScoped(typeof(IAttributeFormatter<,>), typeof(AttributeFormatter<,>));


        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        //register all settings
        //var typeFinder = Singleton<ITypeFinder>.Instance;

        //var settings = typeFinder.FindClassesOfType(typeof(ISettings), false).ToList();
        //foreach (var setting in settings)
        //{
        //    services.AddScoped(setting, serviceProvider =>
        //    {
        //        var storeId = DataSettingsManager.IsDatabaseInstalled()
        //            ? serviceProvider.GetRequiredService<IStoreContext>().GetCurrentStore()?.Id ?? 0
        //            : 0;

        //        return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(setting, storeId).Result;
        //    });
        //}

        //picture service
        //if (appSettings.Get<AzureBlobConfig>().Enabled)
        //    services.AddScoped<IPictureService, AzurePictureService>();
        //else
        //    services.AddScoped<IPictureService, PictureService>();

        //roxy file manager
        //services.AddScoped<IRoxyFilemanService, RoxyFilemanService>();
        //services.AddScoped<IRoxyFilemanFileProvider, RoxyFilemanFileProvider>();

        //installation service
        //services.AddScoped<IInstallationService, InstallationService>();

        //slug route transformer
        //if (DataSettingsManager.IsDatabaseInstalled())
        //    services.AddScoped<SlugRouteTransformer>();

        //schedule tasks
        //services.AddSingleton<ITaskScheduler, TaskScheduler>();
        //services.AddTransient<IScheduleTaskRunner, ScheduleTaskRunner>();

        //event consumers
        //var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
        //foreach (var consumer in consumers)
        //foreach (var findInterface in consumer.FindInterfaces((type, criteria) =>
        //         {
        //             var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
        //             return isMatch;
        //         }, typeof(IConsumer<>)))
        //    services.AddScoped(findInterface, consumer);

        //XML sitemap
        //services.AddScoped<IXmlSiteMap, XmlSiteMap>();

        //register the Lazy resolver for .Net IoC
        //var useAutofac = appSettings.Get<CommonConfig>().UseAutofac;
        //if (!useAutofac)
        //    services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));
    }

    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 2000;
}