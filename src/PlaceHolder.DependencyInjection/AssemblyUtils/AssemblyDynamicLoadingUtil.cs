using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace PlaceHolder.DependencyInjection.AssemblyUtils
{
    public class AssemblyDynamicLoadingUtil
    {
        private static readonly AssemblyLoadContext _loadContext;

        static AssemblyDynamicLoadingUtil()
        {
            _loadContext = AssemblyLoadContext.Default;
            _loadContext.Resolving += OnResolving;
            _loadContext.ResolvingUnmanagedDll += OnResolvingUnmanagedDll;
        }

        public static Assembly LoadAssemblyAndDependency(string assemblyPath)
        {
            Debug.WriteLine($"Loading Adapter Assembly : {Path.GetFileName(assemblyPath)}");

            var loadedAssembly = _loadContext.LoadFromAssemblyPath(assemblyPath);

            foreach(var referenceAssemblyName in loadedAssembly.GetReferencedAssemblies())
            {
                var resolver = new AssemblyDependencyResolver(loadedAssembly.Location);
                var toLoadAssemblyPath = resolver.ResolveAssemblyToPath(referenceAssemblyName);

                if(!String.IsNullOrEmpty(toLoadAssemblyPath))
                {
                    _loadContext.LoadFromAssemblyPath(toLoadAssemblyPath);
                }
                else
                {
                    _loadContext.LoadFromAssemblyName(referenceAssemblyName);
                }
            }

            return loadedAssembly;
        }

        private static IntPtr OnResolvingUnmanagedDll(Assembly loadedAssembly, string assemblyNativeDependencyName)
        {
            var intPtr = IntPtr.Zero;

            Debug.WriteLine($"Trying to resolve native dependency '{assemblyNativeDependencyName}' resolved for '{loadedAssembly.GetName()}'");

            try
            {
                var fallbackRid = PlatformUtil.GetFallbackRid();
                var rid = RuntimeEnvironment.GetRuntimeIdentifier();

                var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var nativeLibrairiesPath = Directory.GetFiles(applicationPath, $"{assemblyNativeDependencyName}", SearchOption.AllDirectories);
                var candidateUnmanagedAssemblyPath = nativeLibrairiesPath.FirstOrDefault(i => i.Contains(rid) || i.Contains(fallbackRid));
                var unmanagedAssemblyPath = Path.GetFullPath(candidateUnmanagedAssemblyPath);

                var loadContext = AssemblyLoadContext.GetLoadContext(loadedAssembly);

                var result = typeof(AssemblyLoadContext)
                    .GetMethod("LoadUnmanagedDllFromPath", BindingFlags.NonPublic | BindingFlags.Instance)
                    .Invoke(loadContext, [unmanagedAssemblyPath]);

                intPtr = (IntPtr)result;

                Debug.WriteLine($"Native dependency '{assemblyNativeDependencyName}' accepted");
            }
            catch(TargetInvocationException)
            {
                Debug.WriteLine($"Native dependency '{assemblyNativeDependencyName}' rejected");
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error on resolving native dependency '{assemblyNativeDependencyName}' for '{loadedAssembly.GetName()}' : '{e.Message}'");
            }

            return intPtr;
        }

        private static Assembly OnResolving(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            Assembly loadedAssembly = null;

            try
            {
                var resolver = new AssemblyDependencyResolver(Assembly.GetExecutingAssembly().Location);
                string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);

                if (assemblyPath != null) return context.LoadFromAssemblyPath(assemblyPath);

                assemblyPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{assemblyName.Name}.dll");
                loadedAssembly = context.LoadFromAssemblyPath(assemblyPath);

                Debug.WriteLine($"Managed dependency '{loadedAssembly.GetName()}' resolved for '{context.Name}'");
            }
            catch
            {
                Debug.WriteLine($"Unresolved managed dependency '{assemblyName.Name}'");
            }

            return loadedAssembly;
        }
    }
}
