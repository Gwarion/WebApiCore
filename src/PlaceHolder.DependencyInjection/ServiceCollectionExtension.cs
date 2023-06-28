using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlaceHolder.Application.Services.BackgroundServices;
using PlaceHolder.Application.Services.Cqrs.Dispatchers;
using PlaceHolder.Application.Services.Cqrs.MediatrPipeline;
using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.DependencyInjection.AssemblyUtils;
using PlaceHolder.DependencyInjection.Diagnostics;
using PlaceHolder.DependencyInjection.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PlaceHolder.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            //CQRS
            services.TryAddTransient<ICommandDispatcher, CommandDispatcher>();
            services.TryAddTransient<IQueryDispatcher, QueryDispatcher>();

            var assemblies = AssemblyFinderUtil.GetApplicationCoreAssemblies();

            //MediatR
            services.AddMediatR(assemblies);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DbTransactionPipelineBehavior<,>));

            //FluentValidation
            FluentValidation.AssemblyScanner
                .FindValidatorsInAssemblies(assemblies)
                .ForEach(validator => services.AddScoped(validator.InterfaceType, validator.ValidatorType));

            return services;
        }

        public static IServiceCollection AddAdapters(this IServiceCollection services, IConfiguration configuration)
        {
            var option = new AdaptersOptions();
            configuration.GetSection(AdaptersOptions.Position).Bind(option);

            if (option.AdaptersAssembliesToUse == null || !option.AdaptersAssembliesToUse.Any())
                return services;

            var assembliesPath = AssemblyFinderUtil.GetAdaptersAssembliesPath(option.AdaptersAssembliesToUse);

            foreach(var assemblyPath in assembliesPath)
            {
                try
                {
                    var loadedAdapterAssembly = AssemblyDynamicLoadingUtil.LoadAssemblyAndDependency(assemblyPath);

                    var installerType = loadedAdapterAssembly.ExportedTypes.SingleOrDefault(i => i.IsClass && typeof(IAdapterInstaller).IsAssignableFrom(i));
                    var installer = Activator.CreateInstance(installerType) as IAdapterInstaller;

                    installer.Install(services, configuration);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Failed to load {Path.GetFileName(assemblyPath)} : {e.Message}");
                    throw;
                }
            }

            return services;
        }

        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            services.AddSingleton<BackgroundRequestQueueManager>();
            services.AddTransient<IAsyncCommandDispatcher, AsyncCommandDispatcher>();
            services.AddHostedService<BackgroundRequestService>();

            return services;
        }

        public static IServiceCollection AddDiagnosticListener<TListener>(this IServiceCollection services)
            where TListener : class, IDiagnosticListener
        {
            services.AddSingleton<IDiagnosticListener, TListener>();

            return services;
        }

        public static IApplicationBuilder UseDiagnosticMetrics(this IApplicationBuilder builder)
        {
            var listeners = builder.ApplicationServices.GetServices<IDiagnosticListener>();

            if(listeners.Any())
            {
                var eventObservers = new List<DiagnosticEventObserver>();
                foreach(var group in listeners.GroupBy(l => l.CategoryName))
                {
                    var eventObserver = new DiagnosticEventObserver(group.Key, group.ToList());
                    eventObservers.Add(eventObserver);
                }

                var diagnosticObserver = new DiagnosticObserver(eventObservers);
                _ = DiagnosticListener.AllListeners.Subscribe(diagnosticObserver);
            }

            return builder;
        }
    }
}
