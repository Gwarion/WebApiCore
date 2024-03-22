using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;

namespace PlaceHolder.DrivenAdapter.SQLServer.Repositories
{
    public abstract class AbstractRepository
    {
        protected PlaceHolderContext _context;

        protected AbstractRepository(PlaceHolderContext context) => _context = context;
    }
}
