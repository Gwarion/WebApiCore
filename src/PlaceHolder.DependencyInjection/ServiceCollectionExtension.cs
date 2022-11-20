using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlaceHolder.Application.Services.Cqrs.Dispatchers;
using PlaceHolder.Application.Services.Cqrs.MediatrPipeline;
using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.DependencyInjection.AssemblyUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PlaceHolder.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        private static readonly List<string> _adaptersAssembly = new List<string>()
        {
            "PlaceHolder.DrivenAdapter.SQLServer",
            "PlaceHolder.DrivingAdapter.WebApi"
        };

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
            var assembliesPath = AssemblyFinderUtil.GetAdaptersAssembliesPath(_adaptersAssembly);

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
    }
}
