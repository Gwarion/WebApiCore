﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using PlaceHolder.Utils.Exceptions.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbConsumer = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Consumer;

namespace PlaceHolder.DrivenAdapter.SQLServer.Repositories
{
    public class ConsumerRepository : AbstractRepository, IConsumerRepository
    {
        private readonly IMapper _mapper;

        public ConsumerRepository(PlaceHolderContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public async Task<Consumer> SaveAsync(Consumer consumer)
        {
            var dbConsumer = _mapper.Map<Consumer, DbConsumer>(consumer);
            var entry = _context.Consumers.Add(dbConsumer);

            await _context.SaveChangesAsync();

            return _mapper.Map<DbConsumer, Consumer>(entry.Entity);
        }

        public async Task<Consumer> UpdateAsync(Consumer consumer)
        {
            var dbConsumer = _mapper.Map<Consumer, DbConsumer>(consumer);
            var entry = _context.Consumers.Update(dbConsumer);

            await _context.SaveChangesAsync();

            return _mapper.Map<DbConsumer, Consumer>(entry.Entity);
        }

        public async Task<Consumer> GetOneByIdAsync(Guid guid)
        {
            var dbConsumer = await _context.Consumers
                .AsNoTracking()
                .Include(c => c.Address)
                .SingleOrDefaultAsync(c => c.Guid == guid);

            return dbConsumer != null 
                ? _mapper.Map<DbConsumer, Consumer>(dbConsumer)
                : throw new NotFoundException($"No consumer with id : {guid}");
        }

        public async Task<List<Consumer>> GetAllAsync()
        {
            return await _context.Consumers
                .AsNoTracking()
                .Select(dbConsumer => new Consumer
                {
                    Guid = dbConsumer.Guid,
                    FirstName = dbConsumer.FirstName,
                    LastName = dbConsumer.LastName
                })
                .ToListAsync();
        }
    }
}
