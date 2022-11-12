using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PlaceHolder.DependencyInjection.AssemblyUtils
{
    public static class AssemblyFinderUtil
    {
        private static readonly string ApplicationAssemblyExtension = ".dll";
        private static readonly string ApplicationDomainRootNamespace = "placeholder.domain";
        private static readonly string ApplicationCoreRootNamespace = "placeholder.application";

        private static string GetApplicationPath() => AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

        public static Assembly[] GetApplicationCoreAssemblies()
        {
            var assembliesPath = GetApplicationCoreAssembliesPath();

            return assembliesPath
                .Select(item => AssemblyLoadContext.Default.LoadFromAssemblyPath(item))
                .ToArray();
        }

        public static string[] GetApplicationCoreAssembliesPath()
        {
            return Directory.GetFiles(GetApplicationPath())
                .Where(file => IsApplicationCoreAssembly(file))
                .ToArray();
        }

        private static bool IsApplicationCoreAssembly(string assemblyName)
        {
            var fileName = Path.GetFileName(assemblyName).ToLower();

            return (fileName.Contains(ApplicationDomainRootNamespace) || fileName.Contains(ApplicationCoreRootNamespace))
                    && fileName.EndsWith(ApplicationAssemblyExtension);
        }

        public static string[] GetAdaptersAssembliesPath(List<string> adaptersAssembliesNames)
        {
            var res = new List<string>();
            var applicationPath = GetApplicationPath();

            foreach(var item in adaptersAssembliesNames)
            {
                var path = Path.Combine(applicationPath, $"{item}{ApplicationAssemblyExtension}");

                if(!File.Exists(path))
                {
                    throw new FileNotFoundException($"Adapter assembly not found {path}");
                }

                res.Add(path);
            }

            return res.ToArray();
        }
    }
}
