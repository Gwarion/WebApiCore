using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;
using PlaceHolder.QueryModel.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceHolder.DrivenAdapter.SQLServer.QueryRepositories
{
    internal class ConsumerQueryRepository : AbstractQueryRepository, IConsumerQueryRepository
    {
        private const int MaxTotalRecords = 5_000_000;        

        public ConsumerQueryRepository(PlaceHolderContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<ConsumerDto> GetOneByIdAsync(Guid guid)
        {
            var consumer = await GetAll<ConsumerEntity>()
                .Include(c => c.Address)
                .SingleOrDefaultAsync(c => c.Guid == guid);

            if (consumer is null) throw new ConsumerNotFoundException(guid);

            return _mapper.Map<ConsumerEntity, ConsumerDto>(consumer);
        }

        public async Task<List<ConsumerDto>> GetAllAsync(int limit = 10)
        {
            var consumers = await GetAll<ConsumerEntity>()
                .Include(c => c.Address)
                .Take(limit)
                .ToListAsync();

            return _mapper.Map<List<ConsumerDto>>(consumers);
        }

        public async Task<IEnumerable<ConsumerDto>> GetAllAsync(int startId, int chunkSize)
        {
            await Task.Delay(100);

            if (startId >= MaxTotalRecords)
                return Enumerable.Empty<ConsumerDto>();

            var chunk = Enumerable.Range(startId, Math.Min(chunkSize, MaxTotalRecords - startId))
                .Select(id => new ConsumerDto(Guid.NewGuid(), $"FirstName_{id}", $"LastName_{id}", $"+33600000001", "test@test.test", "address"))
                .ToList();

            return chunk;
        }
    }
}
