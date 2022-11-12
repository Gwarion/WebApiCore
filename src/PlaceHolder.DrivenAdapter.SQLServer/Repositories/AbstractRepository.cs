using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaceHolder.DrivenAdapter.SQLServer.Repositories
{
    public abstract class AbstractRepository
    {
        protected PlaceHolderContext _context;

        public AbstractRepository(PlaceHolderContext context) => _context = context;
    }
}
