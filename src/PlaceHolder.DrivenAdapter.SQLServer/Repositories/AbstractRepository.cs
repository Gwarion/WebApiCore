using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using System.IO;
using System.Runtime.CompilerServices;

namespace PlaceHolder.DrivenAdapter.SQLServer.Repositories
{
    public abstract class AbstractRepository
    {
        protected PlaceHolderContext _context;

        public AbstractRepository(PlaceHolderContext context) => _context = context;

        protected string GetTag([CallerFilePath] string path = "", [CallerMemberName] string methodName = "")
            => $"{Path.GetFileName(path)} - {methodName}";
    }
}
