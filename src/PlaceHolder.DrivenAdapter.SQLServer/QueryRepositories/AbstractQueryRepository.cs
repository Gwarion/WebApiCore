using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;
using System.Linq;

namespace PlaceHolder.DrivenAdapter.SQLServer.QueryRepositories
{
    internal abstract class AbstractQueryRepository
    {
        private readonly PlaceHolderContext _context;

        protected readonly IMapper _mapper;

        protected AbstractQueryRepository(PlaceHolderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        internal IQueryable<T> GetAll<T>() where T : TimeStampedEntity
            => _context.Set<T>().AsNoTracking();            
    }
}
