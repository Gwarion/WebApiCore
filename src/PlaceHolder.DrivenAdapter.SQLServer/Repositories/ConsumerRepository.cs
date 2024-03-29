using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;
using System;
using System.Threading.Tasks;

namespace PlaceHolder.DrivenAdapter.SQLServer.Repositories
{
    public class ConsumerRepository : AbstractRepository, IConsumerRepository
    {
        private readonly IMapper _mapper;

        public ConsumerRepository(PlaceHolderContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public async Task<Consumer> SaveAsync(Consumer consumer)
        {
            var dbConsumer = _mapper.Map<Consumer, ConsumerEntity>(consumer);
            var entry = await _context.Consumers.AddAsync(dbConsumer);

            await _context.SaveChangesAsync();

            return _mapper.Map<ConsumerEntity, Consumer>(entry.Entity);
        }

        public async Task<Consumer> UpdateAsync(Consumer consumer)
        {
            var dbConsumer = _mapper.Map<Consumer, ConsumerEntity>(consumer);
            var entry = _context.Consumers.Update(dbConsumer);

            await _context.SaveChangesAsync();

            return _mapper.Map<ConsumerEntity, Consumer>(entry.Entity);
        }
        public async Task<Consumer> GetOneByIdAsync(Guid guid)
        {
            var consumer = await _context.Consumers
                .AsNoTracking()
                .Include(c => c.Address)
                .SingleOrDefaultAsync(c => c.Guid == guid);

            if (consumer is null) throw new ConsumerNotFoundException(guid);

            return _mapper.Map<ConsumerEntity, Consumer>(consumer);
        }
    }
}
