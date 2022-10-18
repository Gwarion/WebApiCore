using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PlaceHolder.DependencyInjection.AssemblyUtils
{
    internal class PlatformUtil
    {
        public static readonly string[] NativeLibraryExtensions;
        public static readonly string[] NativeLibraryPrefixes;
        public static readonly string[] ManagedAssemblyExtensions = new[]
        {
            ".dll",
            ".ni.dll",
            ".exe",
            ".ni.exe"
        };

        static PlatformUtil()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                NativeLibraryPrefixes = new[] { "" };
                NativeLibraryExtensions = new[] { ".dll" };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                NativeLibraryPrefixes = new[] { "", "lib" };
                NativeLibraryExtensions = new[] { ".dylib" };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                NativeLibraryPrefixes = new[] { "", "lib" };
                NativeLibraryExtensions = new[] { ".so", ".so.1" };
            }
            else
            {
                NativeLibraryPrefixes = Array.Empty<string>();
                NativeLibraryExtensions = Array.Empty<string>();
            }
        }

        public static string GetFallbackRid()
        {
            string ridBase;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ridBase = "win10";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ridBase = "osx.10.12";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ridBase = "linux";
            }
            else
            {
                return "any";
            }

            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X86 => $"{ridBase}-x86",
                Architecture.X64 => $"{ridBase}-x64",
                Architecture.Arm => $"{ridBase}-arm",
                Architecture.Arm64 => $"{ridBase}-arm64",
                _ => ridBase
            };
        }
    }
}
