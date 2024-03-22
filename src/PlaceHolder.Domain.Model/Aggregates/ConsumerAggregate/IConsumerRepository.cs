using System;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate
{
    public interface IConsumerRepository
    {
        Task<Consumer> SaveAsync(Consumer consumer);
        Task<Consumer> UpdateAsync(Consumer consumer);
        Task<Consumer> GetOneByIdAsync(Guid guid);
    }
}
