using System.Collections.Generic;

namespace PlaceHolder.DependencyInjection.Options
{
    public class AdaptersOptions
    {
        public const string Position = "Adapters";

        public List<string> AdaptersAssembliesToUse { get; set; }
    }
}
