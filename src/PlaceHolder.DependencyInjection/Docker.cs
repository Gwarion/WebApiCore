using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.DependencyInjection
{
    public static class Docker
    {
        public static bool IsStarted() => Environment.GetEnvironmentVariable("RUNNING_IN_DOCKER") != null;
    }
}