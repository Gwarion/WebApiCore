using PlaceHolder.Utils.Exceptions.TechnicalExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.DependencyInjection.Options
{
    public class AdaptersOptions
    {
        public static readonly string Position = "Adapters";

        public List<string> AdaptersAssembliesToUse { get; set; }
    }
}
