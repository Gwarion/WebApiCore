using System;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate
{
    public interface IConsumerRepository
    {
        Task<Consumer> GetOneByIdAsync(Guid guid);
        Task<Consumer> SaveAsync(Consumer consumer);
        Task<Consumer> UpdateAsync(Consumer consumer);
    }
}
