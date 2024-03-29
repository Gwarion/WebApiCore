using System;
using System.Runtime.InteropServices;

namespace PlaceHolder.DependencyInjection.AssemblyUtils
{
    internal class PlatformUtil
    {
        public static readonly string[] NativeLibraryExtensions;
        public static readonly string[] NativeLibraryPrefixes;
        public static readonly string[] ManagedAssemblyExtensions =
        [
            ".dll",
            ".ni.dll",
            ".exe",
            ".ni.exe"
        ];

        static PlatformUtil()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                NativeLibraryPrefixes = [""];
                NativeLibraryExtensions = [".dll"];
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                NativeLibraryPrefixes = ["", "lib"];
                NativeLibraryExtensions = [".dylib"];
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                NativeLibraryPrefixes = ["", "lib"];
                NativeLibraryExtensions = [".so", ".so.1"];
            }
            else
            {
                NativeLibraryPrefixes = [];
                NativeLibraryExtensions = [];
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
